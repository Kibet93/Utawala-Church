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
using Utawalaaltar.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

namespace Utawalaaltar.Models
{
	public class SeatsEntityDto : ModelDto<SeatsEntity>
	{
		// % protected region % [Customise SeatNumber here] off begin
		public int? SeatNumber { get; set; }
		// % protected region % [Customise SeatNumber here] end

		// % protected region % [Customise Reservation here] off begin
		/// <summary>
		/// Seat status Open or Reserved
		/// </summary>
		[JsonProperty("reservation")]
		[JsonConverter(typeof(StringEnumConverter))]
		public Reservation Reservation { get; set; }
		// % protected region % [Customise Reservation here] end

		// % protected region % [Customise WorkflowBehaviourStateIds here] off begin
		public List<Guid> WorkflowBehaviourStateIds { get; set; }
		// % protected region % [Customise WorkflowBehaviourStateIds here] end


		// % protected region % [Add any extra attributes here] off begin
		// % protected region % [Add any extra attributes here] end

		public SeatsEntityDto(SeatsEntity model)
		{
			LoadModelData(model);
			// % protected region % [Add any constructor logic here] off begin
			// % protected region % [Add any constructor logic here] end
		}

		public SeatsEntityDto()
		{
			// % protected region % [Add any parameterless constructor logic here] off begin
			// % protected region % [Add any parameterless constructor logic here] end
		}

		public override SeatsEntity ToModel()
		{
			// % protected region % [Add any extra ToModel logic here] off begin
			// % protected region % [Add any extra ToModel logic here] end

			return new SeatsEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				SeatNumber = SeatNumber,
				Reservation = Reservation,
				WorkflowBehaviourStateIds = WorkflowBehaviourStateIds?.ToList(),
				// % protected region % [Add any extra model properties here] off begin
				// % protected region % [Add any extra model properties here] end
			};
		}

		public override ModelDto<SeatsEntity> LoadModelData(SeatsEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			SeatNumber = model.SeatNumber;
			Reservation = model.Reservation;

			// % protected region % [Add any extra loading data logic here] off begin
			// % protected region % [Add any extra loading data logic here] end

			return this;
		}

		// % protected region % [Add any extra methods here] off begin
		// % protected region % [Add any extra methods here] end
	}
}