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
@BotWritten @associations
Feature: Reference from CategoryLeadersEntity using Category Group Leader to MembersEntity using MEMBER
# % protected region % [Override feature properties here] end
	Scenario: Reference from CategoryLeadersEntity using Category Group Leader to MembersEntity using MEMBER
	Given I login to the site as a user
	And I navigate to the CategoryLeadersEntity backend page
	And I create 1 CategoryLeadersEntity's each associated with 1 Member using MEMBER
	Then I validate each MembersEntity has 1 CategoryLeadersEntity associations using Category Group Leader
	Then I validate each CategoryLeadersEntity has 1 MembersEntity associations using MEMBER