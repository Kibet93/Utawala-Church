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
using APITests.Classes;
using APITests.EntityObjects.Models;
using EntityObject.Enums;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using SeleniumTests.ViewModels.Pages.CRUD.MembersEntityCrud.Internal;
// % protected region % [Custom imports] off begin
// % protected region % [Custom imports] end

namespace SeleniumTests.ViewModels.Pages.CRUD.MembersEntityCrud
{
	public class ReadMembersEntityPage : MembersEntityAttributeCollection
	{
		// % protected region % [Override class properties here] off begin
		public string Url => ContextConfiguration.BaseUrl + "/admin/membersEntity/view";
		// % protected region % [Override class properties here] end

		// % protected region % [Override constructor here] off begin
		public ReadMembersEntityPage(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
		}
		// % protected region % [Override constructor here] end

		// % protected region % [Override get values here] off begin
		public MembersEntity GetValues()
		{
			var membersEntity =  new MembersEntity
			{
				EmailAddress = EmailAddress.Value,
				FullName = FullName.Value,
				NationalID = NationalID.Value,
				Residence = Residence.Value,
				DateOfBirth = DateOfBirth.Value,
				Age = Age.Value.ToNullableInt(),
				Status = Status.Value.ToEnum<Status>(),
				MembershipStatus = MembershipStatus.Value.ToEnum<Membershipstatus>(),
				CategoryChoice = CategoryChoice.Value.ToEnum<CategoryGroups>(),
				AccountabilityGrp = AccountabilityGrp.Value.ToNullableInt(),
				Picture = new FileData { Filename = Picture.Value },
			};

			if (Guid.TryParse(AccountabilityGroupId.Value, out var accountabilityGroupId)) {
				membersEntity.AccountabilityGroupId = accountabilityGroupId;
			}
			if (Guid.TryParse(HomeFellowshipId.Value, out var homeFellowshipId)) {
				membersEntity.HomeFellowshipId = homeFellowshipId;
			}
			return membersEntity;
		}
		// % protected region % [Override get values here] end

		// % protected region % [Add class methods here] off begin
		// % protected region % [Add class methods here] end
	}
}