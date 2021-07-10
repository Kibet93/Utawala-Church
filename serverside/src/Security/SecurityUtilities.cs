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
				new VisitorsMembersEntity(),
				new VisitorsHomeFellowshipEntity(),
				new VisitorsAccountabilityGroupsEntity(),
				new VisitorsAttendanceEntity(),
				new VisitorsNoOfServiceEntity(),
				new VisitorsAdminEntity(),
				new VisitorsUsherEntity(),
				new VisitorsProtocolEntity(),
				new VisitorsCategoryLeadersEntity(),
				new VisitorsSeatsEntity(),
				new VisitorsServicesEntity(),
				new VisitorsHomePage(),
				new VisitorsRegisteredMembersPage(),
				new VisitorsServiceAttendancePage(),
				new VisitorsSeatBookingPage(),
				new AdminMembersEntity(),
				new AdminHomeFellowshipEntity(),
				new AdminAccountabilityGroupsEntity(),
				new AdminAttendanceEntity(),
				new AdminNoOfServiceEntity(),
				new AdminAdminEntity(),
				new AdminUsherEntity(),
				new AdminProtocolEntity(),
				new AdminCategoryLeadersEntity(),
				new AdminSeatsEntity(),
				new AdminServicesEntity(),
				new AdminHomePage(),
				new AdminRegisteredMembersPage(),
				new AdminServiceAttendancePage(),
				new AdminSeatBookingPage(),
				new MembersMembersEntity(),
				new MembersHomeFellowshipEntity(),
				new MembersAccountabilityGroupsEntity(),
				new MembersAttendanceEntity(),
				new MembersNoOfServiceEntity(),
				new MembersAdminEntity(),
				new MembersUsherEntity(),
				new MembersProtocolEntity(),
				new MembersCategoryLeadersEntity(),
				new MembersSeatsEntity(),
				new MembersServicesEntity(),
				new MembersHomePage(),
				new MembersRegisteredMembersPage(),
				new MembersServiceAttendancePage(),
				new MembersSeatBookingPage(),
				new CategoryLeadersMembersEntity(),
				new CategoryLeadersHomeFellowshipEntity(),
				new CategoryLeadersAccountabilityGroupsEntity(),
				new CategoryLeadersAttendanceEntity(),
				new CategoryLeadersNoOfServiceEntity(),
				new CategoryLeadersAdminEntity(),
				new CategoryLeadersUsherEntity(),
				new CategoryLeadersProtocolEntity(),
				new CategoryLeadersCategoryLeadersEntity(),
				new CategoryLeadersSeatsEntity(),
				new CategoryLeadersServicesEntity(),
				new CategoryLeadersHomePage(),
				new CategoryLeadersRegisteredMembersPage(),
				new CategoryLeadersServiceAttendancePage(),
				new CategoryLeadersSeatBookingPage(),
				new UsherMembersEntity(),
				new UsherHomeFellowshipEntity(),
				new UsherAccountabilityGroupsEntity(),
				new UsherAttendanceEntity(),
				new UsherNoOfServiceEntity(),
				new UsherAdminEntity(),
				new UsherUsherEntity(),
				new UsherProtocolEntity(),
				new UsherCategoryLeadersEntity(),
				new UsherSeatsEntity(),
				new UsherServicesEntity(),
				new UsherHomePage(),
				new UsherRegisteredMembersPage(),
				new UsherServiceAttendancePage(),
				new UsherSeatBookingPage(),
				new ProtocolMembersEntity(),
				new ProtocolHomeFellowshipEntity(),
				new ProtocolAccountabilityGroupsEntity(),
				new ProtocolAttendanceEntity(),
				new ProtocolNoOfServiceEntity(),
				new ProtocolAdminEntity(),
				new ProtocolUsherEntity(),
				new ProtocolProtocolEntity(),
				new ProtocolCategoryLeadersEntity(),
				new ProtocolSeatsEntity(),
				new ProtocolServicesEntity(),
				new ProtocolHomePage(),
				new ProtocolRegisteredMembersPage(),
				new ProtocolServiceAttendancePage(),
				new ProtocolSeatBookingPage(),
				new VisitorsAttendanceSubmission(),
				new AdminAttendanceSubmission(),
				new MembersAttendanceSubmission(),
				new CategoryLeadersAttendanceSubmission(),
				new UsherAttendanceSubmission(),
				new ProtocolAttendanceSubmission(),
				new AdminWorkflowBehaviour(),
				new MembersWorkflowBehaviour(),
				new CategoryLeadersWorkflowBehaviour(),
				new UsherWorkflowBehaviour(),
				new ProtocolWorkflowBehaviour(),
				// % protected region % [Add any additional ACLs to the return list here] off begin
				// % protected region % [Add any additional ACLs to the return list here] end
			};
		}
	}
}