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
	public class AccountabilityGroupsEntityDto : ModelDto<AccountabilityGroupsEntity>
	{
		// % protected region % [Customise Name here] off begin
		public String Name { get; set; }
		// % protected region % [Customise Name here] end

		// % protected region % [Customise Category here] off begin
		public int? Category { get; set; }
		// % protected region % [Customise Category here] end

		// % protected region % [Customise LeaderID here] off begin
		public int? LeaderID { get; set; }
		// % protected region % [Customise LeaderID here] end


		// % protected region % [Add any extra attributes here] off begin
		// % protected region % [Add any extra attributes here] end

		public AccountabilityGroupsEntityDto(AccountabilityGroupsEntity model)
		{
			LoadModelData(model);
			// % protected region % [Add any constructor logic here] off begin
			// % protected region % [Add any constructor logic here] end
		}

		public AccountabilityGroupsEntityDto()
		{
			// % protected region % [Add any parameterless constructor logic here] off begin
			// % protected region % [Add any parameterless constructor logic here] end
		}

		public override AccountabilityGroupsEntity ToModel()
		{
			// % protected region % [Add any extra ToModel logic here] off begin
			// % protected region % [Add any extra ToModel logic here] end

			return new AccountabilityGroupsEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				Category = Category,
				LeaderID = LeaderID,
				// % protected region % [Add any extra model properties here] off begin
				// % protected region % [Add any extra model properties here] end
			};
		}

		public override ModelDto<AccountabilityGroupsEntity> LoadModelData(AccountabilityGroupsEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			Category = model.Category;
			LeaderID = model.LeaderID;

			// % protected region % [Add any extra loading data logic here] off begin
			// % protected region % [Add any extra loading data logic here] end

			return this;
		}

		// % protected region % [Add any extra methods here] off begin
		// % protected region % [Add any extra methods here] end
	}
}