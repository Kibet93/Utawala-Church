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
	public class AttendanceEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements

		//FlatPickr Elements
		private DateTimePickerComponent DateOfServiceElement => new DateTimePickerComponent(_contextConfiguration, "dateOfService");

		//Attribute Headers
		private readonly AttendanceEntity _attendanceEntity;

		//Attribute Header Titles
		private IWebElement DateOfServiceHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Date Of Service']"));
		private IWebElement ServiceIDHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Service ID']"));
		private IWebElement SeatNoIDHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Seat No ID']"));
		private IWebElement TemperatureHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Temperature']"));
		private IWebElement AttendedServiceHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Attended Service']"));
		private IWebElement ReasonForNotAttendingHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Reason For Not Attending']"));
		private IWebElement CommentHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Comment']"));
		private IWebElement NameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Name']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public AttendanceEntityDetailSection(ContextConfiguration contextConfiguration, AttendanceEntity attendanceEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_attendanceEntity = attendanceEntity;

			InitializeSelectors();
			// % protected region % [Add any extra construction requires] off begin
			// % protected region % [Add any extra construction requires] end
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements
			selectorDict.Add("ServiceIDElement", (selector: "//div[contains(@class, 'serviceID')]//input", type: SelectorType.XPath));
			selectorDict.Add("SeatNoIDElement", (selector: "//div[contains(@class, 'seatNoID')]//input", type: SelectorType.XPath));
			selectorDict.Add("TemperatureElement", (selector: "//div[contains(@class, 'temperature')]//input", type: SelectorType.XPath));
			selectorDict.Add("AttendedServiceElement", (selector: "//div[contains(@class, 'attendedService')]//input", type: SelectorType.XPath));
			selectorDict.Add("ReasonForNotAttendingElement", (selector: "//div[contains(@class, 'reasonForNotAttending')]//input", type: SelectorType.XPath));
			selectorDict.Add("CommentElement", (selector: "//div[contains(@class, 'comment')]//input", type: SelectorType.XPath));

			// Reference web elements

			// Form Entity specific web Element
			selectorDict.Add("NameElement", (selector: "div.name > input", type: SelectorType.CSS));

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements

		//Attribute web Elements
		private IWebElement ServiceIDElement => FindElementExt("ServiceIDElement");
		private IWebElement SeatNoIDElement => FindElementExt("SeatNoIDElement");
		private IWebElement TemperatureElement => FindElementExt("TemperatureElement");
		private IWebElement AttendedServiceElement => FindElementExt("AttendedServiceElement");
		private IWebElement ReasonForNotAttendingElement => FindElementExt("ReasonForNotAttendingElement");
		private IWebElement CommentElement => FindElementExt("CommentElement");
		private IWebElement NameElement => FindElementExt("NameElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"Date Of Service" => DateOfServiceHeaderTitle,
				"Service ID" => ServiceIDHeaderTitle,
				"Seat No ID" => SeatNoIDHeaderTitle,
				"Temperature" => TemperatureHeaderTitle,
				"Attended Service" => AttendedServiceHeaderTitle,
				"Reason For Not Attending" => ReasonForNotAttendingHeaderTitle,
				"Comment" => CommentHeaderTitle,
				"Name" => NameHeaderTitle,
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
				case "DateOfService":
					return DateOfServiceElement.DateTimePickerElement;
				case "ServiceID":
					return ServiceIDElement;
				case "SeatNoID":
					return SeatNoIDElement;
				case "Temperature":
					return TemperatureElement;
				case "AttendedService":
					return AttendedServiceElement;
				case "ReasonForNotAttending":
					return ReasonForNotAttendingElement;
				case "Comment":
					return CommentElement;
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
				case "DateOfService":
					if (DateTime.TryParse(value, out var dateOfServiceValue))
					{
						SetDateOfService(dateOfServiceValue);
					}
					break;
				case "ServiceID":
					int? serviceID = null;
					if (int.TryParse(value, out var intServiceID))
					{
						serviceID = intServiceID;
					}
					SetServiceID(serviceID);
					break;
				case "SeatNoID":
					int? seatNoID = null;
					if (int.TryParse(value, out var intSeatNoID))
					{
						seatNoID = intSeatNoID;
					}
					SetSeatNoID(seatNoID);
					break;
				case "Temperature":
					SetTemperature(Convert.ToDouble(value));
					break;
				case "AttendedService":
					SetAttendedService(bool.Parse(value));
					break;
				case "ReasonForNotAttending":
					SetReasonForNotAttending(value);
					break;
				case "Comment":
					SetComment(value);
					break;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private By GetErrorAttributeSectionAsBy(string attribute)
		{
			return attribute switch
			{
				"Name" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "//div[contains(@class, 'name')]"),
				"DateOfService" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.dateOfService > div > p"),
				"ServiceID" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.serviceID > div > p"),
				"SeatNoID" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.seatNoID > div > p"),
				"Temperature" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.temperature > div > p"),
				"AttendedService" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.attendedService > div > p"),
				"ReasonForNotAttending" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.reasonForNotAttending > div > p"),
				"Comment" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.comment > div > p"),
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
			SetName(_attendanceEntity.Name);
			SetDateOfService(_attendanceEntity.DateOfService);
			SetServiceID(_attendanceEntity.ServiceID);
			SetSeatNoID(_attendanceEntity.SeatNoID);
			SetTemperature(_attendanceEntity.Temperature);
			SetAttendedService(_attendanceEntity.AttendedService);
			SetReasonForNotAttending(_attendanceEntity.ReasonForNotAttending);
			SetComment(_attendanceEntity.Comment);

			// % protected region % [Configure entity application here] end
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations

		// get associations

		// wait for dropdown to be displaying options
		private void WaitForDropdownOptions()
		{
			var xpath = "//*/div[@aria-expanded='true']";
			var elementBy = WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, xpath);
			WaitUtils.elementState(_driverWait, elementBy,ElementState.EXISTS);
		}

		private void SetDateOfService (DateTime? value)
		{
			if (value is DateTime datetimeValue)
			{
				DateOfServiceElement.SetDate(datetimeValue);
			}
		}

		private DateTime? GetDateOfService =>
			Convert.ToDateTime(DateOfServiceElement.DateTimePickerElement.Text);
		private void SetServiceID (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "serviceID", intValue.ToString(), _isFastText);
			}
		}

		private int? GetServiceID =>
			int.Parse(ServiceIDElement.Text);

		private void SetSeatNoID (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "seatNoID", intValue.ToString(), _isFastText);
			}
		}

		private int? GetSeatNoID =>
			int.Parse(SeatNoIDElement.Text);

		private void SetTemperature (Double? value)
		{
			if (value is double doubleValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "temperature", doubleValue.ToString(), _isFastText);
			}
		}

		private Double? GetTemperature =>
			Convert.ToDouble(TemperatureElement.Text);
		private void SetAttendedService (Boolean? value)
		{
			if (value is bool boolValue)
			{
				if (AttendedServiceElement.Selected != boolValue) {
					AttendedServiceElement.Click();
				}
			}
		}

		private Boolean? GetAttendedService =>
			AttendedServiceElement.Selected;

		private void SetReasonForNotAttending (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "reasonForNotAttending", value, _isFastText);
			ReasonForNotAttendingElement.SendKeys(Keys.Tab);
			ReasonForNotAttendingElement.SendKeys(Keys.Escape);
		}

		private String GetReasonForNotAttending =>
			ReasonForNotAttendingElement.Text;

		private void SetComment (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "comment", value, _isFastText);
			CommentElement.SendKeys(Keys.Tab);
			CommentElement.SendKeys(Keys.Escape);
		}

		private String GetComment =>
			CommentElement.Text;


		// Set Name for form entity
		private void SetName (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "name", value, _isFastText);
			NameElement.SendKeys(Keys.Tab);
		}

		private String GetName => NameElement.Text;
		// % protected region % [Add any additional getters and setters of web elements] off begin
		// % protected region % [Add any additional getters and setters of web elements] end
	}
}