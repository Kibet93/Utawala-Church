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
	makeFetchManyToManyFunc,
	makeJoinEqualsFunc,
	makeFetchOneToManyFunc,
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

export interface IWorkflowStateEntityAttributes extends IModelAttributes {
	displayIndex: number;
	stepName: string;
	stateDescription: string;
	isStartState: boolean;

	workflowVersionId: string;
	workflowVersion: Models.WorkflowVersionEntity | Models.IWorkflowVersionEntityAttributes;
	outgoingTransitionss: Array<Models.WorkflowTransitionEntity | Models.IWorkflowTransitionEntityAttributes>;
	incomingTransitionss: Array<Models.WorkflowTransitionEntity | Models.IWorkflowTransitionEntityAttributes>;
	seatss: Array<Models.SeatsWorkflowStates | Models.ISeatsWorkflowStatesAttributes>;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('WorkflowStateEntity', 'Workflow State')
// % protected region % [Customise your entity metadata here] end
export default class WorkflowStateEntity extends Model implements IWorkflowStateEntityAttributes {
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

	// % protected region % [Modify props to the crud options here for attribute 'Display Index'] off begin
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'Display Index',
		displayType: 'textfield',
		order: 10,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public displayIndex: number;
	// % protected region % [Modify props to the crud options here for attribute 'Display Index'] end

	// % protected region % [Modify props to the crud options here for attribute 'Step Name'] off begin
	/**
	 * The name of the state
	 */
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		name: 'Step Name',
		displayType: 'textfield',
		order: 20,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public stepName: string;
	// % protected region % [Modify props to the crud options here for attribute 'Step Name'] end

	// % protected region % [Modify props to the crud options here for attribute 'State Description'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'State Description',
		displayType: 'textfield',
		order: 30,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public stateDescription: string;
	// % protected region % [Modify props to the crud options here for attribute 'State Description'] end

	// % protected region % [Modify props to the crud options here for attribute 'Is Start State'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'Is Start State',
		displayType: 'checkbox',
		order: 40,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseBoolean,
		displayFunction: attr => attr ? 'True' : 'False',
	})
	public isStartState: boolean;
	// % protected region % [Modify props to the crud options here for attribute 'Is Start State'] end

	/**
	 * A workflow has many states
	 */
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Workflow Version'] off begin
		name: 'Workflow Version',
		displayType: 'reference-combobox',
		order: 50,
		referenceTypeFunc: () => Models.WorkflowVersionEntity,
		// % protected region % [Modify props to the crud options here for reference 'Workflow Version'] end
	})
	public workflowVersionId: string;
	@observable
	@attribute({isReference: true})
	public workflowVersion: Models.WorkflowVersionEntity;

	/**
	 * Outgoing Transitions from a State
	 */
	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Outgoing Transitions'] off begin
		name: "Outgoing Transitionss",
		displayType: 'reference-multicombobox',
		order: 60,
		referenceTypeFunc: () => Models.WorkflowTransitionEntity,
		disableDefaultOptionRemoval: true,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'outgoingTransitionss',
			oppositeEntity: () => Models.WorkflowTransitionEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'Outgoing Transitions'] end
	})
	public outgoingTransitionss: Models.WorkflowTransitionEntity[] = [];

	/**
	 * Incoming Transitions to a state
	 */
	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Incoming Transitions'] off begin
		name: "Incoming Transitionss",
		displayType: 'reference-multicombobox',
		order: 70,
		referenceTypeFunc: () => Models.WorkflowTransitionEntity,
		disableDefaultOptionRemoval: true,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'incomingTransitionss',
			oppositeEntity: () => Models.WorkflowTransitionEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'Incoming Transitions'] end
	})
	public incomingTransitionss: Models.WorkflowTransitionEntity[] = [];

	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Seats'] off begin
		name: 'Seats',
		displayType: 'reference-multicombobox',
		order: 80,
		isJoinEntity: true,
		referenceTypeFunc: () => Models.SeatsWorkflowStates,
		optionEqualFunc: makeJoinEqualsFunc('seatsId'),
		referenceResolveFunction: makeFetchManyToManyFunc({
			entityName: 'workflowStateEntity',
			oppositeEntityName: 'seatsEntity',
			relationName: 'workflowStates',
			relationOppositeName: 'seats',
			entity: () => Models.WorkflowStateEntity,
			joinEntity: () => Models.SeatsWorkflowStates,
			oppositeEntity: () => Models.SeatsEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'Seats'] end
	})
	public seatss: Models.SeatsWorkflowStates[] = [];

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	// eslint-disable-next-line @typescript-eslint/no-useless-constructor
	constructor(attributes?: Partial<IWorkflowStateEntityAttributes>) {
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
	public assignAttributes(attributes?: Partial<IWorkflowStateEntityAttributes>) {
		// % protected region % [Override assign attributes here] off begin
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.displayIndex !== undefined) {
				this.displayIndex = attributes.displayIndex;
			}
			if (attributes.stepName !== undefined) {
				this.stepName = attributes.stepName;
			}
			if (attributes.stateDescription !== undefined) {
				this.stateDescription = attributes.stateDescription;
			}
			if (attributes.isStartState !== undefined) {
				this.isStartState = attributes.isStartState;
			}
			if (attributes.workflowVersionId !== undefined) {
				this.workflowVersionId = attributes.workflowVersionId;
			}
			if (attributes.workflowVersion !== undefined) {
				if (attributes.workflowVersion === null) {
					this.workflowVersion = attributes.workflowVersion;
				} else {
					if (attributes.workflowVersion instanceof Models.WorkflowVersionEntity) {
						this.workflowVersion = attributes.workflowVersion;
						this.workflowVersionId = attributes.workflowVersion.id;
					} else {
						this.workflowVersion = new Models.WorkflowVersionEntity(attributes.workflowVersion);
						this.workflowVersionId = this.workflowVersion.id;
					}
				}
			}
			if (attributes.outgoingTransitionss !== undefined && Array.isArray(attributes.outgoingTransitionss)) {
				for (const model of attributes.outgoingTransitionss) {
					if (model instanceof Models.WorkflowTransitionEntity) {
						this.outgoingTransitionss.push(model);
					} else {
						this.outgoingTransitionss.push(new Models.WorkflowTransitionEntity(model));
					}
				}
			}
			if (attributes.incomingTransitionss !== undefined && Array.isArray(attributes.incomingTransitionss)) {
				for (const model of attributes.incomingTransitionss) {
					if (model instanceof Models.WorkflowTransitionEntity) {
						this.incomingTransitionss.push(model);
					} else {
						this.incomingTransitionss.push(new Models.WorkflowTransitionEntity(model));
					}
				}
			}
			if (attributes.seatss !== undefined && Array.isArray(attributes.seatss)) {
				for (const model of attributes.seatss) {
					if (model instanceof Models.SeatsWorkflowStates) {
						this.seatss.push(model);
					} else {
						this.seatss.push(new Models.SeatsWorkflowStates(model));
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
		seatss {
			${Models.SeatsWorkflowStates.getAttributes().join('\n')}
			seats {
				${Models.SeatsEntity.getAttributes().join('\n')}
			}
		}
		workflowVersion {
			${Models.WorkflowVersionEntity.getAttributes().join('\n')}
		}
		outgoingTransitionss {
			${Models.WorkflowTransitionEntity.getAttributes().join('\n')}
		}
		incomingTransitionss {
			${Models.WorkflowTransitionEntity.getAttributes().join('\n')}
		}
	`;
	// % protected region % [Customize Default Expands here] end

	/**
	 * The save method that is called from the admin CRUD components.
	 */
	// % protected region % [Customize Save From Crud here] off begin
	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
			seatss: {},
			outgoingTransitionss: {},
			incomingTransitionss: {},
		};
		return this.save(
			relationPath,
			{
				options: [
					{
						key: 'mergeReferences',
						graphQlType: '[String]',
						value: [
							'seatss',
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
CRUD(createdAttr)(WorkflowStateEntity.prototype, 'created');
CRUD(modifiedAttr)(WorkflowStateEntity.prototype, 'modified');
// % protected region % [Modify the create and modified CRUD attributes here] end
