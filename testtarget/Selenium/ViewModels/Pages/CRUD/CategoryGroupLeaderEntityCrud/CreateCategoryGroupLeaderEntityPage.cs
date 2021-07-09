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
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using OpenQA.Selenium;
using SeleniumTests.ViewModels.Components.Attribute;
using SeleniumTests.ViewModels.Pages.CRUD.CategoryGroupLeaderEntityCrud.Internal;
using SeleniumTests.ViewModels.Pages.Crud.Internal;
// % protected region % [Custom imports] off begin
// % protected region % [Custom imports] end

namespace SeleniumTests.ViewModels.Pages.CRUD.CategoryGroupLeaderEntityCrud
{
	public class CreateCategoryGroupLeaderEntityPage : CategoryGroupLeaderEntityAttributeCollection
	{
		// % protected region % [Override class properties here] off begin
		public string Url => ContextConfiguration.BaseUrl + "/admin/categoryGroupLeaderEntity/create";
		public AttributePassword Password => new(By.CssSelector("div.password"), ContextConfiguration);
		public AttributePassword ConfirmPassword => new(By.CssSelector("div._confirmPassword"), ContextConfiguration);
		public CrudCreateButtons ActionButtons => new(ContextConfiguration);
		// % protected region % [Override class properties here] end

		// % protected region % [Override constructor here] off begin
		public CreateCategoryGroupLeaderEntityPage(ContextConfiguration contextConfiguration) : base(contextConfiguration)
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
		public void SetValues(CategoryGroupLeaderEntity categoryGroupLeaderEntity)
		{
			EmailAddress.Value = categoryGroupLeaderEntity.EmailAddress;
			Password.Value = categoryGroupLeaderEntity.Password;
			ConfirmPassword.Value = categoryGroupLeaderEntity.Password;
			MemberID.Value = categoryGroupLeaderEntity.MemberID.ToString();
			CategoryID.Value = categoryGroupLeaderEntity.CategoryID.ToString();
			GroupName.Value = categoryGroupLeaderEntity.GroupName;
			GroupCategoryId.Value = categoryGroupLeaderEntity.GroupCategoryId.ToString();
			MemberId.Value = categoryGroupLeaderEntity.MemberId.ToString();
		}
		// % protected region % [Override set values here] end

		// % protected region % [Override get values here] off begin
		public CategoryGroupLeaderEntity GetValues()
		{
			var categoryGroupLeaderEntity =  new CategoryGroupLeaderEntity
			{
				EmailAddress = EmailAddress.Value,
				Password = Password.Value,
				MemberID = MemberID.Value.ToNullableInt(),
				CategoryID = CategoryID.Value.ToNullableInt(),
				GroupName = GroupName.Value,
			};

			if (Guid.TryParse(GroupCategoryId.Value, out var groupCategoryId)) {
				categoryGroupLeaderEntity.GroupCategoryId = groupCategoryId;
			}
			if (Guid.TryParse(MemberId.Value, out var memberId)) {
				categoryGroupLeaderEntity.MemberId = memberId;
			}
			return categoryGroupLeaderEntity;
		}
		// % protected region % [Override get values here] end

		// % protected region % [Add class methods here] off begin
		// % protected region % [Add class methods here] end
	}
}