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
	/// <summary>
	/// State within a workflow
	/// </summary>
	// % protected region % [Configure entity attributes here] off begin
	[Table("WorkflowState")]
	// % protected region % [Configure entity attributes here] end
	// % protected region % [Modify class declaration here] off begin
	public class WorkflowStateEntity : IOwnerAbstractModel 
	// % protected region % [Modify class declaration here] end
	{
		[Key]
		public Guid Id { get; set; }
		public Guid Owner { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		// % protected region % [Customise DisplayIndex here] off begin
		[EntityAttribute]
		public int? DisplayIndex { get; set; }
		// % protected region % [Customise DisplayIndex here] end

		/// <summary>
		/// The name of the state
		/// </summary>
		[Required]
		// % protected region % [Customise StepName here] off begin
		[EntityAttribute]
		public String StepName { get; set; }
		// % protected region % [Customise StepName here] end

		// % protected region % [Customise StateDescription here] off begin
		[EntityAttribute]
		public String StateDescription { get; set; }
		// % protected region % [Customise StateDescription here] end

		// % protected region % [Customise IsStartState here] off begin
		[EntityAttribute]
		public Boolean? IsStartState { get; set; }
		// % protected region % [Customise IsStartState here] end

		// % protected region % [Add any further attributes here] off begin
		// % protected region % [Add any further attributes here] end

		public WorkflowStateEntity()
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
			new AdminWorkflowBehaviour(),
			new MembersWorkflowBehaviour(),
			new CategoryLeadersWorkflowBehaviour(),
			new UsherWorkflowBehaviour(),
			new ProtocolWorkflowBehaviour(),
			// % protected region % [Override ACLs here] end
			// % protected region % [Add any further ACL entries here] off begin
			// % protected region % [Add any further ACL entries here] end
		};

		// % protected region % [Customise WorkflowVersion here] off begin
		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.WorkflowVersionEntity"/>
		public Guid WorkflowVersionId { get; set; }
		[EntityForeignKey("WorkflowVersion", "Statess", true, typeof(WorkflowVersionEntity))]
		public WorkflowVersionEntity WorkflowVersion { get; set; }
		// % protected region % [Customise WorkflowVersion here] end

		// % protected region % [Customise OutgoingTransitionss here] off begin
		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.WorkflowTransitionEntity"/>
		[EntityForeignKey("OutgoingTransitionss", "SourceState", false, typeof(WorkflowTransitionEntity))]
		public ICollection<WorkflowTransitionEntity> OutgoingTransitionss { get; set; }
		// % protected region % [Customise OutgoingTransitionss here] end

		// % protected region % [Customise IncomingTransitionss here] off begin
		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.WorkflowTransitionEntity"/>
		[EntityForeignKey("IncomingTransitionss", "TargetState", false, typeof(WorkflowTransitionEntity))]
		public ICollection<WorkflowTransitionEntity> IncomingTransitionss { get; set; }
		// % protected region % [Customise IncomingTransitionss here] end

		// % protected region % [Customise Seatss here] off begin
		/// <summary>
		/// Outgoing many to many reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.SeatsWorkflowStates"/>
		[EntityForeignKey("Seatss", "WorkflowStates", false, typeof(SeatsWorkflowStates))]
		public ICollection<SeatsWorkflowStates> Seatss { get; set; }
		// % protected region % [Customise Seatss here] end

		public async Task BeforeSave(
			EntityState operation,
			UtawalaaltarDBContext dbContext,
			IServiceProvider serviceProvider,
			CancellationToken cancellationToken = default)
		{
			// % protected region % [Add any initial before save logic here] off begin
			// % protected region % [Add any initial before save logic here] end

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
			var modelList = models.Cast<WorkflowStateEntity>().ToList();
			var ids = modelList.Select(t => t.Id).ToList();

			switch (reference)
			{
				case "Seatss":
					var seatsEntities = modelList
						.SelectMany(m => m.Seatss)
						.Select(m => m.Id);
					var oldSeats = await dbContext.SeatsWorkflowStates
						.Where(m => ids.Contains(m.WorkflowStatesId) && !seatsEntities.Contains(m.Id))
						.ToListAsync(cancellation);
					dbContext.SeatsWorkflowStates.RemoveRange(oldSeats);

					return oldSeats.Count;
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