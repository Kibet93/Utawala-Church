###
# @bot-written
#
# WARNING AND NOTICE
# Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
# Full Software Licence as accepted by you before being granted access to this source code and other materials,
# the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-licence. Any
# commercial use in contravention of the terms of the Full Software Licence may be pursued by Codebots through
# licence termination and further legal action, and be required to indemnify Codebots for any loss or damage,
# including interest and costs. You are deemed to have accepted the terms of the Full Software Licence on any
# access, download, storage, and/or use of this source code.
#
# BOT WARNING
# This file is bot-written.
# Any changes out side of "protected regions" will be lost next time the bot makes any changes.
###
# % protected region % [Override feature properties here] off begin
@sorting @BotWritten @ignore
# WARNING: These Tests have been flagged as unstable and have been ignored until they are updated.
# % protected region % [Override feature properties here] end

Feature: Sort MembersEntity
	@MembersEntity
	Scenario: Sort MembersEntity
	Given I login to the site as a user
	And I navigate to the MembersEntity backend page
	When I sort MembersEntity by Full Name
	Then I assert that Full Name in MembersEntity of type String is properly sorted in descending
	When I sort MembersEntity by Full Name
	Then I assert that Full Name in MembersEntity of type String is properly sorted in ascending
	When I sort MembersEntity by National ID
	Then I assert that National ID in MembersEntity of type String is properly sorted in descending
	When I sort MembersEntity by National ID
	Then I assert that National ID in MembersEntity of type String is properly sorted in ascending
	When I sort MembersEntity by Residence
	Then I assert that Residence in MembersEntity of type String is properly sorted in descending
	When I sort MembersEntity by Residence
	Then I assert that Residence in MembersEntity of type String is properly sorted in ascending
	When I sort MembersEntity by Date of Birth
	Then I assert that Date of Birth in MembersEntity of type Date is properly sorted in descending
	When I sort MembersEntity by Date of Birth
	Then I assert that Date of Birth in MembersEntity of type Date is properly sorted in ascending
	When I sort MembersEntity by Age
	Then I assert that Age in MembersEntity of type int is properly sorted in descending
	When I sort MembersEntity by Age
	Then I assert that Age in MembersEntity of type int is properly sorted in ascending
	When I sort MembersEntity by Status
	Then I assert that Status in MembersEntity of type String is properly sorted in descending
	When I sort MembersEntity by Status
	Then I assert that Status in MembersEntity of type String is properly sorted in ascending
	When I sort MembersEntity by Membership Status
	Then I assert that Membership Status in MembersEntity of type String is properly sorted in descending
	When I sort MembersEntity by Membership Status
	Then I assert that Membership Status in MembersEntity of type String is properly sorted in ascending
	When I sort MembersEntity by Category Choice
	Then I assert that Category Choice in MembersEntity of type String is properly sorted in descending
	When I sort MembersEntity by Category Choice
	Then I assert that Category Choice in MembersEntity of type String is properly sorted in ascending
	When I sort MembersEntity by Accountability Grp
	Then I assert that Accountability Grp in MembersEntity of type int is properly sorted in descending
	When I sort MembersEntity by Accountability Grp
	Then I assert that Accountability Grp in MembersEntity of type int is properly sorted in ascending
	When I sort MembersEntity by Picture
	Then I assert that Picture in MembersEntity of type String is properly sorted in descending
	When I sort MembersEntity by Picture
	Then I assert that Picture in MembersEntity of type String is properly sorted in ascending

