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
using OpenQA.Selenium;
using SeleniumTests.PageObjects.Pages;
using SeleniumTests.Setup;
using SeleniumTests.ViewModels.Components.Attribute;
// % protected region % [Custom imports] off begin
// % protected region % [Custom imports] end

namespace SeleniumTests.ViewModels.Pages.CRUD.MemberEntityCrud.Internal
{
	public class MemberEntityAttributeCollection : Page
	{
		// % protected region % [Override class properties here] off begin
		public AttributeTextField EmailAddress => new(By.CssSelector("div.email"), ContextConfiguration);
		public AttributeTextField Name => new(By.CssSelector("div.name"), ContextConfiguration);
		public AttributeTextField MemberID => new(By.CssSelector("div.memberID"), ContextConfiguration);
		public AttributeTextField FullName => new(By.CssSelector("div.fullName"), ContextConfiguration);
		public AttributeTextField NationalID => new(By.CssSelector("div.nationalID"), ContextConfiguration);
		public AttributeTextField Residence => new(By.CssSelector("div.residence"), ContextConfiguration);
		public AttributeDatePicker DateOfBirth => new(By.CssSelector("div.dateOfBirth"), ContextConfiguration);
		public AttributeTextField Age => new(By.CssSelector("div.age"), ContextConfiguration);
		public AttributeTextField CategoryID => new(By.CssSelector("div.categoryID"), ContextConfiguration);
		public AttributeEnumCombobox Status => new(By.CssSelector("div.status"), ContextConfiguration);
		public AttributeEnumCombobox MembershipStatus => new(By.CssSelector("div.membershipStatus"), ContextConfiguration);
		public AttributeReferenceMultiCombobox FormPageIds => new(By.CssSelector("div.formPages"), ContextConfiguration);
		public AttributeDisplayField CategoryGroupLeaderId => new(By.CssSelector("div.categoryGroupLeader"), ContextConfiguration);
		public AttributeDisplayField ProtocolId => new(By.CssSelector("div.protocol"), ContextConfiguration);
		public AttributeDisplayField UshersId => new(By.CssSelector("div.ushers"), ContextConfiguration);
		public AttributeReferenceCombobox AccountabilityGroupId => new(By.CssSelector("div.accountabilityGroupId"), ContextConfiguration);
		public AttributeReferenceCombobox GroupCategoryId => new(By.CssSelector("div.groupCategoryId"), ContextConfiguration);
		public AttributeReferenceCombobox HomeFellowshipId => new(By.CssSelector("div.homeFellowshipId"), ContextConfiguration);
		// % protected region % [Override class properties here] end

		// % protected region % [Override constructor here] off begin
		public MemberEntityAttributeCollection(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
		}
		// % protected region % [Override constructor here] end

		// % protected region % [Add class methods here] off begin
		// % protected region % [Add class methods here] end
	}
}