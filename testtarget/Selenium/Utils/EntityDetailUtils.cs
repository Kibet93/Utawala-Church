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
using SeleniumTests.PageObjects.BotWritten;
using SeleniumTests.PageObjects.CRUDPageObject.PageDetails;
using SeleniumTests.Setup;

namespace SeleniumTests.Utils
{
	internal static class EntityDetailUtils
	{
		public static IDetailSection GetEntityDetailsSection(string entityName, ContextConfiguration contextConfiguration)
		{
			switch (entityName)
			{
				case "AccountabilityGroupsEntity":
					return new AccountabilityGroupsEntityDetailSection(contextConfiguration);
				case "AttendanceEntity":
					return new AttendanceEntityDetailSection(contextConfiguration);
				case "CategoryLeadersEntity":
					return new CategoryLeadersEntityDetailSection(contextConfiguration);
				case "MembersEntity":
					return new MembersEntityDetailSection(contextConfiguration);
				case "NoOfServiceEntity":
					return new NoOfServiceEntityDetailSection(contextConfiguration);
				case "AdminEntity":
					return new AdminEntityDetailSection(contextConfiguration);
				case "HomeFellowshipEntity":
					return new HomeFellowshipEntityDetailSection(contextConfiguration);
				case "ProtocolEntity":
					return new ProtocolEntityDetailSection(contextConfiguration);
				case "SeatsEntity":
					return new SeatsEntityDetailSection(contextConfiguration);
				case "ServicesEntity":
					return new ServicesEntityDetailSection(contextConfiguration);
				case "UsherEntity":
					return new UsherEntityDetailSection(contextConfiguration);
				case "WorkflowEntity":
					return new WorkflowEntityDetailSection(contextConfiguration);
				case "WorkflowTransitionEntity":
					return new WorkflowTransitionEntityDetailSection(contextConfiguration);
				case "WorkflowVersionEntity":
					return new WorkflowVersionEntityDetailSection(contextConfiguration);
				case "AttendanceSubmissionEntity":
					return new AttendanceSubmissionEntityDetailSection(contextConfiguration);
				default:
					throw new Exception($"Cannot find detail section for type {entityName}");
			}
		}

		public static WorkflowPage GetWorkflowEntityDetailsSection(string entityName, ContextConfiguration contextConfiguration)
		{
			switch (entityName)
			{
				case "SeatsEntity":
					return new SeatsEntityDetailSection(contextConfiguration);
				default:
					throw new Exception($"Cannot find detail section for type {entityName}");
			}
		}
	}
}