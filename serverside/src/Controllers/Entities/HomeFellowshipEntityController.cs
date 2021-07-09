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
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Utawalaaltar.Helpers;
using Utawalaaltar.Models;
using Utawalaaltar.Services;
using Utawalaaltar.Services.Interfaces;
using GraphQL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

namespace Utawalaaltar.Controllers.Entities
{
	/// <summary>
	/// The controller that provides rest endpoints for the HomeFellowshipEntity model
	/// </summary>
	// % protected region % [Override controller attributes here] off begin
	[Route("/api/entity/HomeFellowshipEntity")]
	[Authorize]
	[ApiController]
	// % protected region % [Override controller attributes here] end
	public class HomeFellowshipEntityController : BaseApiController
	{
		private readonly ICrudService _crudService;
		// % protected region % [Add any extra class variables here] off begin
		// % protected region % [Add any extra class variables here] end

		public HomeFellowshipEntityController(
			// % protected region % [Add any extra constructor arguments here] off begin
			// % protected region % [Add any extra constructor arguments here] end
			ICrudService crudService)
		{
			_crudService = crudService;
			// % protected region % [Add any extra constructor logic here] off begin
			// % protected region % [Add any extra constructor logic here] end
		}

		/// <summary>
		/// Get the HomeFellowshipEntity for the given id
		/// </summary>
		/// <param name="id">The id of the HomeFellowshipEntity to be fetched</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>The HomeFellowshipEntity object with the given id</returns>
		// % protected region % [Override get attributes here] off begin
		[HttpGet]
		[Route("{id}")]
		[Authorize]
		// % protected region % [Override get attributes here] end
		public async Task<HomeFellowshipEntityDto> Get(Guid id, CancellationToken cancellation)
		{
			// % protected region % [Override Get by id here] off begin
			var result = _crudService.GetById<HomeFellowshipEntity>(id);
			return await result
				.Select(model => new HomeFellowshipEntityDto(model))
				.AsNoTracking()
				.FirstOrDefaultAsync(cancellation);
			// % protected region % [Override Get by id here] end
		}

		/// <summary>
		/// Gets all HomeFellowshipEntitys with pagination support
		/// </summary>
		/// <param name="options">Filtering params</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A list of HomeFellowshipEntitys</returns>
		// % protected region % [Override get list attributes here] off begin
		[HttpGet]
		[Route("")]
		[Authorize]
		// % protected region % [Override get list attributes here] end
		public async Task<EntityControllerData<HomeFellowshipEntityDto>> Get(
			[FromQuery]HomeFellowshipEntityOptions options,
			CancellationToken cancellation)
		{
			// % protected region % [Override Get here] off begin
			return new EntityControllerData<HomeFellowshipEntityDto>
			{
				Data = await _crudService.Get<HomeFellowshipEntity>(new Pagination(options))
					.AsNoTracking()
					.Select(model => new HomeFellowshipEntityDto(model))
					.ToListAsync(cancellation),
				Count = await _crudService.Get<HomeFellowshipEntity>()
					.AsNoTracking()
					.CountAsync(cancellation)
			};
			// % protected region % [Override Get here] end
		}

		/// <summary>
		/// Create HomeFellowshipEntity
		/// </summary>
		/// <param name="model">The new HomeFellowshipEntity to be created</param>
		/// <param name="cancellation">The cancellation token for this operation</param>
		/// <returns>The HomeFellowshipEntity object after creation</returns>
		// % protected region % [Override post attributes here] off begin
		[HttpPost]
		[Route("")]
		[Consumes("application/json")]
		[Authorize]
		// % protected region % [Override post attributes here] end
		public async Task<HomeFellowshipEntityDto> Post(
			[BindRequired, FromBody] HomeFellowshipEntityDto model,
			CancellationToken cancellation)
		{
			// % protected region % [Override Post here] off begin
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new HomeFellowshipEntityDto(await _crudService.Create(model.ToModel(), cancellation: cancellation));
			// % protected region % [Override Post here] end
		}

		/// <summary>
		/// Create HomeFellowshipEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The HomeFellowshipEntity object after creation</returns>
		// % protected region % [Override post form attributes here] off begin
		[HttpPost]
		[Route("")]
		[Consumes("multipart/form-data")]
		[Authorize]
		// % protected region % [Override post form attributes here] end
		public async Task<HomeFellowshipEntityDto> PostForm(CancellationToken cancellation)
		{
			// % protected region % [Override Post form here] off begin
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<HomeFellowshipEntityDto>(variables.First());
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			var result = await _crudService.Create(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation);

			return new HomeFellowshipEntityDto(result);
			// % protected region % [Override Post form here] end
		}

		/// <summary>
		/// Update an HomeFellowshipEntity
		/// </summary>
		/// <param name="model">The HomeFellowshipEntity to be updated</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The HomeFellowshipEntity object after it has been updated</returns>
		// % protected region % [Override put attributes here] off begin
		[HttpPut]
		[Consumes("application/json")]
		[Authorize]
		// % protected region % [Override put attributes here] end
		public async Task<HomeFellowshipEntityDto> Put(
			[BindRequired, FromBody] HomeFellowshipEntityDto model,
			CancellationToken cancellation)
		{
			// % protected region % [Override Put here] off begin
			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new HomeFellowshipEntityDto(await _crudService.Update(model.ToModel(), cancellation: cancellation));
			// % protected region % [Override Put here] end
		}

		/// <summary>
		/// Update an HomeFellowshipEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The HomeFellowshipEntity object after it has been updated</returns>
		// % protected region % [Override put form attributes here] off begin
		[HttpPut]
		[Consumes("multipart/form-data")]
		[Authorize]
		// % protected region % [Override put form attributes here] end
		public async Task<HomeFellowshipEntityDto> PutForm(CancellationToken cancellation)
		{
			// % protected region % [Override Put form here] off begin
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<HomeFellowshipEntityDto>(variables.First());

			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new HomeFellowshipEntityDto(await _crudService.Update(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation));
			// % protected region % [Override Put form here] end
		}

		/// <summary>
		/// Deletes a HomeFellowshipEntity
		/// </summary>
		/// <param name="id">The id of the HomeFellowshipEntity to delete</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The ids of the deleted HomeFellowshipEntitys</returns>
		// % protected region % [Override delete attributes here] off begin
		[HttpDelete]
		[Route("{id}")]
		[Authorize]
		// % protected region % [Override delete attributes here] end
		public async Task<Guid> Delete(Guid id, CancellationToken cancellation)
		{
			// % protected region % [Override Delete here] off begin
			return await _crudService.Delete<HomeFellowshipEntity>(id, cancellation: cancellation);
			// % protected region % [Override Delete here] end
		}

		/// <summary>
		/// Export the list of Home Fellowships with given the provided conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Home Fellowships</returns>
		// % protected region % [Override export attributes here] off begin
		[HttpGet]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		// % protected region % [Override export attributes here] end
		public async Task Export(
			[FromQuery]IEnumerable<WhereExpression> conditions,
			CancellationToken cancellationToken)
		{
			// % protected region % [Override Export here] off begin
			var queryable = _crudService.Get<HomeFellowshipEntity>()
				.AsNoTracking()
				.AddWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new HomeFellowshipEntityDto(r)),
				"export_home_fellowship.csv",
				cancellationToken);
			// % protected region % [Override Export here] end
		}

		/// <summary>
		/// Export a list of Home Fellowships with given the provided conditions
		/// This is a post endpoint for easier composition of complex conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Home Fellowships</returns>
		// % protected region % [Override export post attributes here] off begin
		[HttpPost]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		// % protected region % [Override export post attributes here] end
		public async Task ExportPost(
			[FromBody]IEnumerable<IEnumerable<WhereExpression>> conditions,
			CancellationToken cancellationToken)
		{
			// % protected region % [Override ExportPost here] off begin
			var queryable = _crudService.Get<HomeFellowshipEntity>()
				.AsNoTracking()
				.AddConditionalWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new HomeFellowshipEntityDto(r)),
				"export_home_fellowship.csv",
				cancellationToken);
			// % protected region % [Override ExportPost here] end
		}

		public class HomeFellowshipEntityOptions : PaginationOptions
		{
			// % protected region % [Add any get params here] off begin
			// % protected region % [Add any get params here] end
		}

		// % protected region % [Add any further endpoints here] off begin
		// % protected region % [Add any further endpoints here] end
	}
}

