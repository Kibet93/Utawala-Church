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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utawalaaltar.Models;
using Utawalaaltar.Services;
using Utawalaaltar.Services.Interfaces;

namespace Utawalaaltar.Controllers
{
	[ApiController]
	[Route("/api/behaviours/workflow")]
	[Authorize(Policy = "AllowVisitorPolicy")]
	public class WorkflowController : Controller
	{
		private readonly IWorkflowService _workflowService;

		public WorkflowController(IWorkflowService workflowService)
		{
			_workflowService = workflowService;
		}

		/// <summary>
		/// Creates a new workflow version
		/// </summary>
		/// <param name="workflowDto">The workflow version to create</param>
		/// <returns>The created version</returns>
		[HttpPost]
		public async Task<CreateWorkflowDto> CreateVersion(CreateWorkflowDto workflowDto)
		{
			return await _workflowService.CreateVersion(workflowDto);
		}

		/// <summary>
		/// Updates a workflow version
		/// </summary>
		/// <param name="workflowDto">The workflow to update</param>
		/// <returns>The updated workflow</returns>
		[HttpPut]
		public async Task<CreateWorkflowDto> UpdateVersion(CreateWorkflowDto workflowDto)
		{
			return await _workflowService.UpdateVersion(workflowDto);
		}

		/// <summary>
		/// Gets the workflow states that the Seats Entity entity is in
		/// </summary>
		/// <param name="id"></param>
		/// <returns>A list of workflow states that this entity is in</returns>
		[HttpGet]
		[Route("SeatsEntity/{id?}")]
		public async Task<IEnumerable<WorkflowStateEntityDto>> GetSeatsEntityStates(Guid? id)
		{
			return (await _workflowService.GetSeatsEntityStates(new List<Guid> {id ?? Guid.Empty})).First().Value;
		}
	}
}