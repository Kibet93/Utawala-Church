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
	public class CategoryLeadersEntityDto : ModelDto<CategoryLeadersEntity>
	{
		public string Email { get; set; }
		// % protected region % [Customise CategoryGroup here] off begin
		[JsonProperty("categoryGroup")]
		[JsonConverter(typeof(StringEnumConverter))]
		public CategoryGroups CategoryGroup { get; set; }
		// % protected region % [Customise CategoryGroup here] end


		// % protected region % [Customise MemberId here] off begin
		public Guid MemberId { get; set; }
		// % protected region % [Customise MemberId here] end

		// % protected region % [Add any extra attributes here] off begin
		// % protected region % [Add any extra attributes here] end

		public CategoryLeadersEntityDto(CategoryLeadersEntity model)
		{
			LoadModelData(model);
			// % protected region % [Add any constructor logic here] off begin
			// % protected region % [Add any constructor logic here] end
		}

		public CategoryLeadersEntityDto()
		{
			// % protected region % [Add any parameterless constructor logic here] off begin
			// % protected region % [Add any parameterless constructor logic here] end
		}

		public override CategoryLeadersEntity ToModel()
		{
			// % protected region % [Add any extra ToModel logic here] off begin
			// % protected region % [Add any extra ToModel logic here] end

			return new CategoryLeadersEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Email = Email,
				CategoryGroup = CategoryGroup,
				MemberId  = MemberId,
				// % protected region % [Add any extra model properties here] off begin
				// % protected region % [Add any extra model properties here] end
			};
		}

		public override ModelDto<CategoryLeadersEntity> LoadModelData(CategoryLeadersEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Email = model.Email;
			CategoryGroup = model.CategoryGroup;
			MemberId  = model.MemberId;

			// % protected region % [Add any extra loading data logic here] off begin
			// % protected region % [Add any extra loading data logic here] end

			return this;
		}

		// % protected region % [Add any extra methods here] off begin
		// % protected region % [Add any extra methods here] end
	}
}