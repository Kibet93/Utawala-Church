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
using System.Threading;
using System.Threading.Tasks;
using Utawalaaltar.Enums;
using Utawalaaltar.Security;
using Utawalaaltar.Security.Acl;
using Microsoft.EntityFrameworkCore;
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

namespace Utawalaaltar.Models {
	public class SeatsWorkflowStates : IOwnerAbstractModel
	{
		[Key]
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public Guid Owner { get; set; }

		public SeatsWorkflowStates() {}

		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.SeatsEntity"/>
		public Guid SeatsId { get; set; }
		[EntityForeignKey("Seats", "WorkflowStatess", true, typeof(SeatsEntity))]
		public SeatsEntity Seats { get; set; }

		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.WorkflowStateEntity"/>
		public Guid WorkflowStatesId { get; set; }
		[EntityForeignKey("WorkflowStates", "Seatss", true, typeof(WorkflowStateEntity))]
		public WorkflowStateEntity WorkflowStates { get; set; }

		public async Task BeforeSave(EntityState operation, UtawalaaltarDBContext dbContext, IServiceProvider serviceProvider, CancellationToken cancellationToken = default)
		{
			// % protected region % [Add any before save logic here] off begin
			// % protected region % [Add any before save logic here] end
		}

		public async Task AfterSave(EntityState operation, UtawalaaltarDBContext dbContext, IServiceProvider serviceProvider, ICollection<ChangeState> changes, CancellationToken cancellationToken = default)
		{
			// % protected region % [Add any after save logic here] off begin
			// % protected region % [Add any after save logic here] end
		}

		[NotMapped]
		public IEnumerable<IAcl> Acls => new List<IAcl>
		{
			// % protected region % [Override ACLs here] off begin
			new SuperAdministratorsScheme(),
			new AdminWorkflowBehaviour(),
			new MemberWorkflowBehaviour(),
			new CategoryGroupLeaderWorkflowBehaviour(),
			new UsherWorkflowBehaviour(),
			new ProtocolWorkflowBehaviour(),
			new GroupCategoryWorkflowBehaviour(),
			// % protected region % [Override ACLs here] end
			// % protected region % [Add any further ACL entries here] off begin
			// % protected region % [Add any further ACL entries here] end
		};

		// % protected region % [Add any further references here] off begin
		// % protected region % [Add any further references here] end

		public async Task<int> CleanReference<T>(
			string reference,
			IEnumerable<T> models,
			UtawalaaltarDBContext dbContext,
			CancellationToken cancellation = default)
			where T : IOwnerAbstractModel => 0;
	}
}