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
using ServersideTests.Helpers;
using Xunit;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

// to prevent warnings of using the model type in theory data.
#pragma warning disable xUnit1026
#pragma warning disable S2699

namespace ServersideTests.Tests.Integration.BotWritten.GroupSecurityTests.Read
{
	[Trait("Category", "BotWritten")]
	[Trait("Category", "Integration")]
	[Trait("Category", "Security")]
	public class UsherReadTests : BaseReadTest
	{

		public UsherReadTests()
		{
			// % protected region % [Add constructor logic here] off begin
			// % protected region % [Add constructor logic here] end
		}

		public static TheoryData<IAbstractModel, bool, string> UsherReadSecurityData 
		{
			get
			{
				var data = new TheoryData<IAbstractModel, bool, string>
				{
					// % protected region % [Configure entity theory data for Usher here] off begin
					{new AccountabilityGroupEntity(), false, null},
					{new NoOfServiceEntity(), true, "Usher"},
					{new AttendanceEntity(), true, "Usher"},
					{new HomeFellowshipEntity(), true, "Usher"},
					{new SeatsEntity(), false, null},
					{new ServicesEntity(), false, null},
					{new WorkflowEntity(), true, "Usher"},
					{new WorkflowStateEntity(), true, "Usher"},
					{new WorkflowTransitionEntity(), true, "Usher"},
					{new WorkflowVersionEntity(), true, "Usher"},
					{new MemberSubmissionEntity(), true, "Usher"},
					{new MemberEntityFormTileEntity(), true, "Usher"},
					// % protected region % [Configure entity theory data for Usher here] end
				};
				// % protected region % [Add any extra theory data here] off begin
				// % protected region % [Add any extra theory data here] end
				return data;
			}
		}

		[Theory]
		[MemberData(nameof(UsherReadSecurityData))]
		public async Task UsherReadSecurityTests<T>(T entity, bool canRead, string groupName)
			where T : class, IOwnerAbstractModel, new()
		{
			// % protected region % [Overwrite delete security test here] off begin
			await ReadTest(entity, canRead, groupName);
			// % protected region % [Overwrite delete security test here] end
		}
	}
}