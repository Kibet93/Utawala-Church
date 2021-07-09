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
	public class AccountabilityGroupEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements
		private static By MembersaccountabilitygroupsElementBy => By.XPath("//*[contains(@class, 'membersaccountabilitygroup')]//div[contains(@class, 'dropdown__container')]/a");
		private static By MembersaccountabilitygroupsInputElementBy => By.XPath("//*[contains(@class, 'membersaccountabilitygroup')]/div/input");

		//FlatPickr Elements

		//Attribute Headers
		private readonly AccountabilityGroupEntity _accountabilityGroupEntity;

		//Attribute Header Titles
		private IWebElement NameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Name']"));
		private IWebElement CategoryHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Category']"));
		private IWebElement LeaderIDHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Leader ID']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public AccountabilityGroupEntityDetailSection(ContextConfiguration contextConfiguration, AccountabilityGroupEntity accountabilityGroupEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_accountabilityGroupEntity = accountabilityGroupEntity;

			InitializeSelectors();
			// % protected region % [Add any extra construction requires] off begin

			// % protected region % [Add any extra construction requires] end
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements
			selectorDict.Add("NameElement", (selector: "//div[contains(@class, 'name')]//input", type: SelectorType.XPath));
			selectorDict.Add("CategoryElement", (selector: "//div[contains(@class, 'category')]//input", type: SelectorType.XPath));
			selectorDict.Add("LeaderIDElement", (selector: "//div[contains(@class, 'leaderID')]//input", type: SelectorType.XPath));

			// Reference web elements
			selectorDict.Add("MembersaccountabilitygroupElement", (selector: ".input-group__dropdown.membersaccountabilitygroups > .dropdown.dropdown__container", type: SelectorType.CSS));

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements

		//Attribute web Elements
		private IWebElement NameElement => FindElementExt("NameElement");
		private IWebElement CategoryElement => FindElementExt("CategoryElement");
		private IWebElement LeaderIDElement => FindElementExt("LeaderIDElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"Name" => NameHeaderTitle,
				"Category" => CategoryHeaderTitle,
				"Leader ID" => LeaderIDHeaderTitle,
				_ => throw new Exception($"Cannot find header tile {attribute}"),
			};
		}

		// Return an IWebElement for an attribute input
		public IWebElement GetInputElement(string attribute)
		{
			switch (attribute)
			{
				case "Name":
					return NameElement;
				case "Category":
					return CategoryElement;
				case "LeaderID":
					return LeaderIDElement;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		public void SetInputElement(string attribute, string value)
		{
			switch (attribute)
			{
				case "Name":
					SetName(value);
					break;
				case "Category":
					int? category = null;
					if (int.TryParse(value, out var intCategory))
					{
						category = intCategory;
					}
					SetCategory(category);
					break;
				case "LeaderID":
					int? leaderID = null;
					if (int.TryParse(value, out var intLeaderID))
					{
						leaderID = intLeaderID;
					}
					SetLeaderID(leaderID);
					break;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private By GetErrorAttributeSectionAsBy(string attribute)
		{
			return attribute switch
			{
				"Name" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.name > div > p"),
				"Category" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.category > div > p"),
				"LeaderID" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.leaderID > div > p"),
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
			SetName(_accountabilityGroupEntity.Name);
			SetCategory(_accountabilityGroupEntity.Category);
			SetLeaderID(_accountabilityGroupEntity.LeaderID);

			if (_accountabilityGroupEntity.MembersaccountabilitygroupIds != null)
			{
				SetMembersaccountabilitygroups(_accountabilityGroupEntity.MembersaccountabilitygroupIds.Select(x => x.ToString()));
			}
			// % protected region % [Configure entity application here] end
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "membersaccountabilitygroup":
					return GetMembersaccountabilitygroups();
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations
		private void SetMembersaccountabilitygroups(IEnumerable<string> ids)
		{
			WaitUtils.elementState(_driverWait, MembersaccountabilitygroupsInputElementBy, ElementState.VISIBLE);
			var membersaccountabilitygroupsInputElement = _driver.FindElementExt(MembersaccountabilitygroupsInputElementBy);

			foreach(var id in ids)
			{
				membersaccountabilitygroupsInputElement.SendKeys(id);
				WaitForDropdownOptions();
				membersaccountabilitygroupsInputElement.SendKeys(Keys.Return);
			}
		}


		// get associations
		private List<Guid> GetMembersaccountabilitygroups()
		{
			var guids = new List<Guid>();
			WaitUtils.elementState(_driverWait, MembersaccountabilitygroupsElementBy, ElementState.VISIBLE);
			var membersaccountabilitygroupsElement = _driver.FindElements(MembersaccountabilitygroupsElementBy);

			foreach(var element in membersaccountabilitygroupsElement)
			{
				guids.Add(new Guid (element.GetAttribute("data-id")));
			}
			return guids;
		}

		// wait for dropdown to be displaying options
		private void WaitForDropdownOptions()
		{
			var xpath = "//*/div[@aria-expanded='true']";
			var elementBy = WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, xpath);
			WaitUtils.elementState(_driverWait, elementBy,ElementState.EXISTS);
		}

		private void SetName (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "name", value, _isFastText);
			NameElement.SendKeys(Keys.Tab);
			NameElement.SendKeys(Keys.Escape);
		}

		private String GetName =>
			NameElement.Text;

		private void SetCategory (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "category", intValue.ToString(), _isFastText);
			}
		}

		private int? GetCategory =>
			int.Parse(CategoryElement.Text);

		private void SetLeaderID (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "leaderID", intValue.ToString(), _isFastText);
			}
		}

		private int? GetLeaderID =>
			int.Parse(LeaderIDElement.Text);


		// % protected region % [Add any additional getters and setters of web elements] off begin
		// % protected region % [Add any additional getters and setters of web elements] end
	}
}