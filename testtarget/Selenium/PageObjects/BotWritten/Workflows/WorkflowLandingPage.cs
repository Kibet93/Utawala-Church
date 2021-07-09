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

using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using SeleniumTests.Setup;

namespace SeleniumTests.PageObjects.BotWritten.Workflows
{
	public class WorkflowLandingPage : BasePage
	{
		public override string Url => baseUrl + "/admin/workflows";
		public IWebElement NewWorkflowButton => FindElementExt("NewWorkflowButton");

		public WorkflowLandingPage(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			InitializeSelectors();
		}
		private void InitializeSelectors()
		{
			selectorDict.Add("NewWorkflowButton", (selector: "div.workflow-item__new", type: SelectorType.CSS));
		}

		public void ClickWorkflowItemWithWait(string workflowInstanceName)
		{
			var success = DriverWait.Until(_ => ClickWorkflowItem(workflowInstanceName));
			ContextConfiguration.TestOutputHelper.WriteLine(success
				? $"Successfully found and clicked workflow item {workflowInstanceName}"
				: $"Failed to find and click workflow item {workflowInstanceName}");
		}
		private IEnumerable<IWebElement> WorkflowItems() =>
			Driver.FindElements(By.CssSelector("div.workflow-item__heading"));
		private bool ClickWorkflowItem(string formInstanceName)
		{

			var formItem = WorkflowItems()
				.FirstOrDefault(x => x.FindElement(By.CssSelector("h3")).Text == formInstanceName);
			if (formItem == null)
			{
				return false;
			}
			formItem.Click();
			return true;
		}
	}
}