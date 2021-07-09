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

Feature: Sort HomeFellowshipEntity
	@HomeFellowshipEntity
	Scenario: Sort HomeFellowshipEntity
	Given I login to the site as a user
	And I navigate to the HomeFellowshipEntity backend page
	When I sort HomeFellowshipEntity by Fellowship ID
	Then I assert that Fellowship ID in HomeFellowshipEntity of type int is properly sorted in descending
	When I sort HomeFellowshipEntity by Fellowship ID
	Then I assert that Fellowship ID in HomeFellowshipEntity of type int is properly sorted in ascending
	When I sort HomeFellowshipEntity by Fellowship Name
	Then I assert that Fellowship Name in HomeFellowshipEntity of type String is properly sorted in descending
	When I sort HomeFellowshipEntity by Fellowship Name
	Then I assert that Fellowship Name in HomeFellowshipEntity of type String is properly sorted in ascending
	When I sort HomeFellowshipEntity by Fellowship Pastor
	Then I assert that Fellowship Pastor in HomeFellowshipEntity of type String is properly sorted in descending
	When I sort HomeFellowshipEntity by Fellowship Pastor
	Then I assert that Fellowship Pastor in HomeFellowshipEntity of type String is properly sorted in ascending

