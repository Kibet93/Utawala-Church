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
using APITests.Factories;
using Xunit;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

namespace APITests.TheoryData.BotWritten
{
	public class UserEntityFactorySingleTheoryData : TheoryData<UserEntityFactory>
	{
		public UserEntityFactorySingleTheoryData()
		{
			// % protected region % [Modify UserEntityFactorySingleTheoryData entities here] off begin
			Add(new UserEntityFactory("ProtocolEntity"));
			Add(new UserEntityFactory("UsherEntity"));
			Add(new UserEntityFactory("MembersEntity"));
			Add(new UserEntityFactory("AdminEntity"));
			Add(new UserEntityFactory("CategoryLeadersEntity"));
			// % protected region % [Modify UserEntityFactorySingleTheoryData entities here] end
		}
	}

	public class EntityFactorySingleTheoryData : TheoryData<EntityFactory, int>
	{
		public EntityFactorySingleTheoryData()
		{
			// % protected region % [Modify UserEntityFactorySingleTheoryData entities here] off begin
			Add(new EntityFactory("AccountabilityGroupsEntity"), 1);
			Add(new EntityFactory("AttendanceEntity"), 1);
			Add(new EntityFactory("CategoryLeadersEntity"), 1);
			Add(new EntityFactory("MembersEntity"), 1);
			Add(new EntityFactory("NoOfServiceEntity"), 1);
			Add(new EntityFactory("AdminEntity"), 1);
			Add(new EntityFactory("HomeFellowshipEntity"), 1);
			Add(new EntityFactory("ProtocolEntity"), 1);
			Add(new EntityFactory("SeatsEntity"), 1);
			Add(new EntityFactory("ServicesEntity"), 1);
			Add(new EntityFactory("UsherEntity"), 1);
			Add(new EntityFactory("WorkflowEntity"), 1);
			Add(new EntityFactory("WorkflowStateEntity"), 1);
			Add(new EntityFactory("WorkflowTransitionEntity"), 1);
			Add(new EntityFactory("WorkflowVersionEntity"), 1);
			// % protected region % [Modify UserEntityFactorySingleTheoryData entities here] end
		}
	}

	public class NonUserEntityFactorySingleTheoryData : TheoryData<EntityFactory, int>
	{
		public NonUserEntityFactorySingleTheoryData()
		{
			// % protected region % [Modify UserEntityFactorySingleTheoryData entities here] off begin
			Add(new EntityFactory("AccountabilityGroupsEntity"), 1);
			Add(new EntityFactory("AttendanceEntity"), 1);
			Add(new EntityFactory("CategoryLeadersEntity"), 1);
			Add(new EntityFactory("MembersEntity"), 1);
			Add(new EntityFactory("NoOfServiceEntity"), 1);
			Add(new EntityFactory("AdminEntity"), 1);
			Add(new EntityFactory("HomeFellowshipEntity"), 1);
			Add(new EntityFactory("ProtocolEntity"), 1);
			Add(new EntityFactory("SeatsEntity"), 1);
			Add(new EntityFactory("ServicesEntity"), 1);
			Add(new EntityFactory("UsherEntity"), 1);
			Add(new EntityFactory("WorkflowEntity"), 1);
			Add(new EntityFactory("WorkflowStateEntity"), 1);
			Add(new EntityFactory("WorkflowTransitionEntity"), 1);
			Add(new EntityFactory("WorkflowVersionEntity"), 1);
			// % protected region % [Modify UserEntityFactorySingleTheoryData entities here] end
		}
	}

	public class EntityFactoryTheoryData : TheoryData<EntityFactory>
	{
		public EntityFactoryTheoryData()
		{
			// % protected region % [Modify UserEntityFactorySingleTheoryData entities here] off begin
			Add(new EntityFactory("AccountabilityGroupsEntity"));
			Add(new EntityFactory("AttendanceEntity"));
			Add(new EntityFactory("CategoryLeadersEntity"));
			Add(new EntityFactory("MembersEntity"));
			Add(new EntityFactory("NoOfServiceEntity"));
			Add(new EntityFactory("AdminEntity"));
			Add(new EntityFactory("HomeFellowshipEntity"));
			Add(new EntityFactory("ProtocolEntity"));
			Add(new EntityFactory("SeatsEntity"));
			Add(new EntityFactory("ServicesEntity"));
			Add(new EntityFactory("UsherEntity"));
			Add(new EntityFactory("WorkflowEntity"));
			Add(new EntityFactory("WorkflowStateEntity"));
			Add(new EntityFactory("WorkflowTransitionEntity"));
			Add(new EntityFactory("WorkflowVersionEntity"));
			// % protected region % [Modify UserEntityFactorySingleTheoryData entities here] end
		}
	}

	public class EntityFactoryMultipleTheoryData : TheoryData<EntityFactory, int>
	{
		public EntityFactoryMultipleTheoryData()
		{
			// % protected region % [Modify UserEntityFactorySingleTheoryData entities here] off begin
			var numEntities = 3;
			Add(new EntityFactory("AccountabilityGroupsEntity"), numEntities);
			Add(new EntityFactory("AttendanceEntity"), numEntities);
			Add(new EntityFactory("CategoryLeadersEntity"), numEntities);
			Add(new EntityFactory("MembersEntity"), numEntities);
			Add(new EntityFactory("NoOfServiceEntity"), numEntities);
			Add(new EntityFactory("HomeFellowshipEntity"), numEntities);
			Add(new EntityFactory("ProtocolEntity"), numEntities);
			Add(new EntityFactory("SeatsEntity"), numEntities);
			Add(new EntityFactory("ServicesEntity"), numEntities);
			Add(new EntityFactory("UsherEntity"), numEntities);
			Add(new EntityFactory("WorkflowEntity"), numEntities);
			Add(new EntityFactory("WorkflowStateEntity"), numEntities);
			Add(new EntityFactory("WorkflowTransitionEntity"), numEntities);
			Add(new EntityFactory("WorkflowVersionEntity"), numEntities);
			// % protected region % [Modify UserEntityFactorySingleTheoryData entities here] end
		}
	}

	// % protected region % [Add any further custom EntityFactoryTheoryData here] off begin
	// % protected region % [Add any further custom EntityFactoryTheoryData here] end

}