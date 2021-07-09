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
import { Model, IModelAttributes, attribute, entity } from 'Models/Model';
import * as Models from 'Models/Entities';
import { IAcl } from '../Security/IAcl';
import { observable } from 'mobx';
import { AdminWorkflowBehaviour } from '../Security/Acl/AdminWorkflowBehaviour';
import { MemberWorkflowBehaviour } from '../Security/Acl/MemberWorkflowBehaviour';
import { CategoryGroupLeaderWorkflowBehaviour } from '../Security/Acl/CategoryGroupLeaderWorkflowBehaviour';
import { UsherWorkflowBehaviour } from '../Security/Acl/UsherWorkflowBehaviour';
import { ProtocolWorkflowBehaviour } from '../Security/Acl/ProtocolWorkflowBehaviour';
import { GroupCategoryWorkflowBehaviour } from '../Security/Acl/GroupCategoryWorkflowBehaviour';

// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface ISeatsWorkflowStatesAttributes extends IModelAttributes {
	seatsId: string;
	workflowStatesId: string;

	seats: Models.SeatsEntity | Models.ISeatsEntityAttributes;
	workflowStates: Models.WorkflowStateEntity | Models.IWorkflowStateEntityAttributes;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

@entity('SeatsWorkflowStates')
export default class SeatsWorkflowStates extends Model implements ISeatsWorkflowStatesAttributes {
	public static acls: IAcl[] = [
		new AdminWorkflowBehaviour(),
		new MemberWorkflowBehaviour(),
		new CategoryGroupLeaderWorkflowBehaviour(),
		new UsherWorkflowBehaviour(),
		new ProtocolWorkflowBehaviour(),
		new GroupCategoryWorkflowBehaviour(),
		// % protected region % [Add any further ACL entries here] off begin
		// % protected region % [Add any further ACL entries here] end
	];

	@observable
	@attribute()
	public seatsId: string;

	@observable
	@attribute()
	public workflowStatesId: string;

	@observable
	@attribute({isReference: true})
	public seats: Models.SeatsEntity;

	@observable
	@attribute({isReference: true})
	public workflowStates: Models.WorkflowStateEntity;
	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	constructor(attributes?: Partial<ISeatsWorkflowStatesAttributes>) {
		// % protected region % [Add any extra constructor logic before calling super here] off begin
		// % protected region % [Add any extra constructor logic before calling super here] end

		super(attributes);

		if (attributes) {
			if (attributes.seatsId) {
				this.seatsId = attributes.seatsId;
			}
			if (attributes.workflowStatesId) {
				this.workflowStatesId = attributes.workflowStatesId;
			}

			if (attributes.seats) {
				if (attributes.seats instanceof Models.SeatsEntity) {
					this.seats = attributes.seats;
					this.seatsId = attributes.seats.id;
				} else {
					this.seats = new Models.SeatsEntity(attributes.seats);
					this.seatsId = this.seats.id;
				}
			} else if (attributes.seatsId !== undefined) {
				this.seatsId = attributes.seatsId;
			}

			if (attributes.workflowStates) {
				if (attributes.workflowStates instanceof Models.WorkflowStateEntity) {
					this.workflowStates = attributes.workflowStates;
					this.workflowStatesId = attributes.workflowStates.id;
				} else {
					this.workflowStates = new Models.WorkflowStateEntity(attributes.workflowStates);
					this.workflowStatesId = this.workflowStates.id;
				}
			} else if (attributes.workflowStatesId !== undefined) {
				this.workflowStatesId = attributes.workflowStatesId;
			}
		}

		// % protected region % [Add any extra constructor logic after calling super here] off begin
		// % protected region % [Add any extra constructor logic after calling super here] end
	}

	// % protected region % [Add any further custom model features here] off begin
	// % protected region % [Add any further custom model features here] end
}