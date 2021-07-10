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
using Utawalaaltar.Validators;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

namespace Utawalaaltar.Models.RegistrationModels
{
	public class CategoryLeadersEntityRegistrationModel : CategoryLeadersEntityDto, IRegistrationModel<CategoryLeadersEntity>
	{

		// % protected region % [Customise fields for Category Leaders Entity here] off begin
		[Email]
		[Required]
		public new string Email { get; set; }

		[Required]
		public string Password { get; set; }
		// % protected region % [Customise fields for Category Leaders Entity here] end
		// % protected region % [Add extra fields for Category Leaders Entity here] off begin
		// % protected region % [Add extra fields for Category Leaders Entity here] end
		public IList<string> Groups => new List<string> {
			"CategoryLeaders",
			// % protected region % [Add extra groups for Category Leaders Entity here] off begin
			// % protected region % [Add extra groups for Category Leaders Entity here] end
		};

		// % protected region % [Add extra parameters for Category Leaders Entity registration model here] off begin
		// % protected region % [Add extra parameters for Category Leaders Entity registration model here] end

		public override CategoryLeadersEntity ToModel()
		{
			var model = base.ToModel();
			model.Email = Email;

			// % protected region % [Add any extra ToModel logic here] off begin
			// % protected region % [Add any extra ToModel logic here] end

			return model;
		}
	}

	public class CategoryLeadersEntityGraphQlRegistrationModel : CategoryLeadersEntityRegistrationModel
	{
		public MembersEntity Member { get; set; }

		public override CategoryLeadersEntity ToModel()
		{
			var model = base.ToModel();
			model.Member = Member;

			// % protected region % [Add any extra GraphQL ToModel logic here] off begin
			// % protected region % [Add any extra GraphQL ToModel logic here] end

			return model;
		}
	}
}