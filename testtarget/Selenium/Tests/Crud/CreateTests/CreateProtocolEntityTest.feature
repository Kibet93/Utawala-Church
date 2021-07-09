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
@BotWritten @create
Feature: Create ProtocolEntity
# % protected region % [Override feature properties here] end
# % protected region % [Override test content here] off begin
	@ProtocolEntity
	Scenario: Create ProtocolEntity
	Given I login to the site as a user
	And I navigate to the ProtocolEntity admin crud page
	And I click to create a ProtocolEntity
	When I create a valid ProtocolEntity
	Then I assert that I am on the ProtocolEntity admin crud page
	Then I assert a toaster alert is displayed with the message: Successfully added Protocol
# % protected region % [Override test content here] end

# % protected region % [Add any additional tests here] off begin
# % protected region % [Add any additional tests here] end