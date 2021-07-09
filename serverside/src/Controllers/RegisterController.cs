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
using System.Threading.Tasks;
using Utawalaaltar.Exceptions;
using Utawalaaltar.Models;
using Utawalaaltar.Models.RegistrationModels;
using Utawalaaltar.Services;
using Utawalaaltar.Services.Interfaces;
using Utawalaaltar.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

namespace Utawalaaltar.Controllers
{
	[ApiController]
	[Authorize]
	[Route("/api/register")]
	public class RegisterController : Controller
	{
		private readonly IUserService _userService;
		private readonly ILogger<RegisterController> _logger;

		public RegisterController(
			IUserService userService,
			ILogger<RegisterController> logger)
		{
			_userService = userService;
			_logger = logger;
		}

		// % protected region % [The default register function for Category Group Leader Entity] off begin
		/// <summary>
		/// Registers a new Category Group Leader Entity user
		/// </summary>
		/// <param name="registrationModel">The fields to set on the user</param>
		/// <returns>A user result on success or a list of errors on failure</returns>
		[HttpPost]
		[Route("category-group-leader-entity")]
		[Authorize(Roles = "Visitors,Admin,Member,CategoryGroupLeader,Usher,Protocol,GroupCategory,Super Administrators")]
		public async Task<IActionResult> RegisterCategoryGroupLeaderEntity([FromBody] CategoryGroupLeaderEntityRegistrationModel registrationModel)
		{
			var model = registrationModel.ToModel();
			return await Register(model, registrationModel.Password, registrationModel.Groups);
		}
		// % protected region % [The default register function for Category Group Leader Entity] end

		// % protected region % [The default register function for Group Category Entity] off begin
		/// <summary>
		/// Registers a new Group Category Entity user
		/// </summary>
		/// <param name="registrationModel">The fields to set on the user</param>
		/// <returns>A user result on success or a list of errors on failure</returns>
		[HttpPost]
		[Route("group-category-entity")]
		[Authorize(Roles = "Visitors,Admin,Member,CategoryGroupLeader,Usher,Protocol,GroupCategory,Super Administrators")]
		public async Task<IActionResult> RegisterGroupCategoryEntity([FromBody] GroupCategoryEntityRegistrationModel registrationModel)
		{
			var model = registrationModel.ToModel();
			return await Register(model, registrationModel.Password, registrationModel.Groups);
		}
		// % protected region % [The default register function for Group Category Entity] end

		// % protected region % [The default register function for MEMBER Entity] off begin
		/// <summary>
		/// Registers a new MEMBER Entity user
		/// </summary>
		/// <param name="registrationModel">The fields to set on the user</param>
		/// <returns>A user result on success or a list of errors on failure</returns>
		[HttpPost]
		[Route("member-entity")]
		[Authorize(Roles = "Visitors,Admin,Member,CategoryGroupLeader,Usher,Protocol,GroupCategory,Super Administrators")]
		public async Task<IActionResult> RegisterMemberEntity([FromBody] MemberEntityRegistrationModel registrationModel)
		{
			var model = registrationModel.ToModel();
			return await Register(model, registrationModel.Password, registrationModel.Groups);
		}
		// % protected region % [The default register function for MEMBER Entity] end

		// % protected region % [The default register function for Admin Entity] off begin
		/// <summary>
		/// Registers a new Admin Entity user
		/// </summary>
		/// <param name="registrationModel">The fields to set on the user</param>
		/// <returns>A user result on success or a list of errors on failure</returns>
		[HttpPost]
		[Route("admin-entity")]
		[Authorize(Roles = "Visitors,Admin,Member,CategoryGroupLeader,Usher,Protocol,GroupCategory,Super Administrators")]
		public async Task<IActionResult> RegisterAdminEntity([FromBody] AdminEntityRegistrationModel registrationModel)
		{
			var model = registrationModel.ToModel();
			return await Register(model, registrationModel.Password, registrationModel.Groups);
		}
		// % protected region % [The default register function for Admin Entity] end

		// % protected region % [The default register function for Protocol Entity] off begin
		/// <summary>
		/// Registers a new Protocol Entity user
		/// </summary>
		/// <param name="registrationModel">The fields to set on the user</param>
		/// <returns>A user result on success or a list of errors on failure</returns>
		[HttpPost]
		[Route("protocol-entity")]
		[Authorize(Roles = "Visitors,Admin,Member,CategoryGroupLeader,Usher,Protocol,GroupCategory,Super Administrators")]
		public async Task<IActionResult> RegisterProtocolEntity([FromBody] ProtocolEntityRegistrationModel registrationModel)
		{
			var model = registrationModel.ToModel();
			return await Register(model, registrationModel.Password, registrationModel.Groups);
		}
		// % protected region % [The default register function for Protocol Entity] end

		// % protected region % [The default register function for Usher Entity] off begin
		/// <summary>
		/// Registers a new Usher Entity user
		/// </summary>
		/// <param name="registrationModel">The fields to set on the user</param>
		/// <returns>A user result on success or a list of errors on failure</returns>
		[HttpPost]
		[Route("usher-entity")]
		[Authorize(Roles = "Visitors,Admin,Member,CategoryGroupLeader,Usher,Protocol,GroupCategory,Super Administrators")]
		public async Task<IActionResult> RegisterUsherEntity([FromBody] UsherEntityRegistrationModel registrationModel)
		{
			var model = registrationModel.ToModel();
			return await Register(model, registrationModel.Password, registrationModel.Groups);
		}
		// % protected region % [The default register function for Usher Entity] end


		// % protected region % [Customise confirm email here] off begin
		[HttpPost]
		[Route("confirm-email")]
		[AllowAnonymous]
		public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailModel model)
		{
			var result = await _userService.ConfirmEmail(model.Email, model.Token);

			if (!result.Succeeded)
			{
				_logger.LogInformation("User confirm email validation failed for {Email}", model.Email);
				_logger.LogInformation(JsonConvert.SerializeObject(result));
				return Unauthorized();
			}

			return Ok();
		}
		// % protected region % [Customise confirm email here] end

		// % protected region % [The default register function] off begin
		private async Task<IActionResult> Register(User model, string password, IEnumerable<string> groups)
		{
			try
			{
				var result = await _userService.RegisterUser(model, password, groups, true);

				if (result.Result.Succeeded == false)
				{
					_logger.LogInformation("Failed to create user {Email}", model.Email);
					_logger.LogInformation(JsonConvert.SerializeObject(result.Result));
					return BadRequest(new ApiErrorResponse(result.Result.Errors.Select(e => e.Description)));
				}

				var userResponse = await _userService.GetUser(result.User);
				return Ok(userResponse);
			}
			catch (DuplicateUserException e)
			{
				_logger.LogInformation("Attempted to create duplicate user. Email: {Email}", model.Email);
				// In the case of a duplicate user return a 409 Conflict response code
				return StatusCode(StatusCodes.Status409Conflict, new ApiErrorResponse(e.Message));
			}
		}
		// % protected region % [The default register function] end

		// % protected region % [Add any extra registration methods here] off begin
		// % protected region % [Add any extra registration methods here] end
	}

	// % protected region % [Add any extra content here] off begin
	// % protected region % [Add any extra content here] end
}