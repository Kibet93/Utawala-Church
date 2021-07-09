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

Feature: Sort AttendanceEntity
	@AttendanceEntity
	Scenario: Sort AttendanceEntity
	Given I login to the site as a user
	And I navigate to the AttendanceEntity backend page
	When I sort AttendanceEntity by Date Of Service
	Then I assert that Date Of Service in AttendanceEntity of type Date is properly sorted in descending
	When I sort AttendanceEntity by Date Of Service
	Then I assert that Date Of Service in AttendanceEntity of type Date is properly sorted in ascending
	When I sort AttendanceEntity by Service ID
	Then I assert that Service ID in AttendanceEntity of type int is properly sorted in descending
	When I sort AttendanceEntity by Service ID
	Then I assert that Service ID in AttendanceEntity of type int is properly sorted in ascending
	When I sort AttendanceEntity by Seat No ID
	Then I assert that Seat No ID in AttendanceEntity of type int is properly sorted in descending
	When I sort AttendanceEntity by Seat No ID
	Then I assert that Seat No ID in AttendanceEntity of type int is properly sorted in ascending
	When I sort AttendanceEntity by Temperature
	Then I assert that Temperature in AttendanceEntity of type double is properly sorted in descending
	When I sort AttendanceEntity by Temperature
	Then I assert that Temperature in AttendanceEntity of type double is properly sorted in ascending
	When I sort AttendanceEntity by Attended Service
	Then I assert that Attended Service in AttendanceEntity of type bool is properly sorted in descending
	When I sort AttendanceEntity by Attended Service
	Then I assert that Attended Service in AttendanceEntity of type bool is properly sorted in ascending
	When I sort AttendanceEntity by Reason For Not Attending
	Then I assert that Reason For Not Attending in AttendanceEntity of type String is properly sorted in descending
	When I sort AttendanceEntity by Reason For Not Attending
	Then I assert that Reason For Not Attending in AttendanceEntity of type String is properly sorted in ascending
	When I sort AttendanceEntity by Comment
	Then I assert that Comment in AttendanceEntity of type String is properly sorted in descending
	When I sort AttendanceEntity by Comment
	Then I assert that Comment in AttendanceEntity of type String is properly sorted in ascending

