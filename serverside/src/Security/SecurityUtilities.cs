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
using Utawalaaltar.Security.Acl;

// % protected region % [Add any additional imports here] off begin
// % protected region % [Add any additional imports here] end

namespace Utawalaaltar.Security
{
	public static class SecurityUtilities
	{
		public static IEnumerable<IAcl> GetAllAcls()
		{
			return new List<IAcl>
			{
				new SuperAdministratorsScheme(),
				new VisitorsMemberEntity(),
				new VisitorsHomeFellowshipEntity(),
				new VisitorsAccountabilityGroupEntity(),
				new VisitorsGroupCategoryEntity(),
				new VisitorsAttendanceEntity(),
				new VisitorsNoOfServiceEntity(),
				new VisitorsAdminEntity(),
				new VisitorsUsherEntity(),
				new VisitorsProtocolEntity(),
				new VisitorsCategoryGroupLeaderEntity(),
				new VisitorsSeatsEntity(),
				new VisitorsServicesEntity(),
				new VisitorsHomePage(),
				new VisitorsBookingPage(),
				new VisitorsRegisteredMembersPage(),
				new VisitorsServiceAttendancePage(),
				new AdminMemberEntity(),
				new AdminHomeFellowshipEntity(),
				new AdminAccountabilityGroupEntity(),
				new AdminGroupCategoryEntity(),
				new AdminAttendanceEntity(),
				new AdminNoOfServiceEntity(),
				new AdminAdminEntity(),
				new AdminUsherEntity(),
				new AdminProtocolEntity(),
				new AdminCategoryGroupLeaderEntity(),
				new AdminSeatsEntity(),
				new AdminServicesEntity(),
				new AdminHomePage(),
				new AdminBookingPage(),
				new AdminRegisteredMembersPage(),
				new AdminServiceAttendancePage(),
				new MemberMemberEntity(),
				new MemberHomeFellowshipEntity(),
				new MemberAccountabilityGroupEntity(),
				new MemberGroupCategoryEntity(),
				new MemberAttendanceEntity(),
				new MemberNoOfServiceEntity(),
				new MemberAdminEntity(),
				new MemberUsherEntity(),
				new MemberProtocolEntity(),
				new MemberCategoryGroupLeaderEntity(),
				new MemberSeatsEntity(),
				new MemberServicesEntity(),
				new MemberHomePage(),
				new MemberBookingPage(),
				new MemberRegisteredMembersPage(),
				new MemberServiceAttendancePage(),
				new CategoryGroupLeaderMemberEntity(),
				new CategoryGroupLeaderHomeFellowshipEntity(),
				new CategoryGroupLeaderAccountabilityGroupEntity(),
				new CategoryGroupLeaderGroupCategoryEntity(),
				new CategoryGroupLeaderAttendanceEntity(),
				new CategoryGroupLeaderNoOfServiceEntity(),
				new CategoryGroupLeaderAdminEntity(),
				new CategoryGroupLeaderUsherEntity(),
				new CategoryGroupLeaderProtocolEntity(),
				new CategoryGroupLeaderCategoryGroupLeaderEntity(),
				new CategoryGroupLeaderSeatsEntity(),
				new CategoryGroupLeaderServicesEntity(),
				new CategoryGroupLeaderHomePage(),
				new CategoryGroupLeaderBookingPage(),
				new CategoryGroupLeaderRegisteredMembersPage(),
				new CategoryGroupLeaderServiceAttendancePage(),
				new UsherMemberEntity(),
				new UsherHomeFellowshipEntity(),
				new UsherAccountabilityGroupEntity(),
				new UsherGroupCategoryEntity(),
				new UsherAttendanceEntity(),
				new UsherNoOfServiceEntity(),
				new UsherAdminEntity(),
				new UsherUsherEntity(),
				new UsherProtocolEntity(),
				new UsherCategoryGroupLeaderEntity(),
				new UsherSeatsEntity(),
				new UsherServicesEntity(),
				new UsherHomePage(),
				new UsherBookingPage(),
				new UsherRegisteredMembersPage(),
				new UsherServiceAttendancePage(),
				new ProtocolMemberEntity(),
				new ProtocolHomeFellowshipEntity(),
				new ProtocolAccountabilityGroupEntity(),
				new ProtocolGroupCategoryEntity(),
				new ProtocolAttendanceEntity(),
				new ProtocolNoOfServiceEntity(),
				new ProtocolAdminEntity(),
				new ProtocolUsherEntity(),
				new ProtocolProtocolEntity(),
				new ProtocolCategoryGroupLeaderEntity(),
				new ProtocolSeatsEntity(),
				new ProtocolServicesEntity(),
				new ProtocolHomePage(),
				new ProtocolBookingPage(),
				new ProtocolRegisteredMembersPage(),
				new ProtocolServiceAttendancePage(),
				new GroupCategoryMemberEntity(),
				new GroupCategoryHomeFellowshipEntity(),
				new GroupCategoryAccountabilityGroupEntity(),
				new GroupCategoryGroupCategoryEntity(),
				new GroupCategoryAttendanceEntity(),
				new GroupCategoryNoOfServiceEntity(),
				new GroupCategoryAdminEntity(),
				new GroupCategoryUsherEntity(),
				new GroupCategoryProtocolEntity(),
				new GroupCategoryCategoryGroupLeaderEntity(),
				new GroupCategorySeatsEntity(),
				new GroupCategoryServicesEntity(),
				new GroupCategoryHomePage(),
				new GroupCategoryBookingPage(),
				new GroupCategoryRegisteredMembersPage(),
				new GroupCategoryServiceAttendancePage(),
				new VisitorsMemberSubmission(),
				new AdminMemberSubmission(),
				new MemberMemberSubmission(),
				new CategoryGroupLeaderMemberSubmission(),
				new UsherMemberSubmission(),
				new ProtocolMemberSubmission(),
				new GroupCategoryMemberSubmission(),
				new AdminWorkflowBehaviour(),
				new MemberWorkflowBehaviour(),
				new CategoryGroupLeaderWorkflowBehaviour(),
				new UsherWorkflowBehaviour(),
				new ProtocolWorkflowBehaviour(),
				new GroupCategoryWorkflowBehaviour(),
				// % protected region % [Add any additional ACLs to the return list here] off begin
				// % protected region % [Add any additional ACLs to the return list here] end
			};
		}
	}
}