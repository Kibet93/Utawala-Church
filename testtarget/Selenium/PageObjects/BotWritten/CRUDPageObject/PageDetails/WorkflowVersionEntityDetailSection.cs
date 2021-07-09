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
	public class WorkflowVersionEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements
		private static By StatessElementBy => By.XPath("//*[contains(@class, 'states')]//div[contains(@class, 'dropdown__container')]/a");
		private static By StatessInputElementBy => By.XPath("//*[contains(@class, 'states')]/div/input");
		private static By WorkflowIdElementBy => By.XPath("//*[contains(@class, 'workflow')]//div[contains(@class, 'dropdown__container')]");
		private static By WorkflowIdInputElementBy => By.XPath("//*[contains(@class, 'workflow')]/div/input");
		private static By CurrentWorkflowIdElementBy => By.XPath("//*[contains(@class, 'currentWorkflow')]//div[contains(@class, 'dropdown__container')]");
		private static By CurrentWorkflowIdInputElementBy => By.XPath("//*[contains(@class, 'currentWorkflow')]/div/input");

		//FlatPickr Elements

		//Attribute Headers
		private readonly WorkflowVersionEntity _workflowVersionEntity;

		//Attribute Header Titles
		private IWebElement WorkflowNameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Workflow Name']"));
		private IWebElement WorkflowDescriptionHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Workflow Description']"));
		private IWebElement VersionNumberHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Version Number']"));
		private IWebElement SeatsAssociationHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Seats Association']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public WorkflowVersionEntityDetailSection(ContextConfiguration contextConfiguration, WorkflowVersionEntity workflowVersionEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_workflowVersionEntity = workflowVersionEntity;

			InitializeSelectors();
			// % protected region % [Add any extra construction requires] off begin

			// % protected region % [Add any extra construction requires] end
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements
			selectorDict.Add("WorkflowNameElement", (selector: "//div[contains(@class, 'workflowName')]//input", type: SelectorType.XPath));
			selectorDict.Add("WorkflowDescriptionElement", (selector: "//div[contains(@class, 'workflowDescription')]//input", type: SelectorType.XPath));
			selectorDict.Add("VersionNumberElement", (selector: "//div[contains(@class, 'versionNumber')]//input", type: SelectorType.XPath));
			selectorDict.Add("SeatsAssociationElement", (selector: "//div[contains(@class, 'seatsAssociation')]//input", type: SelectorType.XPath));

			// Reference web elements
			selectorDict.Add("StatesElement", (selector: ".input-group__dropdown.statess > .dropdown.dropdown__container", type: SelectorType.CSS));
			selectorDict.Add("WorkflowElement", (selector: ".input-group__dropdown.workflowId > .dropdown.dropdown__container", type: SelectorType.CSS));
			selectorDict.Add("CurrentworkflowElement", (selector: ".input-group__dropdown.currentWorkflow > .dropdown.dropdown__container", type: SelectorType.CSS));

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements
		//get the input path as set by the selector library
		private IWebElement WorkflowElement => FindElementExt("WorkflowElement");

		//Attribute web Elements
		private IWebElement WorkflowNameElement => FindElementExt("WorkflowNameElement");
		private IWebElement WorkflowDescriptionElement => FindElementExt("WorkflowDescriptionElement");
		private IWebElement VersionNumberElement => FindElementExt("VersionNumberElement");
		private IWebElement SeatsAssociationElement => FindElementExt("SeatsAssociationElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"Workflow Name" => WorkflowNameHeaderTitle,
				"Workflow Description" => WorkflowDescriptionHeaderTitle,
				"Version Number" => VersionNumberHeaderTitle,
				"Seats Association" => SeatsAssociationHeaderTitle,
				_ => throw new Exception($"Cannot find header tile {attribute}"),
			};
		}

		// Return an IWebElement for an attribute input
		public IWebElement GetInputElement(string attribute)
		{
			switch (attribute)
			{
				case "WorkflowName":
					return WorkflowNameElement;
				case "WorkflowDescription":
					return WorkflowDescriptionElement;
				case "VersionNumber":
					return VersionNumberElement;
				case "SeatsAssociation":
					return SeatsAssociationElement;
				case "WorkflowId":
					return WorkflowElement;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		public void SetInputElement(string attribute, string value)
		{
			switch (attribute)
			{
				case "WorkflowName":
					SetWorkflowName(value);
					break;
				case "WorkflowDescription":
					SetWorkflowDescription(value);
					break;
				case "VersionNumber":
					int? versionNumber = null;
					if (int.TryParse(value, out var intVersionNumber))
					{
						versionNumber = intVersionNumber;
					}
					SetVersionNumber(versionNumber);
					break;
				case "SeatsAssociation":
					SetSeatsAssociation(bool.Parse(value));
					break;
				case "WorkflowId":
					SetWorkflowId(value);
					break;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private By GetErrorAttributeSectionAsBy(string attribute)
		{
			return attribute switch
			{
				"WorkflowName" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.workflowName > div > p"),
				"WorkflowDescription" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.workflowDescription > div > p"),
				"VersionNumber" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.versionNumber > div > p"),
				"SeatsAssociation" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.seatsAssociation > div > p"),
				"WorkflowId" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.workflowId > div > p"),
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
			SetWorkflowName(_workflowVersionEntity.WorkflowName);
			SetWorkflowDescription(_workflowVersionEntity.WorkflowDescription);
			SetVersionNumber(_workflowVersionEntity.VersionNumber);
			SetSeatsAssociation(_workflowVersionEntity.SeatsAssociation);

			if (_workflowVersionEntity.StatesIds != null)
			{
				SetStatess(_workflowVersionEntity.StatesIds.Select(x => x.ToString()));
			}
			SetWorkflowId(_workflowVersionEntity.WorkflowId.ToString());
			// % protected region % [Configure entity application here] end
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "states":
					return GetStatess();
				case "workflow":
					return new List<Guid>() {GetWorkflowId()};
				case "currentworkflow":
					return new List<Guid>() {GetCurrentWorkflowId()};
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations
		private void SetStatess(IEnumerable<string> ids)
		{
			WaitUtils.elementState(_driverWait, StatessInputElementBy, ElementState.VISIBLE);
			var statessInputElement = _driver.FindElementExt(StatessInputElementBy);

			foreach(var id in ids)
			{
				statessInputElement.SendKeys(id);
				WaitForDropdownOptions();
				statessInputElement.SendKeys(Keys.Return);
			}
		}

		private void SetWorkflowId(string id)
		{
			if (id == "") { return; }
			WaitUtils.elementState(_driverWait, WorkflowIdInputElementBy, ElementState.VISIBLE);
			var workflowIdInputElement = _driver.FindElementExt(WorkflowIdInputElementBy);

			workflowIdInputElement.SendKeys(id);
			WaitForDropdownOptions();
			WaitUtils.elementState(_driverWait, By.XPath($"//*/div[@role='option'][@data-id='{id}']"), ElementState.EXISTS);
			workflowIdInputElement.SendKeys(Keys.Return);
		}

		// get associations
		private List<Guid> GetStatess()
		{
			var guids = new List<Guid>();
			WaitUtils.elementState(_driverWait, StatessElementBy, ElementState.VISIBLE);
			var statessElement = _driver.FindElements(StatessElementBy);

			foreach(var element in statessElement)
			{
				guids.Add(new Guid (element.GetAttribute("data-id")));
			}
			return guids;
		}
		private Guid GetWorkflowId()
		{
			WaitUtils.elementState(_driverWait, WorkflowIdElementBy, ElementState.VISIBLE);
			var workflowIdElement = _driver.FindElementExt(WorkflowIdElementBy);
			return new Guid(workflowIdElement.GetAttribute("data-id"));
		}
		private Guid GetCurrentWorkflowId()
		{
			WaitUtils.elementState(_driverWait, CurrentWorkflowIdElementBy, ElementState.VISIBLE);
			var currentWorkflowIdElement = _driver.FindElementExt(CurrentWorkflowIdElementBy);
			return new Guid(currentWorkflowIdElement.GetAttribute("data-id"));
		}

		// wait for dropdown to be displaying options
		private void WaitForDropdownOptions()
		{
			var xpath = "//*/div[@aria-expanded='true']";
			var elementBy = WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, xpath);
			WaitUtils.elementState(_driverWait, elementBy,ElementState.EXISTS);
		}

		private void SetWorkflowName (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "workflowName", value, _isFastText);
			WorkflowNameElement.SendKeys(Keys.Tab);
			WorkflowNameElement.SendKeys(Keys.Escape);
		}

		private String GetWorkflowName =>
			WorkflowNameElement.Text;

		private void SetWorkflowDescription (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "workflowDescription", value, _isFastText);
			WorkflowDescriptionElement.SendKeys(Keys.Tab);
			WorkflowDescriptionElement.SendKeys(Keys.Escape);
		}

		private String GetWorkflowDescription =>
			WorkflowDescriptionElement.Text;

		private void SetVersionNumber (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "versionNumber", intValue.ToString(), _isFastText);
			}
		}

		private int? GetVersionNumber =>
			int.Parse(VersionNumberElement.Text);

		private void SetSeatsAssociation (Boolean? value)
		{
			if (value is bool boolValue)
			{
				if (SeatsAssociationElement.Selected != boolValue) {
					SeatsAssociationElement.Click();
				}
			}
		}

		private Boolean? GetSeatsAssociation =>
			SeatsAssociationElement.Selected;


		// % protected region % [Add any additional getters and setters of web elements] off begin
		// % protected region % [Add any additional getters and setters of web elements] end
	}
}