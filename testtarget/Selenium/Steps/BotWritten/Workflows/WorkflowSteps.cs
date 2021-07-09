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
using System.Collections.Generic;
using System.Linq;
using APITests.EntityObjects.Models;
using APITests.Factories;
using SeleniumTests.PageObjects.BotWritten.Workflows;
using SeleniumTests.PageObjects.CRUDPageObject;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using TechTalk.SpecFlow;
using Xunit;

namespace SeleniumTests.Steps.BotWritten.Workflows
{
	[Binding]
	public sealed class WorkflowSteps : BaseStepDefinition
	{
		private WorkflowLandingPage _workflowLandingPage;
		private WorkflowDesignerPageStates _workflowDesignerPageStates;
		private WorkflowDesignerPageDetails _workflowDesignerPageDetails;
		private WorkflowVersionEntity _workflowVersion;
		private ContextConfiguration _contextConfiguration;
		private List<WorkflowStateEntity> _workflowStates;
		private List<WorkflowTransitionEntity> _workflowTransitions;
		private CrudGenericEntityPage _crudGenericEntityPage;
		private const int NumStates = 3;

		public WorkflowSteps(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
			_workflowLandingPage = new WorkflowLandingPage(contextConfiguration);
			_workflowDesignerPageDetails = new WorkflowDesignerPageDetails(contextConfiguration);
			_workflowDesignerPageStates = new WorkflowDesignerPageStates(contextConfiguration);
			_crudGenericEntityPage = new CrudGenericEntityPage(contextConfiguration);
			_workflowVersion = new WorkflowVersionEntity(BaseEntity.ConfigureOptions.CREATE_ATTRIBUTES_ONLY);
			_workflowVersion.WorkflowName = Guid.NewGuid().ToString();
			_workflowStates = new List<WorkflowStateEntity>();
			_workflowTransitions = new List<WorkflowTransitionEntity>();
		}

		[StepDefinition(@"I create a new workflow linked to (.*)s")]
		public void ICreateANewWorkflowLinkedToWorkflowEntity(string entity)
		{

			// navigate to the create workflow page
			_workflowLandingPage.Navigate();
			_workflowLandingPage.NewWorkflowButton.ClickWithWait(_driverWait);

			// fill out the details of the workflow
			_workflowDesignerPageDetails.WorkflowNameInput
				.SendKeysWithWait(_driverWait, _workflowVersion.WorkflowName);
			_workflowDesignerPageDetails.SetEntityAssociation(entity);
			_workflowDesignerPageDetails.WorkflowStatesButton.ClickWithWait(_driverWait);

			// create workflow states
			foreach (var index in Enumerable.Range(0, NumStates))
			{
				_workflowStates.Add(new WorkflowStateEntity(BaseEntity.ConfigureOptions.CREATE_ATTRIBUTES_ONLY));
				_workflowStates[index].StepName = Guid.NewGuid().ToString();
				_workflowStates[index].IsStartState = index == 0;
				_workflowDesignerPageStates.WorkflowNewStateButton.ClickWithWait(_driverWait);
				_workflowDesignerPageStates.SetWorkflowStepName(index, _workflowStates[index].StepName);
			}

			// create workflow transitions
			foreach (var index in Enumerable.Range(0, NumStates))
			{
				_workflowTransitions.Add(new WorkflowTransitionEntity(BaseEntity.ConfigureOptions.CREATE_ATTRIBUTES_ONLY));
				_workflowTransitions[index].SourceStateId = _workflowStates[index].Id;
				_workflowTransitions[index].TargetStateId = _workflowStates[index + 1 < 3 ? index + 1 : 0].Id;
				_workflowDesignerPageStates.SetWorkflowStateTransition(index,
						_workflowTransitions[index].TransitionName,
						_workflowStates.First(s => s.Id == _workflowTransitions[index].TargetStateId).StepName);
			}

			// save and check for redirect
			_workflowDesignerPageStates.WorkflowSaveButton.ClickWithWait(_driverWait);
			_driverWait.Until(driver => driver.Url == _workflowLandingPage.Url);
		}

		//I expect to be able to view and modify a Investors workflow state
		[StepDefinition(@"I expect to be able to view and modify a (.*)s workflow state")]
		public void ExpectToViewAndModifyAWorkflowEntityWorkflowState(string entityName)
		{
			// create a new instance of the entity with workflow behaviour
			var entityFactory = new EntityFactory(entityName);
			var entity = entityFactory.ConstructAndSave(_testOutputHelper);
			var workflowEntityDetailsSection =
				EntityDetailUtils.GetWorkflowEntityDetailsSection(entityName, _contextConfiguration);

			// loop through each workflow transition, updating the current state of the entity and
			// asserting that only the states defined in outgoing transitions is available to select on
			// the entity crud page.
			foreach (var index in Enumerable.Range(0, NumStates))
			{
				_driver.Navigate()
					.GoToUrl(_baseUrl + $"/admin/{entityName.ToLower()}/edit/{entity.Id}");

				var workflowElement = workflowEntityDetailsSection.GetWorkflowElement(_workflowVersion.WorkflowName);
				var currentWorkflowState = workflowEntityDetailsSection.GetCurrentStateOfWorkflow(workflowElement);
				Assert.Equal(_workflowStates[index].StepName, currentWorkflowState);
				var workflowStateOptions = workflowEntityDetailsSection.GetWorkflowStateOptions(workflowElement);

				var expectedWorkflowStateOptions = _workflowTransitions
					.Where(x => x.SourceStateId == _workflowStates[index].Id)
					.Select(x => x.TransitionName)
					.Append(_workflowStates[index].StepName)
					.ToList();
				Assert.Equal(expectedWorkflowStateOptions.OrderBy(x => x), workflowStateOptions.OrderBy(x => x));
				var nextState = expectedWorkflowStateOptions.First(x => x != _workflowStates[index].StepName);
				workflowEntityDetailsSection.SetWorkflowState(workflowElement, nextState);
				_crudGenericEntityPage.SubmitButton.ClickWithWait(_driverWait);
				_driverWait.Until(driver =>
					driver.Url.Equals(_baseUrl + $"/admin/workflows"));
			}
		}
	}
}