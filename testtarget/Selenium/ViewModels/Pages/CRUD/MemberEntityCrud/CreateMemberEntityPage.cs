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
using APITests.EntityObjects.Models;
using EntityObject.Enums;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using OpenQA.Selenium;
using SeleniumTests.ViewModels.Components.Attribute;
using SeleniumTests.ViewModels.Pages.CRUD.MemberEntityCrud.Internal;
using SeleniumTests.ViewModels.Pages.Crud.Internal;
// % protected region % [Custom imports] off begin
// % protected region % [Custom imports] end

namespace SeleniumTests.ViewModels.Pages.CRUD.MemberEntityCrud
{
	public class CreateMemberEntityPage : MemberEntityAttributeCollection
	{
		// % protected region % [Override class properties here] off begin
		public string Url => ContextConfiguration.BaseUrl + "/admin/memberEntity/create";
		public AttributePassword Password => new(By.CssSelector("div.password"), ContextConfiguration);
		public AttributePassword ConfirmPassword => new(By.CssSelector("div._confirmPassword"), ContextConfiguration);
		public CrudCreateButtons ActionButtons => new(ContextConfiguration);
		// % protected region % [Override class properties here] end

		// % protected region % [Override constructor here] off begin
		public CreateMemberEntityPage(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
		}
		// % protected region % [Override constructor here] end
		// % protected region % [Override Navigate here] off begin
		public void Navigate()
		{
			ContextConfiguration.WebDriver.GoToUrlExt(ContextConfiguration.WebDriverWait, Url);
		}
		// % protected region % [Override Navigate here] end

		// % protected region % [Override set values here] off begin
		public void SetValues(MemberEntity memberEntity)
		{
			EmailAddress.Value = memberEntity.EmailAddress;
			Password.Value = memberEntity.Password;
			ConfirmPassword.Value = memberEntity.Password;
			Name.Value = memberEntity.Name;
			MemberID.Value = memberEntity.MemberID.ToString();
			FullName.Value = memberEntity.FullName;
			NationalID.Value = memberEntity.NationalID;
			Residence.Value = memberEntity.Residence;
			DateOfBirth.Value = memberEntity.DateOfBirth.GetValueOrDefault();
			Age.Value = memberEntity.Age.ToString();
			CategoryID.Value = memberEntity.CategoryID.ToString();
			Status.Value = memberEntity.Status.ToString();
			MembershipStatus.Value = memberEntity.MembershipStatus.ToString();
			AccountabilityGroupId.Value = memberEntity.AccountabilityGroupId == null ? string.Empty : memberEntity.AccountabilityGroupId.ToString();
			GroupCategoryId.Value = memberEntity.GroupCategoryId == null ? string.Empty : memberEntity.GroupCategoryId.ToString();
			HomeFellowshipId.Value = memberEntity.HomeFellowshipId == null ? string.Empty : memberEntity.HomeFellowshipId.ToString();
		}
		// % protected region % [Override set values here] end

		// % protected region % [Override get values here] off begin
		public MemberEntity GetValues()
		{
			var memberEntity =  new MemberEntity
			{
				EmailAddress = EmailAddress.Value,
				Password = Password.Value,
				Name = Name.Value,
				MemberID = MemberID.Value.ToNullableInt(),
				FullName = FullName.Value,
				NationalID = NationalID.Value,
				Residence = Residence.Value,
				DateOfBirth = DateOfBirth.Value,
				Age = Age.Value.ToNullableInt(),
				CategoryID = CategoryID.Value.ToNullableInt(),
				Status = Status.Value.ToEnum<Status>(),
				MembershipStatus = MembershipStatus.Value.ToEnum<Membershipstatus>(),
			};

			if (Guid.TryParse(AccountabilityGroupId.Value, out var accountabilityGroupId)) {
				memberEntity.AccountabilityGroupId = accountabilityGroupId;
			}
			if (Guid.TryParse(GroupCategoryId.Value, out var groupCategoryId)) {
				memberEntity.GroupCategoryId = groupCategoryId;
			}
			if (Guid.TryParse(HomeFellowshipId.Value, out var homeFellowshipId)) {
				memberEntity.HomeFellowshipId = homeFellowshipId;
			}
			return memberEntity;
		}
		// % protected region % [Override get values here] end

		// % protected region % [Add class methods here] off begin
		// % protected region % [Add class methods here] end
	}
}