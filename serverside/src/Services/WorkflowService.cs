/*
 * @bot-written
 *
 * WARNING AND NOTICE
 * Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
 * Full Software Licence as accepted by you before being granted access to this source code and other materials,
 * the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-licence. Any
 * commercial use in contravention of the terms of the Full Software Licence may be pursued by Codebots through
 * licence termination and further legal action, and be required to indemnify Codebots for any loss or damage,
 * including interest and costs. You are deemed to have accepted the terms of the Full Software Licence on any
 * access, download, storage, and/or use of this source code.
 *
 * BOT WARNING
 * This file is bot-written.
 * Any changes out side of "protected regions" will be lost next time the bot makes any changes.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Utawalaaltar.Models;
using Utawalaaltar.Services.Interfaces;

namespace Utawalaaltar.Services
{
	public class CreateWorkflowDto
	{
		[Required]
		public WorkflowVersionEntityDto Version { get; set; }

		[Required]
		public IEnumerable<WorkflowStateEntityDto> States { get; set; }

		[Required]
		public IEnumerable<WorkflowTransitionEntityDto> Transitions { get; set; }
	}

	public class WorkflowService : IWorkflowService
	{
		private readonly UtawalaaltarDBContext _dbContext;
		private readonly IIdentityService _identityService;
		private readonly ICrudService _crudService;
		private readonly ISecurityService _securityService;

		public WorkflowService(
			UtawalaaltarDBContext dbContext,
			IIdentityService identityService,
			ICrudService crudService,
			ISecurityService securityService)
		{
			_dbContext = dbContext;
			_identityService = identityService;
			_crudService = crudService;
			_securityService = securityService;
		}

		/// <inheritdoc />
		public Task<WorkflowVersionEntity> GetWorkflowVersion(Guid id)
		{
			return _dbContext.WorkflowVersionEntity
				.Include(v => v.Statess)
				.ThenInclude(s => s.IncomingTransitionss)
				.Include(v => v.Statess)
				.ThenInclude(s => s.OutgoingTransitionss)
				.FirstAsync(v => v.Id == id);
		}

		private async Task<CreateWorkflowDto> GetWorkflowDto(Guid id)
		{
			var result = await GetWorkflowVersion(id);
			return new CreateWorkflowDto
			{
				Version = new WorkflowVersionEntityDto(result),
				States = result.Statess.Select(s => new WorkflowStateEntityDto(s)),
				Transitions = result.Statess.SelectMany(s => s.IncomingTransitionss)
					.Union(result.Statess.SelectMany(s => s.OutgoingTransitionss))
					.Select(t => new WorkflowTransitionEntityDto(t)),
			};
		}

		/// <inheritdoc />
		public async Task<CreateWorkflowDto> CreateVersion(CreateWorkflowDto workflowDto)
		{
			// Wrap the action in a transaction to ensure the correct workflow version id
			await using var transaction = await _dbContext.Database.BeginTransactionAsync();

			var version = workflowDto.Version.ToModel();
			var maxVersion = await _crudService
				.Get<WorkflowVersionEntity>()
				.Where(v => v.WorkflowId == version.WorkflowId)
				.MaxAsync(v => v.VersionNumber);
			version.VersionNumber = maxVersion ?? 1;

			_dbContext.Add(version);
			_dbContext.AddRange(workflowDto.States.Select(x => x.ToModel()).ToList());
			_dbContext.AddRange(workflowDto.Transitions.Select(x => x.ToModel()).ToList());

			var securityErrors = await _securityService.CheckDbSecurityChanges(_identityService, _dbContext);
			if (securityErrors.Any())
			{
				throw new AggregateException(securityErrors.Select(error => new InvalidOperationException(error)));
			}

			var workflow = _dbContext.WorkflowEntity.FirstOrDefault(w => w.Id == version.WorkflowId);
			if (workflow != null)
			{
				var startState = workflowDto.States.FirstOrDefault(x => x.IsStartState == true);
				workflow.CurrentVersionId = version.Id;
				if (version.SeatsAssociation == true && startState != null)
				{
					var seatsWorkflowStates = _dbContext.SeatsEntity
						.Select(x => new SeatsWorkflowStates
						{
							Seats = x, 
							WorkflowStatesId = startState.Id
						});
					_dbContext.SeatsWorkflowStates.AddRange(seatsWorkflowStates);
				}
			}

			await _dbContext.SaveChangesAsync();
			await transaction.CommitAsync();

			return await GetWorkflowDto(version.Id);
		}

		/// <inheritdoc />
		public async Task<CreateWorkflowDto> UpdateVersion(CreateWorkflowDto workflowDto)
		{
			// await using var transaction = await _dbContext.Database.BeginTransactionAsync();

			// Extract models from DTOs
			var version = workflowDto.Version.ToModel();
			var transitions = workflowDto.Transitions.Select(x => x.ToModel()).ToList();
			var states = workflowDto.States.Select(x => x.ToModel()).ToList();

			// Update the version
			await _crudService.Update(version);

			// Get the ids that have been pushed for states and transitions
			var newStateIds = workflowDto.States.Select(s => s.Id);
			var newTransitionIds = workflowDto.Transitions.Select(s => s.Id);

			// Get the existing states and transitions that are already in the DB for this version
			var existingStates = await _crudService.Get<WorkflowStateEntity>()
				.AsNoTracking()
				.Where(s => s.WorkflowVersionId == workflowDto.Version.Id)
				.Include(s => s.IncomingTransitionss)
				.Include(s => s.OutgoingTransitionss)
				.ToListAsync();

			var existingStateIds = existingStates.Select(s => s.Id).ToList();
			var incomingTransitionIds = existingStates
				.SelectMany(s => s.IncomingTransitionss)
				.Select(t => t.Id)
				.ToList();
			var outgoingTransitionIds = existingStates
				.SelectMany(s => s.OutgoingTransitionss)
				.Select(t => t.Id)
				.ToList();
			var existingTransitionIds = outgoingTransitionIds.Union(incomingTransitionIds).ToList();

			// Get the states and transitions to be removed
			var danglingStateIds = existingStateIds.Except(newStateIds).ToList();
			var danglingTransitionIds = existingTransitionIds.Except(newTransitionIds).ToList();

			// Delete any unneeded states and transitions
			await _crudService.Delete<WorkflowTransitionEntity>(danglingTransitionIds);
			await _crudService.Delete<WorkflowStateEntity>(danglingStateIds);

			// Update existing states and transitions
			await _crudService.Update(states.Where(s => existingStateIds.Contains(s.Id)).ToList());
			await _crudService.Update(transitions.Where(t => existingTransitionIds.Contains(t.Id)).ToList());

			// Create any new stats and transitions
			await _crudService.Create(states.Where(s => !existingStateIds.Contains(s.Id)).ToList());
			await _crudService.Create(transitions.Where(s => !existingTransitionIds.Contains(s.Id)).ToList());

			// await transaction.CommitAsync();

			return await GetWorkflowDto(version.Id);
		}

		/// <inheritdoc />
		public async Task<Dictionary<Guid, List<WorkflowStateEntityDto>>> GetSeatsEntityStates(List<Guid> ids)
		{
			// Get the published versions that are associated with this entity
			var workflowVersions = await _dbContext.WorkflowVersionEntity
				.AsNoTracking()
				.Include(v => v.Workflow)
				.Include(v => v.Statess)
				.Where(v => v.SeatsAssociation == true)
				.Where(v => v.Workflow.CurrentVersionId == v.Id)
				.ToListAsync();

			// Get the states that have already been set for this entity
			var existingStates = await _dbContext.SeatsWorkflowStates
				.AsNoTracking()
				.Include(s => s.WorkflowStates)
				.ThenInclude(s => s.WorkflowVersion)
				.Where(s => workflowVersions.Select(v => v.Id).Contains(s.WorkflowStates.WorkflowVersionId))
				.Where(s => ids.Contains(s.SeatsId))
				.ToListAsync();

			// The default states are the states that do no need to be set
			var defaultStates = workflowVersions
				.Select(v => v.Statess.FirstOrDefault(s => s.IsStartState == true))
				.Where(s => s != null)
				.ToList();

			// Get the outgoing transitions for the states
			var outgoingTransitions = await _dbContext.WorkflowTransitionEntity
				.AsNoTracking()
				.Include(t => t.TargetState)
				.Where(t => existingStates.Select(s => s.WorkflowStatesId).Union(defaultStates.Select(s => s.Id)).Contains(t.SourceStateId))
				.Select(t => new WorkflowTransitionEntityDto(t))
				.ToListAsync();

			// Construct a result dictionary
			var results = new Dictionary<Guid, List<WorkflowStateEntityDto>>();
			foreach (var id in ids)
			{
				var entityExistingStates = existingStates.Where(e => e.SeatsId == id).Select(e => e.WorkflowStates).ToList();
				var states = entityExistingStates
					.Union(defaultStates.Where(d => !entityExistingStates.Select(s => s.WorkflowVersionId).Contains(d.WorkflowVersionId)))
					.Select(s =>
					{
						var workflowStateDto = new WorkflowStateEntityDto(s);
						workflowStateDto.OutgoingTransitionss = outgoingTransitions
							.Where(t => t.SourceStateId == workflowStateDto.Id)
							.ToList();
						return workflowStateDto;
					})
					.ToList();

				results.Add(id, states);
			}

			return results;
		}
	}
}