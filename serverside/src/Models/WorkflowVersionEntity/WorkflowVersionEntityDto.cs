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
using System.Linq;
using System.Collections.Generic;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

namespace Utawalaaltar.Models
{
	/// <summary>
	/// Version of Workflow
	/// </summary>
	public class WorkflowVersionEntityDto : ModelDto<WorkflowVersionEntity>
	{
		// % protected region % [Customise WorkflowName here] off begin
		/// <summary>
		/// Workflow Name
		/// </summary>
		public String WorkflowName { get; set; }
		// % protected region % [Customise WorkflowName here] end

		// % protected region % [Customise WorkflowDescription here] off begin
		/// <summary>
		/// Description of Workflow
		/// </summary>
		public String WorkflowDescription { get; set; }
		// % protected region % [Customise WorkflowDescription here] end

		// % protected region % [Customise VersionNumber here] off begin
		/// <summary>
		/// Version Number of Workflow Version
		/// </summary>
		public int? VersionNumber { get; set; }
		// % protected region % [Customise VersionNumber here] end

		// % protected region % [Customise SeatsAssociation here] off begin
		/// <summary>
		/// If Seats's are associated with this workflow version
		/// </summary>
		public Boolean? SeatsAssociation { get; set; }
		// % protected region % [Customise SeatsAssociation here] end


		// % protected region % [Customise WorkflowId here] off begin
		public Guid WorkflowId { get; set; }
		// % protected region % [Customise WorkflowId here] end

		// % protected region % [Add any extra attributes here] off begin
		// % protected region % [Add any extra attributes here] end

		public WorkflowVersionEntityDto(WorkflowVersionEntity model)
		{
			LoadModelData(model);
			// % protected region % [Add any constructor logic here] off begin
			// % protected region % [Add any constructor logic here] end
		}

		public WorkflowVersionEntityDto()
		{
			// % protected region % [Add any parameterless constructor logic here] off begin
			// % protected region % [Add any parameterless constructor logic here] end
		}

		public override WorkflowVersionEntity ToModel()
		{
			// % protected region % [Add any extra ToModel logic here] off begin
			// % protected region % [Add any extra ToModel logic here] end

			return new WorkflowVersionEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				WorkflowName = WorkflowName,
				WorkflowDescription = WorkflowDescription,
				VersionNumber = VersionNumber,
				SeatsAssociation = SeatsAssociation,
				WorkflowId  = WorkflowId,
				// % protected region % [Add any extra model properties here] off begin
				// % protected region % [Add any extra model properties here] end
			};
		}

		public override ModelDto<WorkflowVersionEntity> LoadModelData(WorkflowVersionEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			WorkflowName = model.WorkflowName;
			WorkflowDescription = model.WorkflowDescription;
			VersionNumber = model.VersionNumber;
			SeatsAssociation = model.SeatsAssociation;
			WorkflowId  = model.WorkflowId;

			// % protected region % [Add any extra loading data logic here] off begin
			// % protected region % [Add any extra loading data logic here] end

			return this;
		}

		// % protected region % [Add any extra methods here] off begin
		// % protected region % [Add any extra methods here] end
	}
}