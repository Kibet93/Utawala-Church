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
	public class AttendanceEntityDto : ModelDto<AttendanceEntity>
	{
		public String Name { get; set; }
		// % protected region % [Customise DateOfService here] off begin
		public DateTime? DateOfService { get; set; }
		// % protected region % [Customise DateOfService here] end

		// % protected region % [Customise ServiceID here] off begin
		public int? ServiceID { get; set; }
		// % protected region % [Customise ServiceID here] end

		// % protected region % [Customise SeatNoID here] off begin
		public int? SeatNoID { get; set; }
		// % protected region % [Customise SeatNoID here] end

		// % protected region % [Customise Temperature here] off begin
		public Double? Temperature { get; set; }
		// % protected region % [Customise Temperature here] end

		// % protected region % [Customise AttendedService here] off begin
		public Boolean? AttendedService { get; set; }
		// % protected region % [Customise AttendedService here] end

		// % protected region % [Customise ReasonForNotAttending here] off begin
		public String ReasonForNotAttending { get; set; }
		// % protected region % [Customise ReasonForNotAttending here] end

		// % protected region % [Customise Comment here] off begin
		public String Comment { get; set; }
		// % protected region % [Customise Comment here] end


		// % protected region % [Add any extra attributes here] off begin
		// % protected region % [Add any extra attributes here] end

		public AttendanceEntityDto(AttendanceEntity model)
		{
			LoadModelData(model);
			// % protected region % [Add any constructor logic here] off begin
			// % protected region % [Add any constructor logic here] end
		}

		public AttendanceEntityDto()
		{
			// % protected region % [Add any parameterless constructor logic here] off begin
			// % protected region % [Add any parameterless constructor logic here] end
		}

		public override AttendanceEntity ToModel()
		{
			// % protected region % [Add any extra ToModel logic here] off begin
			// % protected region % [Add any extra ToModel logic here] end

			return new AttendanceEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				DateOfService = DateOfService,
				ServiceID = ServiceID,
				SeatNoID = SeatNoID,
				Temperature = Temperature,
				AttendedService = AttendedService,
				ReasonForNotAttending = ReasonForNotAttending,
				Comment = Comment,
				// % protected region % [Add any extra model properties here] off begin
				// % protected region % [Add any extra model properties here] end
			};
		}

		public override ModelDto<AttendanceEntity> LoadModelData(AttendanceEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			DateOfService = model.DateOfService;
			ServiceID = model.ServiceID;
			SeatNoID = model.SeatNoID;
			Temperature = model.Temperature;
			AttendedService = model.AttendedService;
			ReasonForNotAttending = model.ReasonForNotAttending;
			Comment = model.Comment;

			// % protected region % [Add any extra loading data logic here] off begin
			// % protected region % [Add any extra loading data logic here] end

			return this;
		}

		// % protected region % [Add any extra methods here] off begin
		// % protected region % [Add any extra methods here] end
	}
}