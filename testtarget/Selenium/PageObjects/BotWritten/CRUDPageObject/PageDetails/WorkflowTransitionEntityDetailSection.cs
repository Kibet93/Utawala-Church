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
	public class WorkflowTransitionEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements
		private static By SourceStateIdElementBy => By.XPath("//*[contains(@class, 'sourceState')]//div[contains(@class, 'dropdown__container')]");
		private static By SourceStateIdInputElementBy => By.XPath("//*[contains(@class, 'sourceState')]/div/input");
		private static By TargetStateIdElementBy => By.XPath("//*[contains(@class, 'targetState')]//div[contains(@class, 'dropdown__container')]");
		private static By TargetStateIdInputElementBy => By.XPath("//*[contains(@class, 'targetState')]/div/input");

		//FlatPickr Elements

		//Attribute Headers
		private readonly WorkflowTransitionEntity _workflowTransitionEntity;

		//Attribute Header Titles
		private IWebElement TransitionNameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Transition Name']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public WorkflowTransitionEntityDetailSection(ContextConfiguration contextConfiguration, WorkflowTransitionEntity workflowTransitionEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_workflowTransitionEntity = workflowTransitionEntity;

			InitializeSelectors();
			// % protected region % [Add any extra construction requires] off begin

			// % protected region % [Add any extra construction requires] end
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements
			selectorDict.Add("TransitionNameElement", (selector: "//div[contains(@class, 'transitionName')]//input", type: SelectorType.XPath));

			// Reference web elements
			selectorDict.Add("SourcestateElement", (selector: ".input-group__dropdown.sourceStateId > .dropdown.dropdown__container", type: SelectorType.CSS));
			selectorDict.Add("TargetstateElement", (selector: ".input-group__dropdown.targetStateId > .dropdown.dropdown__container", type: SelectorType.CSS));

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements
		//get the input path as set by the selector library
		private IWebElement SourceStateElement => FindElementExt("SourceStateElement");
		//get the input path as set by the selector library
		private IWebElement TargetStateElement => FindElementExt("TargetStateElement");

		//Attribute web Elements
		private IWebElement TransitionNameElement => FindElementExt("TransitionNameElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"Transition Name" => TransitionNameHeaderTitle,
				_ => throw new Exception($"Cannot find header tile {attribute}"),
			};
		}

		// Return an IWebElement for an attribute input
		public IWebElement GetInputElement(string attribute)
		{
			switch (attribute)
			{
				case "TransitionName":
					return TransitionNameElement;
				case "SourceStateId":
					return SourceStateElement;
				case "TargetStateId":
					return TargetStateElement;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		public void SetInputElement(string attribute, string value)
		{
			switch (attribute)
			{
				case "TransitionName":
					SetTransitionName(value);
					break;
				case "SourceStateId":
					SetSourceStateId(value);
					break;
				case "TargetStateId":
					SetTargetStateId(value);
					break;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private By GetErrorAttributeSectionAsBy(string attribute)
		{
			return attribute switch
			{
				"TransitionName" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.transitionName > div > p"),
				"SourceStateId" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.sourceStateId > div > p"),
				"TargetStateId" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.targetStateId > div > p"),
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
			SetTransitionName(_workflowTransitionEntity.TransitionName);

			SetSourceStateId(_workflowTransitionEntity.SourceStateId.ToString());
			SetTargetStateId(_workflowTransitionEntity.TargetStateId.ToString());
			// % protected region % [Configure entity application here] end
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "sourcestate":
					return new List<Guid>() {GetSourceStateId()};
				case "targetstate":
					return new List<Guid>() {GetTargetStateId()};
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations
		private void SetSourceStateId(string id)
		{
			if (id == "") { return; }
			WaitUtils.elementState(_driverWait, SourceStateIdInputElementBy, ElementState.VISIBLE);
			var sourceStateIdInputElement = _driver.FindElementExt(SourceStateIdInputElementBy);

			sourceStateIdInputElement.SendKeys(id);
			WaitForDropdownOptions();
			WaitUtils.elementState(_driverWait, By.XPath($"//*/div[@role='option'][@data-id='{id}']"), ElementState.EXISTS);
			sourceStateIdInputElement.SendKeys(Keys.Return);
		}
		private void SetTargetStateId(string id)
		{
			if (id == "") { return; }
			WaitUtils.elementState(_driverWait, TargetStateIdInputElementBy, ElementState.VISIBLE);
			var targetStateIdInputElement = _driver.FindElementExt(TargetStateIdInputElementBy);

			targetStateIdInputElement.SendKeys(id);
			WaitForDropdownOptions();
			WaitUtils.elementState(_driverWait, By.XPath($"//*/div[@role='option'][@data-id='{id}']"), ElementState.EXISTS);
			targetStateIdInputElement.SendKeys(Keys.Return);
		}

		// get associations
		private Guid GetSourceStateId()
		{
			WaitUtils.elementState(_driverWait, SourceStateIdElementBy, ElementState.VISIBLE);
			var sourceStateIdElement = _driver.FindElementExt(SourceStateIdElementBy);
			return new Guid(sourceStateIdElement.GetAttribute("data-id"));
		}
		private Guid GetTargetStateId()
		{
			WaitUtils.elementState(_driverWait, TargetStateIdElementBy, ElementState.VISIBLE);
			var targetStateIdElement = _driver.FindElementExt(TargetStateIdElementBy);
			return new Guid(targetStateIdElement.GetAttribute("data-id"));
		}

		// wait for dropdown to be displaying options
		private void WaitForDropdownOptions()
		{
			var xpath = "//*/div[@aria-expanded='true']";
			var elementBy = WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, xpath);
			WaitUtils.elementState(_driverWait, elementBy,ElementState.EXISTS);
		}

		private void SetTransitionName (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "transitionName", value, _isFastText);
			TransitionNameElement.SendKeys(Keys.Tab);
			TransitionNameElement.SendKeys(Keys.Escape);
		}

		private String GetTransitionName =>
			TransitionNameElement.Text;


		// % protected region % [Add any additional getters and setters of web elements] off begin
		// % protected region % [Add any additional getters and setters of web elements] end
	}
}