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

export type categoryGroups =
	// % protected region % [Override categoryGroups keys here] off begin
	'MEN' |
		'WOMEN' |
		'YOUNG_MEN' |
		'YOUNG_LADIES' |
		'CHILDREN';
	// % protected region % [Override categoryGroups keys here] end

export const categoryGroupsOptions: { [key in categoryGroups]: string } = {
	// % protected region % [Override categoryGroups display fields here] off begin
	MEN: 'Men',
	WOMEN: 'Women ',
	YOUNG_MEN: 'Young Men ',
	YOUNG_LADIES: 'Young Ladies',
	CHILDREN: 'Children',
	// % protected region % [Override categoryGroups display fields here] end
};

export type membershipstatus =
	// % protected region % [Override membershipstatus keys here] off begin
	'FIRST_TIME_VISITOR' |
		'SECOND_TIME_VISITOR' |
		'MEMBER';
	// % protected region % [Override membershipstatus keys here] end

export const membershipstatusOptions: { [key in membershipstatus]: string } = {
	// % protected region % [Override membershipstatus display fields here] off begin
	FIRST_TIME_VISITOR: 'First Time Visitor',
	SECOND_TIME_VISITOR: 'Second Time Visitor',
	MEMBER: 'Member',
	// % protected region % [Override membershipstatus display fields here] end
};

export type reservation =
	// % protected region % [Override reservation keys here] off begin
	'RESERVED' |
		'OPEN';
	// % protected region % [Override reservation keys here] end

export const reservationOptions: { [key in reservation]: string } = {
	// % protected region % [Override reservation display fields here] off begin
	RESERVED: 'Reserved',
	OPEN: 'Open',
	// % protected region % [Override reservation display fields here] end
};

export type status =
	// % protected region % [Override status keys here] off begin
	'ACTIVE' |
		'INACTIVE' |
		'SUSPENDED';
	// % protected region % [Override status keys here] end

export const statusOptions: { [key in status]: string } = {
	// % protected region % [Override status display fields here] off begin
	ACTIVE: 'Active',
	INACTIVE: 'Inactive',
	SUSPENDED: 'Suspended',
	// % protected region % [Override status display fields here] end
};

// % protected region % [Add any extra enums here] off begin
// % protected region % [Add any extra enums here] end
