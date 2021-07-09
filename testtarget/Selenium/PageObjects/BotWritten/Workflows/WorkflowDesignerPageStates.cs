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
using SeleniumTests.Setup;
using SeleniumTests.Utils;

namespace SeleniumTests.PageObjects.BotWritten.Workflows
{
	public class WorkflowDesignerPageStates : BasePage
	{
		private WorkflowDesignerPageTransitionSidebar _transitionSidebar;
		public override string Url => baseUrl + "/admin/workflows/create";
		public IWebElement WorkflowNewStateButton => FindElementExt("WorkflowNewStateButton");
		public IWebElement WorkflowSaveButton => FindElementExt("WorkflowSaveButton");

		public WorkflowDesignerPageStates(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			InitializeSelectors();
			_transitionSidebar = new WorkflowDesignerPageTransitionSidebar(contextConfiguration);
		}

		private void InitializeSelectors()
		{
			selectorDict.Add("WorkflowNewStateButton",
				(selector: "div.workflow__new-state > button", type: SelectorType.CSS));
			selectorDict.Add("WorkflowSaveButton", 
				(selector: "//button[text() = 'Save']", type: SelectorType.XPath));
		}

		public void SetWorkflowStepName(int index, string name)
		{
			var workflowStateNameInput = Driver
				.FindElementExt(By.XPath($"//div[@class = 'workflow__states-step'][{index+1}]//input"));
			workflowStateNameInput.SendKeysWithWait(DriverWait, name);
		}

		public void SetWorkflowAsStartState(int index)
		{
			var workflowStateDeleteButton = Driver.FindElementExt(By.XPath
					($"//div[@class = 'workflow__states-step'][{index+1}]//button[contains(@class, 'workflow__delete-state')]"));
			workflowStateDeleteButton.ClickWithWait(DriverWait);
		}

		public void SetWorkflowStateTransition(int index, string transitionName, string targetName)
		{
			var workflowStateEditButton = Driver.FindElementExt(By.XPath
				($"//div[@class = 'workflow__states-step'][{index+1}]//button[contains(@class, 'workflow__edit-state')]"));
			workflowStateEditButton.ClickWithWait(DriverWait);
			_transitionSidebar.WorkflowAddTransitionButton.ClickWithWait(DriverWait);
			var workflowTransitionIndex = _transitionSidebar.GetNumberOfTransition();
			_transitionSidebar.SetTransitionName(workflowTransitionIndex, transitionName);
			_transitionSidebar.SetTransitionDestination(workflowTransitionIndex, targetName);
			_transitionSidebar.WorkflowSaveTransitionButton.ClickWithWait(DriverWait);
		}
	}
}