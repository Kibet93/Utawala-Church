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
using APITests.EntityObjects.Models;
using APITests.Factories;
using SeleniumTests.PageObjects.CRUDPageObject.PageDetails;
using SeleniumTests.Setup;

namespace SeleniumTests.Factories
{
	internal class EntityDetailFactory
	{
		private readonly ContextConfiguration _contextConfiguration;

		public EntityDetailFactory(ContextConfiguration contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
		}

		public BaseEntity ApplyDetails(string entityName, bool isValid)
		{
			var entityFactory = new EntityFactory(entityName);
			var entity = entityFactory.Construct(isValid);
			entity.Configure(BaseEntity.ConfigureOptions.CREATE_ATTRIBUTES_AND_REFERENCES);
			CreateDetailSection(entityName, entity).Apply();
			return entity;
		}

		public IEntityDetailSection CreateDetailSection(string entityName, BaseEntity entity = null)
		{
			return entityName switch
			{
				"AccountabilityGroupsEntity" => new AccountabilityGroupsEntityDetailSection(_contextConfiguration, (AccountabilityGroupsEntity) entity),
				"AttendanceEntity" => new AttendanceEntityDetailSection(_contextConfiguration, (AttendanceEntity) entity),
				"CategoryLeadersEntity" => new CategoryLeadersEntityDetailSection(_contextConfiguration, (CategoryLeadersEntity) entity),
				"MembersEntity" => new MembersEntityDetailSection(_contextConfiguration, (MembersEntity) entity),
				"NoOfServiceEntity" => new NoOfServiceEntityDetailSection(_contextConfiguration, (NoOfServiceEntity) entity),
				"AdminEntity" => new AdminEntityDetailSection(_contextConfiguration, (AdminEntity) entity),
				"HomeFellowshipEntity" => new HomeFellowshipEntityDetailSection(_contextConfiguration, (HomeFellowshipEntity) entity),
				"ProtocolEntity" => new ProtocolEntityDetailSection(_contextConfiguration, (ProtocolEntity) entity),
				"SeatsEntity" => new SeatsEntityDetailSection(_contextConfiguration, (SeatsEntity) entity),
				"ServicesEntity" => new ServicesEntityDetailSection(_contextConfiguration, (ServicesEntity) entity),
				"UsherEntity" => new UsherEntityDetailSection(_contextConfiguration, (UsherEntity) entity),
				"WorkflowEntity" => new WorkflowEntityDetailSection(_contextConfiguration, (WorkflowEntity) entity),
				"WorkflowTransitionEntity" => new WorkflowTransitionEntityDetailSection(_contextConfiguration, (WorkflowTransitionEntity) entity),
				"WorkflowVersionEntity" => new WorkflowVersionEntityDetailSection(_contextConfiguration, (WorkflowVersionEntity) entity),
				"AttendanceSubmissionEntity" => new AttendanceSubmissionEntityDetailSection(_contextConfiguration, (AttendanceSubmissionEntity) entity),
				"AttendanceEntityFormTileEntity" => new AttendanceEntityFormTileEntityDetailSection(_contextConfiguration, (AttendanceEntityFormTileEntity) entity),
				_ => throw new Exception($"Cannot find entity type {entityName}"),
			};
		}
	}
}
