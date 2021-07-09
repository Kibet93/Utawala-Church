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

export interface IWorkflowVersionEntityAttributes extends IModelAttributes {
	workflowName: string;
	workflowDescription: string;
	versionNumber: number;
	seatsAssociation: boolean;

	statess: Array<Models.WorkflowStateEntity | Models.IWorkflowStateEntityAttributes>;
	workflowId: string;
	workflow: Models.WorkflowEntity | Models.IWorkflowEntityAttributes;
	currentWorkflow?: Models.WorkflowEntity | Models.IWorkflowEntityAttributes;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('WorkflowVersionEntity', 'Workflow Version')
// % protected region % [Customise your entity metadata here] end
export default class WorkflowVersionEntity extends Model implements IWorkflowVersionEntityAttributes {
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

	// % protected region % [Modify props to the crud options here for attribute 'Workflow Name'] off begin
	/**
	 * Workflow Name
	 */
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		name: 'Workflow Name',
		displayType: 'textfield',
		order: 10,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public workflowName: string;
	// % protected region % [Modify props to the crud options here for attribute 'Workflow Name'] end

	// % protected region % [Modify props to the crud options here for attribute 'Workflow Description'] off begin
	/**
	 * Description of Workflow
	 */
	@observable
	@attribute()
	@CRUD({
		name: 'Workflow Description',
		displayType: 'textfield',
		order: 20,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public workflowDescription: string;
	// % protected region % [Modify props to the crud options here for attribute 'Workflow Description'] end

	// % protected region % [Modify props to the crud options here for attribute 'Version Number'] off begin
	/**
	 * Version Number of Workflow Version
	 */
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'Version Number',
		displayType: 'textfield',
		order: 30,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public versionNumber: number;
	// % protected region % [Modify props to the crud options here for attribute 'Version Number'] end

	// % protected region % [Modify props to the crud options here for attribute 'Seats Association'] off begin
	/**
	 * If Seats's are associated with this workflow version
	 */
	@observable
	@attribute()
	@CRUD({
		name: 'Seats Association',
		displayType: 'checkbox',
		order: 40,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseBoolean,
		displayFunction: attr => attr ? 'True' : 'False',
	})
	public seatsAssociation: boolean;
	// % protected region % [Modify props to the crud options here for attribute 'Seats Association'] end

	/**
	 * A workflow has many states
	 */
	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'States'] off begin
		name: "Statess",
		displayType: 'reference-multicombobox',
		order: 50,
		referenceTypeFunc: () => Models.WorkflowStateEntity,
		disableDefaultOptionRemoval: true,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'statess',
			oppositeEntity: () => Models.WorkflowStateEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'States'] end
	})
	public statess: Models.WorkflowStateEntity[] = [];

	/**
	 * Version of a workflow
	 */
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Workflow'] off begin
		name: 'Workflow',
		displayType: 'reference-combobox',
		order: 60,
		referenceTypeFunc: () => Models.WorkflowEntity,
		// % protected region % [Modify props to the crud options here for reference 'Workflow'] end
	})
	public workflowId: string;
	@observable
	@attribute({isReference: true})
	public workflow: Models.WorkflowEntity;

	/**
	 * The current version of this workflow
	 */
	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Current Workflow'] off begin
		name: 'Current Workflow',
		displayType: 'reference-combobox',
		order: 70,
		referenceTypeFunc: () => Models.WorkflowEntity,
		optionEqualFunc: (model, option) => model.id === option,
		inputProps: {
			fetchReferenceEntity: true,
		},
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'currentWorkflows',
			oppositeEntity: () => Models.WorkflowEntity,
		})
		// % protected region % [Modify props to the crud options here for reference 'Current Workflow'] end
	})
	public currentWorkflow?: Models.WorkflowEntity;

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	// eslint-disable-next-line @typescript-eslint/no-useless-constructor
	constructor(attributes?: Partial<IWorkflowVersionEntityAttributes>) {
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
	public assignAttributes(attributes?: Partial<IWorkflowVersionEntityAttributes>) {
		// % protected region % [Override assign attributes here] off begin
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.workflowName !== undefined) {
				this.workflowName = attributes.workflowName;
			}
			if (attributes.workflowDescription !== undefined) {
				this.workflowDescription = attributes.workflowDescription;
			}
			if (attributes.versionNumber !== undefined) {
				this.versionNumber = attributes.versionNumber;
			}
			if (attributes.seatsAssociation !== undefined) {
				this.seatsAssociation = attributes.seatsAssociation;
			}
			if (attributes.statess !== undefined && Array.isArray(attributes.statess)) {
				for (const model of attributes.statess) {
					if (model instanceof Models.WorkflowStateEntity) {
						this.statess.push(model);
					} else {
						this.statess.push(new Models.WorkflowStateEntity(model));
					}
				}
			}
			if (attributes.workflowId !== undefined) {
				this.workflowId = attributes.workflowId;
			}
			if (attributes.workflow !== undefined) {
				if (attributes.workflow === null) {
					this.workflow = attributes.workflow;
				} else {
					if (attributes.workflow instanceof Models.WorkflowEntity) {
						this.workflow = attributes.workflow;
						this.workflowId = attributes.workflow.id;
					} else {
						this.workflow = new Models.WorkflowEntity(attributes.workflow);
						this.workflowId = this.workflow.id;
					}
				}
			}
			if (attributes.currentWorkflow !== undefined) {
				if (attributes.currentWorkflow === null) {
					this.currentWorkflow = attributes.currentWorkflow;
				} else {
					if (attributes.currentWorkflow instanceof Models.WorkflowEntity) {
						this.currentWorkflow = attributes.currentWorkflow;
					} else {
						this.currentWorkflow = new Models.WorkflowEntity(attributes.currentWorkflow);
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
		statess {
			${Models.WorkflowStateEntity.getAttributes().join('\n')}
		}
		workflow {
			${Models.WorkflowEntity.getAttributes().join('\n')}
		}
		currentWorkflow {
			${Models.WorkflowEntity.getAttributes().join('\n')}
		}
	`;
	// % protected region % [Customize Default Expands here] end

	/**
	 * The save method that is called from the admin CRUD components.
	 */
	// % protected region % [Customize Save From Crud here] off begin
	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
			statess: {},
			currentWorkflow: {},
		};
		return this.save(
			relationPath,
			{
				options: [
					{
						key: 'mergeReferences',
						graphQlType: '[String]',
						value: [
							'currentWorkflow',
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
CRUD(createdAttr)(WorkflowVersionEntity.prototype, 'created');
CRUD(modifiedAttr)(WorkflowVersionEntity.prototype, 'modified');
// % protected region % [Modify the create and modified CRUD attributes here] end
