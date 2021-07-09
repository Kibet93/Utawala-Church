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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServersideTests.Helpers;
using ServersideTests.Helpers.EntityFactory;
using Utawalaaltar.Models;
using Utawalaaltar.Controllers;
using Utawalaaltar.Services;
using Xunit;

namespace ServersideTests.Tests.Integration.BotWritten
{
	[Trait("Category", "BotWritten")]
	[Trait("Category", "Unit")]
	public class WorkflowBehaviourTests
	{
		/// <summary>
		/// Test for the Create workflow API endpoint
		/// </summary>
		[Fact]
		public async void WorkflowVersionCreateTest()
		{
			using var host = ServerBuilder.CreateServer();

			var database = host.Services.GetRequiredService<UtawalaaltarDBContext>();
			var controller = host.Services.GetRequiredService<WorkflowController>();

			// Create the test data
			var (workflow, workflowVersion, workflowStates, workflowTransitions) = CreateTestWorkflow();

			// Save the initial workflow to the database
			database.Add(workflow);
			await database.SaveChangesAsync();

			// Use the controller to save the version and its states and transitions to the database
			await controller.CreateVersion(new CreateWorkflowDto
			{
				Version = new WorkflowVersionEntityDto(workflowVersion),
				States = workflowStates.Select(s => new WorkflowStateEntityDto(s)),
				Transitions = workflowTransitions.Select(t => new WorkflowTransitionEntityDto(t)),
			});

			// Fetch the saved data out of the database
			var newVersion = database.WorkflowVersionEntity
				.Include(v => v.Workflow)
				.Include(v => v.Statess)
				.First();

			var newStateIds = newVersion.Statess.Select(s => s.Id).ToList();

			var newTransitions = database.WorkflowTransitionEntity
				.Where(t => newStateIds.Contains(t.SourceStateId) || newStateIds.Contains(t.TargetStateId))
				.ToList();

			// Assert that the data matches
			Assert.True(newVersion != null);
			Assert.Equal(newVersion, workflowVersion, new ModelIdComparer());

			Assert.Equal(newVersion.Statess.Count, workflowStates.Count);
			Assert.Contains(newVersion.Statess, d => workflowStates.Select(s => s.Id).Contains(d.Id));

			Assert.Equal(newTransitions.Count, workflowTransitions.Count);
			Assert.Contains(newTransitions, d => workflowTransitions.Select(t => t.Id).Contains(d.Id));
		}

		/// <summary>
		/// Creates a set of testing workflow data
		/// In total this will create a workflow, a workflow version, 3 states and 3 transitions linking those states
		/// </summary>
		/// <returns>A tuple of workflow models</returns>
		private (WorkflowEntity, WorkflowVersionEntity, List<WorkflowStateEntity>, List<WorkflowTransitionEntity>) CreateTestWorkflow()
		{
			var owner = Guid.NewGuid();

			var workflow = new EntityFactory<WorkflowEntity>()
				.UseAttributes()
				.UseOwner(owner)
				.Generate()
				.First();

			// Create a new workflow version
			var workflowVersion = new EntityFactory<WorkflowVersionEntity>()
				.UseAttributes()
				.UseOwner(owner)
				.Generate()
				.First();

			workflowVersion.WorkflowId = workflow.Id;

			// Create the states for this workflow
			var workflowStates = new EntityFactory<WorkflowStateEntity>(3)
				.UseAttributes()
				.UseOwner(owner)
				.Generate()
				.ToList();

			foreach (var workflowState in workflowStates)
			{
				workflowState.WorkflowVersionId = workflowVersion.Id;
			}

			// There should only be one start state
			workflowStates.First().IsStartState = true;
			foreach (var state in workflowStates.Skip(1))
			{
				state.IsStartState = false;
			}

			// Create transitions between these states
			var workflowTransitions = new EntityFactory<WorkflowTransitionEntity>(workflowStates.Count)
				.UseAttributes()
				.UseOwner(owner)
				.Generate()
				.ToList();

			for (var i = 0; i < workflowStates.Count; i++)
			{
				workflowTransitions[i].SourceStateId = workflowStates[i].Id;
				workflowTransitions[i].TargetStateId = workflowStates[(i + 1) % workflowStates.Count].Id;
			}

			return (workflow, workflowVersion, workflowStates, workflowTransitions);
		}
	}
}