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
import { action, observable } from 'mobx';
import { Model, IModelAttributes, attribute, entity } from 'Models/Model';
import * as Models from 'Models/Entities';
import * as Validators from 'Validators';
import { CRUD } from '../CRUDOptions';
import * as AttrUtils from "Util/AttributeUtils";
import { IAcl } from 'Models/Security/IAcl';
import {
	getCreatedModifiedCrudOptions,
} from 'Util/EntityUtils';
import { AdminWorkflowBehaviour } from 'Models/Security/Acl/AdminWorkflowBehaviour';
import { MemberWorkflowBehaviour } from 'Models/Security/Acl/MemberWorkflowBehaviour';
import { CategoryGroupLeaderWorkflowBehaviour } from 'Models/Security/Acl/CategoryGroupLeaderWorkflowBehaviour';
import { UsherWorkflowBehaviour } from 'Models/Security/Acl/UsherWorkflowBehaviour';
import { ProtocolWorkflowBehaviour } from 'Models/Security/Acl/ProtocolWorkflowBehaviour';
import { GroupCategoryWorkflowBehaviour } from 'Models/Security/Acl/GroupCategoryWorkflowBehaviour';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import {SuperAdministratorScheme} from '../Security/Acl/SuperAdministratorScheme';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface IWorkflowTransitionEntityAttributes extends IModelAttributes {
	transitionName: string;

	sourceStateId: string;
	sourceState: Models.WorkflowStateEntity | Models.IWorkflowStateEntityAttributes;
	targetStateId: string;
	targetState: Models.WorkflowStateEntity | Models.IWorkflowStateEntityAttributes;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('WorkflowTransitionEntity', 'Workflow Transition')
// % protected region % [Customise your entity metadata here] end
export default class WorkflowTransitionEntity extends Model implements IWorkflowTransitionEntityAttributes {
	public static acls: IAcl[] = [
		new SuperAdministratorScheme(),
		new AdminWorkflowBehaviour(),
		new MemberWorkflowBehaviour(),
		new CategoryGroupLeaderWorkflowBehaviour(),
		new UsherWorkflowBehaviour(),
		new ProtocolWorkflowBehaviour(),
		new GroupCategoryWorkflowBehaviour(),
		// % protected region % [Add any further ACL entries here] off begin
		// % protected region % [Add any further ACL entries here] end
	];

	/**
	 * Fields to exclude from the JSON serialization in create operations.
	 */
	public static excludeFromCreate: string[] = [
		// % protected region % [Add any custom create exclusions here] off begin
		// % protected region % [Add any custom create exclusions here] end
	];

	/**
	 * Fields to exclude from the JSON serialization in update operations.
	 */
	public static excludeFromUpdate: string[] = [
		// % protected region % [Add any custom update exclusions here] off begin
		// % protected region % [Add any custom update exclusions here] end
	];

	// % protected region % [Modify props to the crud options here for attribute 'Transition Name'] off begin
	/**
	 * The name of transition
	 */
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		name: 'Transition Name',
		displayType: 'textfield',
		order: 10,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public transitionName: string;
	// % protected region % [Modify props to the crud options here for attribute 'Transition Name'] end

	/**
	 * Outgoing Transitions from a State
	 */
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Source State'] off begin
		name: 'Source State',
		displayType: 'reference-combobox',
		order: 20,
		referenceTypeFunc: () => Models.WorkflowStateEntity,
		// % protected region % [Modify props to the crud options here for reference 'Source State'] end
	})
	public sourceStateId: string;
	@observable
	@attribute({isReference: true})
	public sourceState: Models.WorkflowStateEntity;

	/**
	 * Incoming Transitions to a state
	 */
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Target State'] off begin
		name: 'Target State',
		displayType: 'reference-combobox',
		order: 30,
		referenceTypeFunc: () => Models.WorkflowStateEntity,
		// % protected region % [Modify props to the crud options here for reference 'Target State'] end
	})
	public targetStateId: string;
	@observable
	@attribute({isReference: true})
	public targetState: Models.WorkflowStateEntity;

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	// eslint-disable-next-line @typescript-eslint/no-useless-constructor
	constructor(attributes?: Partial<IWorkflowTransitionEntityAttributes>) {
		// % protected region % [Add any extra constructor logic before calling super here] off begin
		// % protected region % [Add any extra constructor logic before calling super here] end

		super(attributes);

		// % protected region % [Add any extra constructor logic after calling super here] off begin
		// % protected region % [Add any extra constructor logic after calling super here] end
	}

	/**
	 * Assigns fields from a passed in JSON object to the fields in this model.
	 * Any reference objects that are passed in are converted to models if they are not already.
	 * This function is called from the constructor to assign the initial fields.
	 */
	@action
	public assignAttributes(attributes?: Partial<IWorkflowTransitionEntityAttributes>) {
		// % protected region % [Override assign attributes here] off begin
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.transitionName !== undefined) {
				this.transitionName = attributes.transitionName;
			}
			if (attributes.sourceStateId !== undefined) {
				this.sourceStateId = attributes.sourceStateId;
			}
			if (attributes.sourceState !== undefined) {
				if (attributes.sourceState === null) {
					this.sourceState = attributes.sourceState;
				} else {
					if (attributes.sourceState instanceof Models.WorkflowStateEntity) {
						this.sourceState = attributes.sourceState;
						this.sourceStateId = attributes.sourceState.id;
					} else {
						this.sourceState = new Models.WorkflowStateEntity(attributes.sourceState);
						this.sourceStateId = this.sourceState.id;
					}
				}
			}
			if (attributes.targetStateId !== undefined) {
				this.targetStateId = attributes.targetStateId;
			}
			if (attributes.targetState !== undefined) {
				if (attributes.targetState === null) {
					this.targetState = attributes.targetState;
				} else {
					if (attributes.targetState instanceof Models.WorkflowStateEntity) {
						this.targetState = attributes.targetState;
						this.targetStateId = attributes.targetState.id;
					} else {
						this.targetState = new Models.WorkflowStateEntity(attributes.targetState);
						this.targetStateId = this.targetState.id;
					}
				}
			}
			// % protected region % [Override assign attributes here] end

			// % protected region % [Add any extra assign attributes logic here] off begin
			// % protected region % [Add any extra assign attributes logic here] end
		}
	}

	/**
	 * Additional fields that are added to GraphQL queries when using the
	 * the managed model APIs.
	 */
	// % protected region % [Customize Default Expands here] off begin
	public defaultExpands = `
		sourceState {
			${Models.WorkflowStateEntity.getAttributes().join('\n')}
		}
		targetState {
			${Models.WorkflowStateEntity.getAttributes().join('\n')}
		}
	`;
	// % protected region % [Customize Default Expands here] end

	/**
	 * The save method that is called from the admin CRUD components.
	 */
	// % protected region % [Customize Save From Crud here] off begin
	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
		};
		return this.save(
			relationPath,
			{
				options: [
					{
						key: 'mergeReferences',
						graphQlType: '[String]',
						value: [
						]
					},
				],
			}
		);
	}
	// % protected region % [Customize Save From Crud here] end

	/**
	 * Returns the string representation of this entity to display on the UI.
	 */
	public getDisplayName() {
		// % protected region % [Customise the display name for this entity] off begin
		return this.id;
		// % protected region % [Customise the display name for this entity] end
	}


	// % protected region % [Add any further custom model features here] off begin
	// % protected region % [Add any further custom model features here] end
}

// % protected region % [Modify the create and modified CRUD attributes here] off begin
/*
 * Retrieve the created and modified CRUD attributes for defining the CRUD views and decorate the class with them.
 */
const [ createdAttr, modifiedAttr ] = getCreatedModifiedCrudOptions();
CRUD(createdAttr)(WorkflowTransitionEntity.prototype, 'created');
CRUD(modifiedAttr)(WorkflowTransitionEntity.prototype, 'modified');
// % protected region % [Modify the create and modified CRUD attributes here] end
