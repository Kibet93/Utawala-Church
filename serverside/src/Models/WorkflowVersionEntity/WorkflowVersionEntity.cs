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
	/// Version of Workflow
	/// </summary>
	// % protected region % [Configure entity attributes here] off begin
	[Table("WorkflowVersion")]
	// % protected region % [Configure entity attributes here] end
	// % protected region % [Modify class declaration here] off begin
	public class WorkflowVersionEntity : IOwnerAbstractModel 
	// % protected region % [Modify class declaration here] end
	{
		[Key]
		public Guid Id { get; set; }
		public Guid Owner { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		/// <summary>
		/// Workflow Name
		/// </summary>
		[Required]
		// % protected region % [Customise WorkflowName here] off begin
		[EntityAttribute]
		public String WorkflowName { get; set; }
		// % protected region % [Customise WorkflowName here] end

		/// <summary>
		/// Description of Workflow
		/// </summary>
		// % protected region % [Customise WorkflowDescription here] off begin
		[EntityAttribute]
		public String WorkflowDescription { get; set; }
		// % protected region % [Customise WorkflowDescription here] end

		/// <summary>
		/// Version Number of Workflow Version
		/// </summary>
		// % protected region % [Customise VersionNumber here] off begin
		[EntityAttribute]
		public int? VersionNumber { get; set; }
		// % protected region % [Customise VersionNumber here] end

		/// <summary>
		/// If Seats's are associated with this workflow version
		/// </summary>
		// % protected region % [Customise SeatsAssociation here] off begin
		[EntityAttribute]
		public Boolean? SeatsAssociation { get; set; }
		// % protected region % [Customise SeatsAssociation here] end

		// % protected region % [Add any further attributes here] off begin
		// % protected region % [Add any further attributes here] end

		public WorkflowVersionEntity()
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

		// % protected region % [Customise Statess here] off begin
		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.WorkflowStateEntity"/>
		[EntityForeignKey("Statess", "WorkflowVersion", false, typeof(WorkflowStateEntity))]
		public ICollection<WorkflowStateEntity> Statess { get; set; }
		// % protected region % [Customise Statess here] end

		// % protected region % [Customise Workflow here] off begin
		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.WorkflowEntity"/>
		public Guid WorkflowId { get; set; }
		[EntityForeignKey("Workflow", "Versionss", true, typeof(WorkflowEntity))]
		public WorkflowEntity Workflow { get; set; }
		// % protected region % [Customise Workflow here] end

		// % protected region % [Customise CurrentWorkflow here] off begin
		/// <summary>
		/// Incoming one to one reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.WorkflowEntity"/>
		[EntityForeignKey("CurrentWorkflow", "CurrentVersion", false, typeof(WorkflowEntity))]
		public WorkflowEntity CurrentWorkflow { get; set; }
		// % protected region % [Customise CurrentWorkflow here] end

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
			var modelList = models.Cast<WorkflowVersionEntity>().ToList();
			var ids = modelList.Select(t => t.Id).ToList();

			switch (reference)
			{
				case "CurrentWorkflow":
					var currentWorkflowIds = modelList.Select(x => x.CurrentWorkflow.Id).ToList();
					var oldcurrentWorkflow = await dbContext.WorkflowEntity
						.Where(m => m.CurrentVersionId.HasValue && ids.Contains(m.CurrentVersionId.Value))
						.Where(m => !currentWorkflowIds.Contains(m.Id))
						.ToListAsync(cancellation);

					foreach (var currentWorkflow in oldcurrentWorkflow)
					{
						currentWorkflow.CurrentVersionId = null;
					}

					dbContext.WorkflowEntity.UpdateRange(oldcurrentWorkflow);
					return oldcurrentWorkflow.Count;
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