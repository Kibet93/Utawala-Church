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
using System.Threading.Tasks;
using Utawalaaltar.Models;
using Utawalaaltar.Models.RegistrationModels;
using ServersideTests.Helpers;
using Xunit;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

// to prevent warnings of using the model type in theory data.
#pragma warning disable xUnit1026
#pragma warning disable S2699

namespace ServersideTests.Tests.Integration.BotWritten.GroupSecurityTests.Create
{
	[Trait("Category", "BotWritten")]
	[Trait("Category", "Integration")]
	[Trait("Category", "Security")]
	public class UnauthenticatedCreateTest : BaseCreateTest
	{

		public UnauthenticatedCreateTest()
		{
			// % protected region % [Add constructor logic here] off begin
			// % protected region % [Add constructor logic here] end
		}

		public static TheoryData<IAbstractModel, string, string> UnauthenticatedCreateSecurityData 
		{
			get
			{
				var data = new TheoryData<IAbstractModel, string,string>
				{
					// % protected region % [Configure entity theory data for Unauthenticated here] off begin
					{new AccountabilityGroupsEntity(), SecurityStringHelper.UserPermissionDenied, null},
					{new AttendanceEntity(), SecurityStringHelper.UserPermissionDenied, null},
					{new NoOfServiceEntity(), SecurityStringHelper.UserPermissionDenied, null},
					{new HomeFellowshipEntity(), SecurityStringHelper.UserPermissionDenied, null},
					{new SeatsEntity(), SecurityStringHelper.UserPermissionDenied, null},
					{new ServicesEntity(), SecurityStringHelper.UserPermissionDenied, null},
					{new WorkflowEntity(), SecurityStringHelper.NoApplicableSchemes, null},
					{new WorkflowStateEntity(), SecurityStringHelper.NoApplicableSchemes, null},
					{new WorkflowTransitionEntity(), SecurityStringHelper.NoApplicableSchemes, null},
					{new WorkflowVersionEntity(), SecurityStringHelper.NoApplicableSchemes, null},
					{new AttendanceSubmissionEntity(), null, null},
					{new AttendanceEntityFormTileEntity(), SecurityStringHelper.UserPermissionDenied, null},
					// % protected region % [Configure entity theory data for Unauthenticated here] end
				};
				// % protected region % [Add any extra theory data here] off begin
				// % protected region % [Add any extra theory data here] end
				return data;
			}
		}

		[Theory]
		[MemberData(nameof(UnauthenticatedCreateSecurityData))]
		public async Task UnauthenticatedCreateTests<T>(T model, string message, string groupName)
			where T : class, IOwnerAbstractModel, new()
		{
			// % protected region % [Overwrite create security test here] off begin
			await CreateSecurityTest(model, message, groupName);
			// % protected region % [Overwrite create security test here] end
		}

		public static TheoryData<IAbstractModel, object, string, string> UnauthenticatedCreateUserSecurityData
		{
			get
			{
				var data = new TheoryData<IAbstractModel, object, string, string>
				{
					// % protected region % [Configure user theory data for Unauthenticated here] off begin
					{new CategoryLeadersEntity(), new CategoryLeadersEntityGraphQlRegistrationModel(), SecurityStringHelper.UserPermissionDenied, null},
					{new MembersEntity(), new MembersEntityGraphQlRegistrationModel(), SecurityStringHelper.UserPermissionDenied, null},
					{new AdminEntity(), new AdminEntityGraphQlRegistrationModel(), SecurityStringHelper.UserPermissionDenied, null},
					{new ProtocolEntity(), new ProtocolEntityGraphQlRegistrationModel(), SecurityStringHelper.UserPermissionDenied, null},
					{new UsherEntity(), new UsherEntityGraphQlRegistrationModel(), SecurityStringHelper.UserPermissionDenied, null},
					// % protected region % [Configure user theory data for Unauthenticated here] end
				};
				// % protected region % [Add any extra user theory data here] off begin
				// % protected region % [Add any extra user theory data here] end
				return data;
			}
		}

		[Theory]
		[MemberData(nameof(UnauthenticatedCreateUserSecurityData))]
		public async Task UnauthenticatedCreateUserTests<T, TDto>(T model, TDto dto, string message, string groupName)
			where T : User, IOwnerAbstractModel, new()
			where TDto : ModelDto<T>, IRegistrationModel<T>, new()
		{
			// % protected region % [Overwrite create user security test here] off begin
			await CreateUserTest(model, dto, message, groupName);
			// % protected region % [Overwrite create user security test here] end
		}
	}
}