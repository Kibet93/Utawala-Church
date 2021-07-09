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

using System.Linq;
using OpenQA.Selenium;
using SeleniumTests.Setup;
using SeleniumTests.Utils;

namespace SeleniumTests.PageObjects.BotWritten.Workflows
{
	public class WorkflowDesignerPageDetails : BasePage
	{
		private IWebElement WorkflowEntitiesDropdown => FindElementExt("WorkflowEntitiesDropdown");

		public override string Url => baseUrl + "/admin/workflows/create";
		public IWebElement WorkflowNameInput => FindElementExt("WorkflowNameInput");
		public IWebElement WorkflowDescriptionInput => FindElementExt("WorkflowDescriptionInput");
		public IWebElement WorkflowSaveButton => FindElementExt("WorkflowSaveButton");
		public IWebElement WorkflowCancelButton => FindElementExt("WorkflowCancelButton");
		public IWebElement WorkflowStatesButton => FindElementExt("WorkflowStatesButton");

		public WorkflowDesignerPageDetails(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			InitializeSelectors();
		}

		private void InitializeSelectors()
		{
			selectorDict.Add("WorkflowNameInput",
				(selector: "//div[label = 'Workflow Name']/input", type: SelectorType.XPath));
			selectorDict.Add("WorkflowDescriptionInput",
				(selector: "//div[label = 'Workflow Description']/textarea", type: SelectorType.XPath));
			selectorDict.Add("WorkflowEntitiesDropdown",
				(selector: "//div[label = 'Entities']/div", type: SelectorType.XPath));
			selectorDict.Add("WorkflowSaveButton", (selector: "//button[text() = 'Save']", type: SelectorType.XPath));
			selectorDict.Add("WorkflowCancelButton",
				(selector: "//button[text() = 'Cancel']", type: SelectorType.XPath));
			selectorDict.Add("WorkflowStatesButton",
				(selector: "//button[text() = 'States']", type: SelectorType.XPath));
		}

		public bool SetEntityAssociation(string entityName)
		{
			WorkflowEntitiesDropdown.ClickWithWait(DriverWait);
			var dropdownOptions = Driver.FindElementsExt(By.XPath("//div[@role='option']//span"));
			var entityOption = dropdownOptions.FirstOrDefault(x => $"{x.Text}Entity".ToLower().Equals(entityName.ToLower()));

			if (entityOption == null)
			{
				return false;
			}
			entityOption.ClickWithWait(DriverWait);
			return true;
		}
	}
}