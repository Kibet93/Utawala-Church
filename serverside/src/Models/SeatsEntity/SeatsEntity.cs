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
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utawalaaltar.Enums;
using Utawalaaltar.Security;
using Utawalaaltar.Security.Acl;
using Utawalaaltar.Validators;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Z.EntityFramework.Plus;
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

namespace Utawalaaltar.Models {
	// % protected region % [Configure entity attributes here] off begin
	[Table("Seats")]
	// % protected region % [Configure entity attributes here] end
	// % protected region % [Modify class declaration here] off begin
	public class SeatsEntity : IOwnerAbstractModel 
	// % protected region % [Modify class declaration here] end
	{
		[Key]
		public Guid Id { get; set; }
		public Guid Owner { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		// % protected region % [Customise SeatNumber here] off begin
		[EntityAttribute]
		public int? SeatNumber { get; set; }
		// % protected region % [Customise SeatNumber here] end

		/// <summary>
		/// Seat status Open or Reserved
		/// </summary>
		// % protected region % [Customise Reservation here] off begin
		[EntityAttribute]
		public Reservation Reservation { get; set; }
		// % protected region % [Customise Reservation here] end

		[NotMapped]
		public List<Guid> WorkflowBehaviourStateIds { get; set; }

		// % protected region % [Add any further attributes here] off begin
		// % protected region % [Add any further attributes here] end

		public SeatsEntity()
		{
			// % protected region % [Add any constructor logic here] off begin
			// % protected region % [Add any constructor logic here] end
		}

		// % protected region % [Customise ACL attributes here] off begin
		[NotMapped]
		[JsonIgnore]
		// % protected region % [Customise ACL attributes here] end
		public IEnumerable<IAcl> Acls => new List<IAcl>
		{
			// % protected region % [Override ACLs here] off begin
			new SuperAdministratorsScheme(),
			new VisitorsSeatsEntity(),
			new AdminSeatsEntity(),
			new MemberSeatsEntity(),
			new CategoryGroupLeaderSeatsEntity(),
			new UsherSeatsEntity(),
			new ProtocolSeatsEntity(),
			new GroupCategorySeatsEntity(),
			// % protected region % [Override ACLs here] end
			// % protected region % [Add any further ACL entries here] off begin
			// % protected region % [Add any further ACL entries here] end
		};

		// % protected region % [Customise WorkflowStatess here] off begin
		/// <summary>
		/// Incoming many to many reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.SeatsWorkflowStates"/>
		[EntityForeignKey("WorkflowStatess", "Seats", false, typeof(SeatsWorkflowStates))]
		public ICollection<SeatsWorkflowStates> WorkflowStatess { get; set; }
		// % protected region % [Customise WorkflowStatess here] end

		public async Task BeforeSave(
			EntityState operation,
			UtawalaaltarDBContext dbContext,
			IServiceProvider serviceProvider,
			CancellationToken cancellationToken = default)
		{
			// % protected region % [Add any initial before save logic here] off begin
			// % protected region % [Add any initial before save logic here] end

			if ((operation == EntityState.Added || operation == EntityState.Modified) && WorkflowBehaviourStateIds != null)
			{
				// Any states that are associated with this entity are going to be untracked so we can rewrite them
				var existingAssociations = dbContext.ChangeTracker
					.Entries<SeatsWorkflowStates>()
					.Where(e => e.Entity.SeatsId == Id)
					.ToList();

				// Only add states that are published and associated with this entity
				var statesToAdd = dbContext.WorkflowStateEntity
					.AsNoTracking()
					.Include(s => s.WorkflowVersion)
					.ThenInclude(v => v.Workflow)
					.Where(s => WorkflowBehaviourStateIds.Contains(s.Id))
					.Where(s => s.WorkflowVersion.SeatsAssociation == true)
					.Where(s => s.WorkflowVersion.Id == s.WorkflowVersion.Workflow.CurrentVersionId)
					.ToList();

				foreach (var association in existingAssociations)
				{
					if (statesToAdd.Select(s => s.WorkflowVersionId).Contains(association.Entity.WorkflowStatesId))
					{
						association.State = EntityState.Unchanged;
					}
				}

				// Delete any old states that are going to be replaced
				var statesToDelete = dbContext.SeatsWorkflowStates
					.AsNoTracking()
					.Include(s => s.WorkflowStates)
					.ThenInclude(s => s.WorkflowVersion)
					.Where(s => statesToAdd.Select(a => a.WorkflowVersionId).Contains(s.WorkflowStates.WorkflowVersionId) && s.SeatsId == Id)
					.ToList();

				foreach (var workflowState in dbContext.ChangeTracker.Entries<SeatsWorkflowStates>())
				{
					if (statesToDelete.Select(s => s.Id).Contains(workflowState.Entity.Id))
					{
						workflowState.State = EntityState.Detached;
					}
				}
				dbContext.SeatsWorkflowStates.RemoveRange(statesToDelete);

				dbContext.SeatsWorkflowStates.AddRange(statesToAdd.Select(s => new SeatsWorkflowStates
				{
					Owner = Owner,
					SeatsId = Id,
					WorkflowStatesId = s.Id,
				}));


				var newStates = statesToAdd
					.Where(s => !statesToDelete.Select(d => d.WorkflowStates.Id).Contains(s.Id));

				foreach (var newState in newStates)
				{
					// % protected region % [Add any custom workflow logic here] off begin
					// % protected region % [Add any custom workflow logic here] end
				}
			}

			// % protected region % [Add any before save logic here] off begin
			// % protected region % [Add any before save logic here] end
		}

		public async Task AfterSave(
			EntityState operation,
			UtawalaaltarDBContext dbContext,
			IServiceProvider serviceProvider,
			ICollection<ChangeState> changes,
			CancellationToken cancellationToken = default)
		{
			// % protected region % [Add any initial after save logic here] off begin
			// % protected region % [Add any initial after save logic here] end

			// % protected region % [Add any after save logic here] off begin
			// % protected region % [Add any after save logic here] end
		}

		public async Task<int> CleanReference<T>(
			string reference,
			IEnumerable<T> models,
			UtawalaaltarDBContext dbContext,
			CancellationToken cancellation = default)
			where T : IOwnerAbstractModel
			{
			var modelList = models.Cast<SeatsEntity>().ToList();
			var ids = modelList.Select(t => t.Id).ToList();

			switch (reference)
			{
				case "WorkflowStatess":
					var workflowStatesEntities = modelList
						.SelectMany(m => m.WorkflowStatess)
						.Select(m => m.Id);
					var oldWorkflowStates = await dbContext.SeatsWorkflowStates
						.Where(m => ids.Contains(m.SeatsId) && !workflowStatesEntities.Contains(m.Id))
						.ToListAsync(cancellation);
					dbContext.SeatsWorkflowStates.RemoveRange(oldWorkflowStates);

					return oldWorkflowStates.Count;
				// % protected region % [Add any extra clean reference logic here] off begin
				// % protected region % [Add any extra clean reference logic here] end
				default:
					return 0;
			}
		}

		// % protected region % [Add any further references here] off begin
		// % protected region % [Add any further references here] end
	}
}