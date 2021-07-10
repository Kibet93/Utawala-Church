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
using System.IO;
using System.Collections.Generic;
using EntityObject.Enums;
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
	public class MembersEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements
		private static By AccountabilityGroupIdElementBy => By.XPath("//*[contains(@class, 'accountabilityGroup')]//div[contains(@class, 'dropdown__container')]");
		private static By AccountabilityGroupIdInputElementBy => By.XPath("//*[contains(@class, 'accountabilityGroup')]/div/input");
		private static By HomeFellowshipIdElementBy => By.XPath("//*[contains(@class, 'homeFellowship')]//div[contains(@class, 'dropdown__container')]");
		private static By HomeFellowshipIdInputElementBy => By.XPath("//*[contains(@class, 'homeFellowship')]/div/input");
		private static By CategoryGroupLeaderIdElementBy => By.XPath("//*[contains(@class, 'categoryGroupLeader')]//div[contains(@class, 'dropdown__container')]");
		private static By CategoryGroupLeaderIdInputElementBy => By.XPath("//*[contains(@class, 'categoryGroupLeader')]/div/input");
		private static By ProtocolIdElementBy => By.XPath("//*[contains(@class, 'protocol')]//div[contains(@class, 'dropdown__container')]");
		private static By ProtocolIdInputElementBy => By.XPath("//*[contains(@class, 'protocol')]/div/input");
		private static By UshersIdElementBy => By.XPath("//*[contains(@class, 'ushers')]//div[contains(@class, 'dropdown__container')]");
		private static By UshersIdInputElementBy => By.XPath("//*[contains(@class, 'ushers')]/div/input");

		//FlatPickr Elements
		private DateTimePickerComponent DateOfBirthElement => new DateTimePickerComponent(_contextConfiguration, "dateOfBirth");

		//Attribute Headers
		private readonly MembersEntity _membersEntity;

		//Attribute Header Titles
		private IWebElement FullNameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Full Name']"));
		private IWebElement NationalIDHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='National ID']"));
		private IWebElement ResidenceHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Residence']"));
		private IWebElement DateOfBirthHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Date of Birth']"));
		private IWebElement AgeHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Age']"));
		private IWebElement StatusHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Status']"));
		private IWebElement MembershipStatusHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Membership Status']"));
		private IWebElement CategoryChoiceHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Category Choice']"));
		private IWebElement AccountabilityGrpHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Accountability Grp']"));
		private IWebElement PictureHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Picture']"));

		// User Entity specific web Elements
		private IWebElement UserEmailElement => FindElementExt("UserEmailElement");
		private IWebElement UserPasswordElement => FindElementExt("UserPasswordElement");
		private IWebElement UserConfirmPasswordElement => FindElementExt("UserConfirmPasswordElement");
		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public MembersEntityDetailSection(ContextConfiguration contextConfiguration, MembersEntity membersEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_membersEntity = membersEntity;

			InitializeSelectors();
			// % protected region % [Add any extra construction requires] off begin

			// % protected region % [Add any extra construction requires] end
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements
			selectorDict.Add("FullNameElement", (selector: "//div[contains(@class, 'fullName')]//input", type: SelectorType.XPath));
			selectorDict.Add("NationalIDElement", (selector: "//div[contains(@class, 'nationalID')]//input", type: SelectorType.XPath));
			selectorDict.Add("ResidenceElement", (selector: "//div[contains(@class, 'residence')]//input", type: SelectorType.XPath));
			selectorDict.Add("AgeElement", (selector: "//div[contains(@class, 'age')]//input", type: SelectorType.XPath));
			selectorDict.Add("StatusElement", (selector: "//div[contains(@class, 'status')]//input", type: SelectorType.XPath));
			selectorDict.Add("MembershipStatusElement", (selector: "//div[contains(@class, 'membershipStatus')]//input", type: SelectorType.XPath));
			selectorDict.Add("CategoryChoiceElement", (selector: "//div[contains(@class, 'categoryChoice')]//input", type: SelectorType.XPath));
			selectorDict.Add("AccountabilityGrpElement", (selector: "//div[contains(@class, 'accountabilityGrp')]//input", type: SelectorType.XPath));
			selectorDict.Add("PictureElement", (selector: "//div[contains(@class, 'picture')]//input", type: SelectorType.XPath));

			// Reference web elements
			selectorDict.Add("AccountabilitygroupElement", (selector: ".input-group__dropdown.accountabilityGroupId > .dropdown.dropdown__container", type: SelectorType.CSS));
			selectorDict.Add("HomefellowshipElement", (selector: ".input-group__dropdown.homeFellowshipId > .dropdown.dropdown__container", type: SelectorType.CSS));
			selectorDict.Add("CategorygroupleaderElement", (selector: ".input-group__displayfield.categoryGroupLeader > input", type: SelectorType.CSS));
			selectorDict.Add("ProtocolElement", (selector: ".input-group__displayfield.protocol > input", type: SelectorType.CSS));
			selectorDict.Add("UshersElement", (selector: ".input-group__displayfield.ushers > input", type: SelectorType.CSS));

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
		private IWebElement AccountabilityGroupElement => FindElementExt("AccountabilityGroupElement");
		//get the input path as set by the selector library
		private IWebElement HomeFellowshipElement => FindElementExt("HomeFellowshipElement");

		//Attribute web Elements
		private IWebElement FullNameElement => FindElementExt("FullNameElement");
		private IWebElement NationalIDElement => FindElementExt("NationalIDElement");
		private IWebElement ResidenceElement => FindElementExt("ResidenceElement");
		private IWebElement AgeElement => FindElementExt("AgeElement");
		private IWebElement StatusElement => FindElementExt("StatusElement");
		private IWebElement MembershipStatusElement => FindElementExt("MembershipStatusElement");
		private IWebElement CategoryChoiceElement => FindElementExt("CategoryChoiceElement");
		private IWebElement AccountabilityGrpElement => FindElementExt("AccountabilityGrpElement");
		private IWebElement PictureElement => FindElementExt("PictureElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"Full Name" => FullNameHeaderTitle,
				"National ID" => NationalIDHeaderTitle,
				"Residence" => ResidenceHeaderTitle,
				"Date of Birth" => DateOfBirthHeaderTitle,
				"Age" => AgeHeaderTitle,
				"Status" => StatusHeaderTitle,
				"Membership Status" => MembershipStatusHeaderTitle,
				"Category Choice" => CategoryChoiceHeaderTitle,
				"Accountability Grp" => AccountabilityGrpHeaderTitle,
				"Picture" => PictureHeaderTitle,
				_ => throw new Exception($"Cannot find header tile {attribute}"),
			};
		}

		// Return an IWebElement for an attribute input
		public IWebElement GetInputElement(string attribute)
		{
			switch (attribute)
			{
				case "FullName":
					return FullNameElement;
				case "NationalID":
					return NationalIDElement;
				case "Residence":
					return ResidenceElement;
				case "DateOfBirth":
					return DateOfBirthElement.DateTimePickerElement;
				case "Age":
					return AgeElement;
				case "Status":
					return StatusElement;
				case "MembershipStatus":
					return MembershipStatusElement;
				case "CategoryChoice":
					return CategoryChoiceElement;
				case "AccountabilityGrp":
					return AccountabilityGrpElement;
				case "Picture":
					return PictureElement;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		public void SetInputElement(string attribute, string value)
		{
			switch (attribute)
			{
				case "FullName":
					SetFullName(value);
					break;
				case "NationalID":
					SetNationalID(value);
					break;
				case "Residence":
					SetResidence(value);
					break;
				case "DateOfBirth":
					if (DateTime.TryParse(value, out var dateOfBirthValue))
					{
						SetDateOfBirth(dateOfBirthValue);
					}
					break;
				case "Age":
					int? age = null;
					if (int.TryParse(value, out var intAge))
					{
						age = intAge;
					}
					SetAge(age);
					break;
				case "Status":
					SetStatus((Status)Enum.Parse(typeof(Status), value));
					break;
				case "MembershipStatus":
					SetMembershipStatus((Membershipstatus)Enum.Parse(typeof(Membershipstatus), value));
					break;
				case "CategoryChoice":
					SetCategoryChoice((CategoryGroups)Enum.Parse(typeof(CategoryGroups), value));
					break;
				case "AccountabilityGrp":
					int? accountabilityGrp = null;
					if (int.TryParse(value, out var intAccountabilityGrp))
					{
						accountabilityGrp = intAccountabilityGrp;
					}
					SetAccountabilityGrp(accountabilityGrp);
					break;
				case "Picture":
					SetPicture(value);
					break;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private By GetErrorAttributeSectionAsBy(string attribute)
		{
			return attribute switch
			{
				"FullName" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.fullName > div > p"),
				"NationalID" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.nationalID > div > p"),
				"Residence" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.residence > div > p"),
				"DateOfBirth" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.dateOfBirth > div > p"),
				"Age" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.age > div > p"),
				"Status" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.status > div > p"),
				"MembershipStatus" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.membershipStatus > div > p"),
				"CategoryChoice" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.categoryChoice > div > p"),
				"AccountabilityGrp" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.accountabilityGrp > div > p"),
				"Picture" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.picture > div > p"),
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
			SetFullName(_membersEntity.FullName);
			SetNationalID(_membersEntity.NationalID);
			SetResidence(_membersEntity.Residence);
			SetDateOfBirth(_membersEntity.DateOfBirth);
			SetAge(_membersEntity.Age);
			SetStatus(_membersEntity.Status);
			SetMembershipStatus(_membersEntity.MembershipStatus);
			SetCategoryChoice(_membersEntity.CategoryChoice);
			SetAccountabilityGrp(_membersEntity.AccountabilityGrp);
			SetPicture(_membersEntity.PictureId.ToString());

			SetAccountabilityGroupId(_membersEntity.AccountabilityGroupId?.ToString());
			SetHomeFellowshipId(_membersEntity.HomeFellowshipId?.ToString());

			if (_driver.Url == $"{_contextConfiguration.BaseUrl}/admin/membersentity/create")
			{
				SetUserFields(_membersEntity);
			}
			// % protected region % [Configure entity application here] end
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "accountabilitygroup":
					return new List<Guid>() {GetAccountabilityGroupId()};
				case "homefellowship":
					return new List<Guid>() {GetHomeFellowshipId()};
				case "categorygroupleader":
					return new List<Guid>() {GetCategoryGroupLeaderId()};
				case "protocol":
					return new List<Guid>() {GetProtocolId()};
				case "ushers":
					return new List<Guid>() {GetUshersId()};
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations
		private void SetAccountabilityGroupId(string id)
		{
			if (id == "") { return; }
			WaitUtils.elementState(_driverWait, AccountabilityGroupIdInputElementBy, ElementState.VISIBLE);
			var accountabilityGroupIdInputElement = _driver.FindElementExt(AccountabilityGroupIdInputElementBy);

			if (id != null)
			{
				accountabilityGroupIdInputElement.SendKeys(id);
				WaitForDropdownOptions();
				WaitUtils.elementState(_driverWait, By.XPath($"//*/div[@role='option']/span[text()='{id}']"), ElementState.EXISTS);
				accountabilityGroupIdInputElement.SendKeys(Keys.Return);
			}
		}
		private void SetHomeFellowshipId(string id)
		{
			if (id == "") { return; }
			WaitUtils.elementState(_driverWait, HomeFellowshipIdInputElementBy, ElementState.VISIBLE);
			var homeFellowshipIdInputElement = _driver.FindElementExt(HomeFellowshipIdInputElementBy);

			if (id != null)
			{
				homeFellowshipIdInputElement.SendKeys(id);
				WaitForDropdownOptions();
				WaitUtils.elementState(_driverWait, By.XPath($"//*/div[@role='option']/span[text()='{id}']"), ElementState.EXISTS);
				homeFellowshipIdInputElement.SendKeys(Keys.Return);
			}
		}

		// get associations
		private Guid GetAccountabilityGroupId()
		{
			WaitUtils.elementState(_driverWait, AccountabilityGroupIdElementBy, ElementState.VISIBLE);
			var accountabilityGroupIdElement = _driver.FindElementExt(AccountabilityGroupIdElementBy);
			return new Guid(accountabilityGroupIdElement.GetAttribute("data-id"));
		}
		private Guid GetHomeFellowshipId()
		{
			WaitUtils.elementState(_driverWait, HomeFellowshipIdElementBy, ElementState.VISIBLE);
			var homeFellowshipIdElement = _driver.FindElementExt(HomeFellowshipIdElementBy);
			return new Guid(homeFellowshipIdElement.GetAttribute("data-id"));
		}
		private Guid GetCategoryGroupLeaderId()
		{
			WaitUtils.elementState(_driverWait, GetWebElementBy("CategoryGroupLeaderElement"), ElementState.VISIBLE);
			var elementId = FindElementExt("CategoryGroupLeaderElement");
			var dataId = elementId.GetAttribute("value");
			return new Guid(dataId);
		}
		private Guid GetProtocolId()
		{
			WaitUtils.elementState(_driverWait, GetWebElementBy("ProtocolElement"), ElementState.VISIBLE);
			var elementId = FindElementExt("ProtocolElement");
			var dataId = elementId.GetAttribute("value");
			return new Guid(dataId);
		}
		private Guid GetUshersId()
		{
			WaitUtils.elementState(_driverWait, GetWebElementBy("UshersElement"), ElementState.VISIBLE);
			var elementId = FindElementExt("UshersElement");
			var dataId = elementId.GetAttribute("value");
			return new Guid(dataId);
		}

		// wait for dropdown to be displaying options
		private void WaitForDropdownOptions()
		{
			var xpath = "//*/div[@aria-expanded='true']";
			var elementBy = WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, xpath);
			WaitUtils.elementState(_driverWait, elementBy,ElementState.EXISTS);
		}

		private void SetFullName (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "fullName", value, _isFastText);
			FullNameElement.SendKeys(Keys.Tab);
			FullNameElement.SendKeys(Keys.Escape);
		}

		private String GetFullName =>
			FullNameElement.Text;

		private void SetNationalID (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "nationalID", value, _isFastText);
			NationalIDElement.SendKeys(Keys.Tab);
			NationalIDElement.SendKeys(Keys.Escape);
		}

		private String GetNationalID =>
			NationalIDElement.Text;

		private void SetResidence (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "residence", value, _isFastText);
			ResidenceElement.SendKeys(Keys.Tab);
			ResidenceElement.SendKeys(Keys.Escape);
		}

		private String GetResidence =>
			ResidenceElement.Text;

		private void SetDateOfBirth (DateTime? value)
		{
			if (value is DateTime datetimeValue)
			{
				DateOfBirthElement.SetDate(datetimeValue);
			}
		}

		private DateTime? GetDateOfBirth =>
			Convert.ToDateTime(DateOfBirthElement.DateTimePickerElement.Text);
		private void SetAge (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "age", intValue.ToString(), _isFastText);
			}
		}

		private int? GetAge =>
			int.Parse(AgeElement.Text);

		private void SetStatus (Status value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "status", value.ToString(), _isFastText);
		}

		private Status GetStatus =>
			(Status)Enum.Parse(typeof(Status), StatusElement.Text);
		private void SetMembershipStatus (Membershipstatus value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "membershipStatus", value.ToString(), _isFastText);
		}

		private Membershipstatus GetMembershipStatus =>
			(Membershipstatus)Enum.Parse(typeof(Membershipstatus), MembershipStatusElement.Text);
		private void SetCategoryChoice (CategoryGroups value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "categoryChoice", value.ToString(), _isFastText);
		}

		private CategoryGroups GetCategoryChoice =>
			(CategoryGroups)Enum.Parse(typeof(CategoryGroups), CategoryChoiceElement.Text);
		private void SetAccountabilityGrp (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "accountabilityGrp", intValue.ToString(), _isFastText);
			}
		}

		private int? GetAccountabilityGrp =>
			int.Parse(AccountabilityGrpElement.Text);

		private void SetPicture (String value)
		{
			const string script = "document.querySelector('.pictureId>div>input').removeAttribute('style')";
			var js = (IJavaScriptExecutor)Driver;
			js.ExecuteScript(script);
			var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Resources/RedCircle.svg"));
			var fileUploadElement = Driver.FindElementExt(By.CssSelector(".pictureId>div>input"));
			fileUploadElement.SendKeys(path);
		}

		private String GetPicture =>
			PictureElement.Text;

		// set the email, password and confirm password fields
		private void SetUserFields(MembersEntity membersEntity)
		{
			UserEmailElement.SendKeys(membersEntity.EmailAddress);
			UserPasswordElement.SendKeys(membersEntity.Password);
			UserConfirmPasswordElement.SendKeys(membersEntity.Password);
		}

		// % protected region % [Add any additional getters and setters of web elements] off begin
		// % protected region % [Add any additional getters and setters of web elements] end
	}
}