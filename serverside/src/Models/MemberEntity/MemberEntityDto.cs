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
	public class MemberEntityDto : ModelDto<MemberEntity>
	{
		public string Email { get; set; }
		public String Name { get; set; }
		// % protected region % [Customise MemberID here] off begin
		public int? MemberID { get; set; }
		// % protected region % [Customise MemberID here] end

		// % protected region % [Customise FullName here] off begin
		public String FullName { get; set; }
		// % protected region % [Customise FullName here] end

		// % protected region % [Customise NationalID here] off begin
		public String NationalID { get; set; }
		// % protected region % [Customise NationalID here] end

		// % protected region % [Customise Residence here] off begin
		public String Residence { get; set; }
		// % protected region % [Customise Residence here] end

		// % protected region % [Customise DateOfBirth here] off begin
		/// <summary>
		/// For age Calculation
		/// </summary>
		public DateTime? DateOfBirth { get; set; }
		// % protected region % [Customise DateOfBirth here] end

		// % protected region % [Customise Age here] off begin
		/// <summary>
		/// Current Age
		/// </summary>
		public int? Age { get; set; }
		// % protected region % [Customise Age here] end

		// % protected region % [Customise CategoryID here] off begin
		public int? CategoryID { get; set; }
		// % protected region % [Customise CategoryID here] end

		// % protected region % [Customise Status here] off begin
		[JsonProperty("status")]
		[JsonConverter(typeof(StringEnumConverter))]
		public Status Status { get; set; }
		// % protected region % [Customise Status here] end

		// % protected region % [Customise MembershipStatus here] off begin
		[JsonProperty("membershipStatus")]
		[JsonConverter(typeof(StringEnumConverter))]
		public Membershipstatus MembershipStatus { get; set; }
		// % protected region % [Customise MembershipStatus here] end


		// % protected region % [Customise AccountabilityGroupId here] off begin
		public Guid? AccountabilityGroupId { get; set; }
		// % protected region % [Customise AccountabilityGroupId here] end

		// % protected region % [Customise GroupCategoryId here] off begin
		public Guid? GroupCategoryId { get; set; }
		// % protected region % [Customise GroupCategoryId here] end

		// % protected region % [Customise HomeFellowshipId here] off begin
		public Guid? HomeFellowshipId { get; set; }
		// % protected region % [Customise HomeFellowshipId here] end

		// % protected region % [Add any extra attributes here] off begin
		// % protected region % [Add any extra attributes here] end

		public MemberEntityDto(MemberEntity model)
		{
			LoadModelData(model);
			// % protected region % [Add any constructor logic here] off begin
			// % protected region % [Add any constructor logic here] end
		}

		public MemberEntityDto()
		{
			// % protected region % [Add any parameterless constructor logic here] off begin
			// % protected region % [Add any parameterless constructor logic here] end
		}

		public override MemberEntity ToModel()
		{
			// % protected region % [Add any extra ToModel logic here] off begin
			// % protected region % [Add any extra ToModel logic here] end

			return new MemberEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				Email = Email,
				MemberID = MemberID,
				FullName = FullName,
				NationalID = NationalID,
				Residence = Residence,
				DateOfBirth = DateOfBirth,
				Age = Age,
				CategoryID = CategoryID,
				Status = Status,
				MembershipStatus = MembershipStatus,
				AccountabilityGroupId  = AccountabilityGroupId,
				GroupCategoryId  = GroupCategoryId,
				HomeFellowshipId  = HomeFellowshipId,
				// % protected region % [Add any extra model properties here] off begin
				// % protected region % [Add any extra model properties here] end
			};
		}

		public override ModelDto<MemberEntity> LoadModelData(MemberEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			Email = model.Email;
			MemberID = model.MemberID;
			FullName = model.FullName;
			NationalID = model.NationalID;
			Residence = model.Residence;
			DateOfBirth = model.DateOfBirth;
			Age = model.Age;
			CategoryID = model.CategoryID;
			Status = model.Status;
			MembershipStatus = model.MembershipStatus;
			AccountabilityGroupId  = model.AccountabilityGroupId;
			GroupCategoryId  = model.GroupCategoryId;
			HomeFellowshipId  = model.HomeFellowshipId;

			// % protected region % [Add any extra loading data logic here] off begin
			// % protected region % [Add any extra loading data logic here] end

			return this;
		}

		// % protected region % [Add any extra methods here] off begin
		// % protected region % [Add any extra methods here] end
	}
}