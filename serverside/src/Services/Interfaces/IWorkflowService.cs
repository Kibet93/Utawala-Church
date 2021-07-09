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
using System.Threading.Tasks;
using Utawalaaltar.Models;

namespace Utawalaaltar.Services.Interfaces
{
	public interface IWorkflowService
	{
		/// <summary>
		/// Gets a workflow version with it's associated states and transitions
		/// </summary>
		/// <param name="id">The Id of the version to fetch</param>
		/// <returns>A workflow version</returns>
		Task<WorkflowVersionEntity> GetWorkflowVersion(Guid id);

		/// <summary>
		/// Creates a new workflow version
		/// </summary>
		/// <param name="workflowDto">The workflow version to create</param>
		/// <returns>The created version</returns>
		Task<CreateWorkflowDto> CreateVersion(CreateWorkflowDto workflowDto);

		/// <summary>
		/// Updates a workflow version
		/// </summary>
		/// <param name="workflowDto">The workflow to update</param>
		/// <returns>The updated workflow</returns>
		Task<CreateWorkflowDto> UpdateVersion(CreateWorkflowDto workflowDto);

		/// <summary>
		/// Gets the states of Seats Entity entities
		/// </summary>
		/// <param name="ids">The ids of the Seats Entity entities</param>
		/// <returns>A dictionary of investorSeats Entity ids to the states that they are in</returns>
		Task<Dictionary<Guid, List<WorkflowStateEntityDto>>> GetSeatsEntityStates(List<Guid> ids);
	}
}