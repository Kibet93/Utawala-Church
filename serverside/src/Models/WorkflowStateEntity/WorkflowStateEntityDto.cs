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
using CsvHelper.Configuration.Attributes;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

namespace Utawalaaltar.Models
{
	/// <summary>
	/// State within a workflow
	/// </summary>
	public class WorkflowStateEntityDto : ModelDto<WorkflowStateEntity>
	{
		// % protected region % [Customise DisplayIndex here] off begin
		public int? DisplayIndex { get; set; }
		// % protected region % [Customise DisplayIndex here] end

		// % protected region % [Customise StepName here] off begin
		/// <summary>
		/// The name of the state
		/// </summary>
		public String StepName { get; set; }
		// % protected region % [Customise StepName here] end

		// % protected region % [Customise StateDescription here] off begin
		public String StateDescription { get; set; }
		// % protected region % [Customise StateDescription here] end

		// % protected region % [Customise IsStartState here] off begin
		public Boolean? IsStartState { get; set; }
		// % protected region % [Customise IsStartState here] end

		// % protected region % [Customise WorkflowVersion here] off begin
		[IgnoreAttribute]
		public WorkflowVersionEntityDto? WorkflowVersion { get; set; }
		// % protected region % [Customise WorkflowVersion here] end

		// % protected region % [Customise OutgoingTransitionss here] off begin
		public List<WorkflowTransitionEntityDto> OutgoingTransitionss { get; set; }
		// % protected region % [Customise OutgoingTransitionss here] end


		// % protected region % [Customise WorkflowVersionId here] off begin
		public Guid WorkflowVersionId { get; set; }
		// % protected region % [Customise WorkflowVersionId here] end

		// % protected region % [Add any extra attributes here] off begin
		// % protected region % [Add any extra attributes here] end

		public WorkflowStateEntityDto(WorkflowStateEntity model)
		{
			LoadModelData(model);
			// % protected region % [Add any constructor logic here] off begin
			// % protected region % [Add any constructor logic here] end
		}

		public WorkflowStateEntityDto()
		{
			// % protected region % [Add any parameterless constructor logic here] off begin
			// % protected region % [Add any parameterless constructor logic here] end
		}

		public override WorkflowStateEntity ToModel()
		{
			// % protected region % [Add any extra ToModel logic here] off begin
			// % protected region % [Add any extra ToModel logic here] end

			return new WorkflowStateEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				DisplayIndex = DisplayIndex,
				StepName = StepName,
				StateDescription = StateDescription,
				IsStartState = IsStartState,
				WorkflowVersionId  = WorkflowVersionId,
				// % protected region % [Add any extra model properties here] off begin
				// % protected region % [Add any extra model properties here] end
			};
		}

		public override ModelDto<WorkflowStateEntity> LoadModelData(WorkflowStateEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			DisplayIndex = model.DisplayIndex;
			StepName = model.StepName;
			StateDescription = model.StateDescription;
			IsStartState = model.IsStartState;
			if (model.WorkflowVersion != null)
			{
				WorkflowVersion = new WorkflowVersionEntityDto(model.WorkflowVersion);
			}
			WorkflowVersionId  = model.WorkflowVersionId;

			// % protected region % [Add any extra loading data logic here] off begin
			// % protected region % [Add any extra loading data logic here] end

			return this;
		}

		// % protected region % [Add any extra methods here] off begin
		// % protected region % [Add any extra methods here] end
	}
}