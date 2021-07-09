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
	public class CategoryGroupLeaderEntityDto : ModelDto<CategoryGroupLeaderEntity>
	{
		public string Email { get; set; }
		// % protected region % [Customise MemberID here] off begin
		public int? MemberID { get; set; }
		// % protected region % [Customise MemberID here] end

		// % protected region % [Customise CategoryID here] off begin
		public int? CategoryID { get; set; }
		// % protected region % [Customise CategoryID here] end

		// % protected region % [Customise GroupName here] off begin
		public String GroupName { get; set; }
		// % protected region % [Customise GroupName here] end


		// % protected region % [Customise GroupCategoryId here] off begin
		public Guid GroupCategoryId { get; set; }
		// % protected region % [Customise GroupCategoryId here] end

		// % protected region % [Customise MemberId here] off begin
		public Guid MemberId { get; set; }
		// % protected region % [Customise MemberId here] end

		// % protected region % [Add any extra attributes here] off begin
		// % protected region % [Add any extra attributes here] end

		public CategoryGroupLeaderEntityDto(CategoryGroupLeaderEntity model)
		{
			LoadModelData(model);
			// % protected region % [Add any constructor logic here] off begin
			// % protected region % [Add any constructor logic here] end
		}

		public CategoryGroupLeaderEntityDto()
		{
			// % protected region % [Add any parameterless constructor logic here] off begin
			// % protected region % [Add any parameterless constructor logic here] end
		}

		public override CategoryGroupLeaderEntity ToModel()
		{
			// % protected region % [Add any extra ToModel logic here] off begin
			// % protected region % [Add any extra ToModel logic here] end

			return new CategoryGroupLeaderEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Email = Email,
				MemberID = MemberID,
				CategoryID = CategoryID,
				GroupName = GroupName,
				GroupCategoryId  = GroupCategoryId,
				MemberId  = MemberId,
				// % protected region % [Add any extra model properties here] off begin
				// % protected region % [Add any extra model properties here] end
			};
		}

		public override ModelDto<CategoryGroupLeaderEntity> LoadModelData(CategoryGroupLeaderEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Email = model.Email;
			MemberID = model.MemberID;
			CategoryID = model.CategoryID;
			GroupName = model.GroupName;
			GroupCategoryId  = model.GroupCategoryId;
			MemberId  = model.MemberId;

			// % protected region % [Add any extra loading data logic here] off begin
			// % protected region % [Add any extra loading data logic here] end

			return this;
		}

		// % protected region % [Add any extra methods here] off begin
		// % protected region % [Add any extra methods here] end
	}
}