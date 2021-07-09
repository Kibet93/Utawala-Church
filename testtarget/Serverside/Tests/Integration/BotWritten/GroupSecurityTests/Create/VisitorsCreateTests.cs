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
	public class VisitorsCreateTest : BaseCreateTest
	{

		public VisitorsCreateTest()
		{
			// % protected region % [Add constructor logic here] off begin
			// % protected region % [Add constructor logic here] end
		}

		public static TheoryData<IAbstractModel, string, string> VisitorsCreateSecurityData 
		{
			get
			{
				var data = new TheoryData<IAbstractModel, string,string>
				{
					// % protected region % [Configure entity theory data for Visitors here] off begin
					{new AccountabilityGroupEntity(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
					{new NoOfServiceEntity(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
					{new AttendanceEntity(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
					{new HomeFellowshipEntity(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
					{new SeatsEntity(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
					{new ServicesEntity(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
					{new WorkflowEntity(), SecurityStringHelper.NoApplicableSchemes, "Visitors"},
					{new WorkflowStateEntity(), SecurityStringHelper.NoApplicableSchemes, "Visitors"},
					{new WorkflowTransitionEntity(), SecurityStringHelper.NoApplicableSchemes, "Visitors"},
					{new WorkflowVersionEntity(), SecurityStringHelper.NoApplicableSchemes, "Visitors"},
					{new MemberSubmissionEntity(), null, "Visitors"},
					{new MemberEntityFormTileEntity(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
					// % protected region % [Configure entity theory data for Visitors here] end
				};
				// % protected region % [Add any extra theory data here] off begin
				// % protected region % [Add any extra theory data here] end
				return data;
			}
		}

		[Theory]
		[MemberData(nameof(VisitorsCreateSecurityData))]
		public async Task VisitorsCreateTests<T>(T model, string message, string groupName)
			where T : class, IOwnerAbstractModel, new()
		{
			// % protected region % [Overwrite create security test here] off begin
			await CreateSecurityTest(model, message, groupName);
			// % protected region % [Overwrite create security test here] end
		}

		public static TheoryData<IAbstractModel, object, string, string> VisitorsCreateUserSecurityData
		{
			get
			{
				var data = new TheoryData<IAbstractModel, object, string, string>
				{
					// % protected region % [Configure user theory data for Visitors here] off begin
					{new CategoryGroupLeaderEntity(), new CategoryGroupLeaderEntityGraphQlRegistrationModel(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
					{new GroupCategoryEntity(), new GroupCategoryEntityGraphQlRegistrationModel(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
					{new MemberEntity(), new MemberEntityGraphQlRegistrationModel(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
					{new AdminEntity(), new AdminEntityGraphQlRegistrationModel(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
					{new ProtocolEntity(), new ProtocolEntityGraphQlRegistrationModel(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
					{new UsherEntity(), new UsherEntityGraphQlRegistrationModel(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
					// % protected region % [Configure user theory data for Visitors here] end
				};
				// % protected region % [Add any extra user theory data here] off begin
				// % protected region % [Add any extra user theory data here] end
				return data;
			}
		}

		[Theory]
		[MemberData(nameof(VisitorsCreateUserSecurityData))]
		public async Task VisitorsCreateUserTests<T, TDto>(T model, TDto dto, string message, string groupName)
			where T : User, IOwnerAbstractModel, new()
			where TDto : ModelDto<T>, IRegistrationModel<T>, new()
		{
			// % protected region % [Overwrite create user security test here] off begin
			await CreateUserTest(model, dto, message, groupName);
			// % protected region % [Overwrite create user security test here] end
		}
	}
}