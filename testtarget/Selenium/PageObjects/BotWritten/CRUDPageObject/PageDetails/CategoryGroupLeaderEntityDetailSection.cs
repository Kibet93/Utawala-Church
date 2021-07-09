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
using APITests.EntityObjects.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.PageObjects.Components;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using SeleniumTests.Enums;
using SeleniumTests.PageObjects.BotWritten;
// % protected region % [Custom imports] off begin
// % protected region % [Custom imports] end

namespace SeleniumTests.PageObjects.CRUDPageObject.PageDetails
{
	//This section is a mapping from an entity object to an entity create or detailed view page
	public class CategoryGroupLeaderEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements
		private static By GroupCategoryIdElementBy => By.XPath("//*[contains(@class, 'groupCategory')]//div[contains(@class, 'dropdown__container')]");
		private static By GroupCategoryIdInputElementBy => By.XPath("//*[contains(@class, 'groupCategory')]/div/input");
		private static By MemberIdElementBy => By.XPath("//*[contains(@class, 'member')]//div[contains(@class, 'dropdown__container')]");
		private static By MemberIdInputElementBy => By.XPath("//*[contains(@class, 'member')]/div/input");

		//FlatPickr Elements

		//Attribute Headers
		private readonly CategoryGroupLeaderEntity _categoryGroupLeaderEntity;

		//Attribute Header Titles
		private IWebElement MemberIDHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Member ID']"));
		private IWebElement CategoryIDHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Category ID']"));
		private IWebElement GroupNameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Group Name']"));

		// User Entity specific web Elements
		private IWebElement UserEmailElement => FindElementExt("UserEmailElement");
		private IWebElement UserPasswordElement => FindElementExt("UserPasswordElement");
		private IWebElement UserConfirmPasswordElement => FindElementExt("UserConfirmPasswordElement");
		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public CategoryGroupLeaderEntityDetailSection(ContextConfiguration contextConfiguration, CategoryGroupLeaderEntity categoryGroupLeaderEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_categoryGroupLeaderEntity = categoryGroupLeaderEntity;

			InitializeSelectors();
			// % protected region % [Add any extra construction requires] off begin

			// % protected region % [Add any extra construction requires] end
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements
			selectorDict.Add("MemberIDElement", (selector: "//div[contains(@class, 'memberID')]//input", type: SelectorType.XPath));
			selectorDict.Add("CategoryIDElement", (selector: "//div[contains(@class, 'categoryID')]//input", type: SelectorType.XPath));
			selectorDict.Add("GroupNameElement", (selector: "//div[contains(@class, 'groupName')]//input", type: SelectorType.XPath));

			// Reference web elements
			selectorDict.Add("GroupcategoryElement", (selector: ".input-group__dropdown.groupCategoryId > .dropdown.dropdown__container", type: SelectorType.CSS));
			selectorDict.Add("MemberElement", (selector: ".input-group__dropdown.memberId > .dropdown.dropdown__container", type: SelectorType.CSS));

			// User Entity specific web Elements
			selectorDict.Add("UserEmailElement", (selector: "div.email > input", type: SelectorType.CSS));
			selectorDict.Add("UserPasswordElement", (selector: "div.password> input", type: SelectorType.CSS));
			selectorDict.Add("UserConfirmPasswordElement", (selector: "div._confirmPassword > input", type: SelectorType.CSS));

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements
		//get the input path as set by the selector library
		private IWebElement GroupCategoryElement => FindElementExt("GroupCategoryElement");

		//Attribute web Elements
		private IWebElement MemberIDElement => FindElementExt("MemberIDElement");
		private IWebElement CategoryIDElement => FindElementExt("CategoryIDElement");
		private IWebElement GroupNameElement => FindElementExt("GroupNameElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"Member ID" => MemberIDHeaderTitle,
				"Category ID" => CategoryIDHeaderTitle,
				"Group Name" => GroupNameHeaderTitle,
				_ => throw new Exception($"Cannot find header tile {attribute}"),
			};
		}

		// Return an IWebElement for an attribute input
		public IWebElement GetInputElement(string attribute)
		{
			switch (attribute)
			{
				case "MemberID":
					return MemberIDElement;
				case "CategoryID":
					return CategoryIDElement;
				case "GroupName":
					return GroupNameElement;
				case "GroupCategoryId":
					return GroupCategoryElement;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		public void SetInputElement(string attribute, string value)
		{
			switch (attribute)
			{
				case "MemberID":
					int? memberID = null;
					if (int.TryParse(value, out var intMemberID))
					{
						memberID = intMemberID;
					}
					SetMemberID(memberID);
					break;
				case "CategoryID":
					int? categoryID = null;
					if (int.TryParse(value, out var intCategoryID))
					{
						categoryID = intCategoryID;
					}
					SetCategoryID(categoryID);
					break;
				case "GroupName":
					SetGroupName(value);
					break;
				case "GroupCategoryId":
					SetGroupCategoryId(value);
					break;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private By GetErrorAttributeSectionAsBy(string attribute)
		{
			return attribute switch
			{
				"MemberID" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.memberID > div > p"),
				"CategoryID" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.categoryID > div > p"),
				"GroupName" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.groupName > div > p"),
				"GroupCategoryId" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.groupCategoryId > div > p"),
				_ => throw new Exception($"No such attribute {attribute}"),
			};
		}

		public List<string> GetErrorMessagesForAttribute(string attribute)
		{
			var elementBy = GetErrorAttributeSectionAsBy(attribute);
			WaitUtils.elementState(_driverWait, elementBy, ElementState.VISIBLE);
			var element = _driver.FindElementExt(elementBy);
			var errors = new List<string>(element.Text.Split("\r\n"));
			// remove the item in the list which is the name of the attribute and not an error.
			errors.Remove(attribute);
			return errors;
		}

		public void Apply()
		{
			// % protected region % [Configure entity application here] off begin
			SetMemberID(_categoryGroupLeaderEntity.MemberID);
			SetCategoryID(_categoryGroupLeaderEntity.CategoryID);
			SetGroupName(_categoryGroupLeaderEntity.GroupName);

			SetGroupCategoryId(_categoryGroupLeaderEntity.GroupCategoryId.ToString());
			SetMemberId(_categoryGroupLeaderEntity.MemberId.ToString());

			if (_driver.Url == $"{_contextConfiguration.BaseUrl}/admin/categorygroupleaderentity/create")
			{
				SetUserFields(_categoryGroupLeaderEntity);
			}
			// % protected region % [Configure entity application here] end
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "groupcategory":
					return new List<Guid>() {GetGroupCategoryId()};
				case "member":
					return new List<Guid>() {GetMemberId()};
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations
		private void SetGroupCategoryId(string id)
		{
			if (id == "") { return; }
			WaitUtils.elementState(_driverWait, GroupCategoryIdInputElementBy, ElementState.VISIBLE);
			var groupCategoryIdInputElement = _driver.FindElementExt(GroupCategoryIdInputElementBy);

			groupCategoryIdInputElement.SendKeys(id);
			WaitForDropdownOptions();
			WaitUtils.elementState(_driverWait, By.XPath($"//*/div[@role='option'][@data-id='{id}']"), ElementState.EXISTS);
			groupCategoryIdInputElement.SendKeys(Keys.Return);
		}
		private void SetMemberId(string id)
		{
			if (id == "") { return; }
			WaitUtils.elementState(_driverWait, MemberIdInputElementBy, ElementState.VISIBLE);
			var memberIdInputElement = _driver.FindElementExt(MemberIdInputElementBy);

			memberIdInputElement.SendKeys(id);
			WaitForDropdownOptions();
			WaitUtils.elementState(_driverWait, By.XPath($"//*/div[@role='option'][@data-id='{id}']"), ElementState.EXISTS);
			memberIdInputElement.SendKeys(Keys.Return);
		}

		// get associations
		private Guid GetGroupCategoryId()
		{
			WaitUtils.elementState(_driverWait, GroupCategoryIdElementBy, ElementState.VISIBLE);
			var groupCategoryIdElement = _driver.FindElementExt(GroupCategoryIdElementBy);
			return new Guid(groupCategoryIdElement.GetAttribute("data-id"));
		}
		private Guid GetMemberId()
		{
			WaitUtils.elementState(_driverWait, MemberIdElementBy, ElementState.VISIBLE);
			var memberIdElement = _driver.FindElementExt(MemberIdElementBy);
			return new Guid(memberIdElement.GetAttribute("data-id"));
		}

		// wait for dropdown to be displaying options
		private void WaitForDropdownOptions()
		{
			var xpath = "//*/div[@aria-expanded='true']";
			var elementBy = WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, xpath);
			WaitUtils.elementState(_driverWait, elementBy,ElementState.EXISTS);
		}

		private void SetMemberID (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "memberID", intValue.ToString(), _isFastText);
			}
		}

		private int? GetMemberID =>
			int.Parse(MemberIDElement.Text);

		private void SetCategoryID (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "categoryID", intValue.ToString(), _isFastText);
			}
		}

		private int? GetCategoryID =>
			int.Parse(CategoryIDElement.Text);

		private void SetGroupName (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "groupName", value, _isFastText);
			GroupNameElement.SendKeys(Keys.Tab);
			GroupNameElement.SendKeys(Keys.Escape);
		}

		private String GetGroupName =>
			GroupNameElement.Text;

		// set the email, password and confirm password fields
		private void SetUserFields(CategoryGroupLeaderEntity categoryGroupLeaderEntity)
		{
			UserEmailElement.SendKeys(categoryGroupLeaderEntity.EmailAddress);
			UserPasswordElement.SendKeys(categoryGroupLeaderEntity.Password);
			UserConfirmPasswordElement.SendKeys(categoryGroupLeaderEntity.Password);
		}

		// % protected region % [Add any additional getters and setters of web elements] off begin
		// % protected region % [Add any additional getters and setters of web elements] end
	}
}