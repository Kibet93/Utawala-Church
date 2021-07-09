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

export interface IWorkflowEntityAttributes extends IModelAttributes {
	name: string;

	versionss: Array<Models.WorkflowVersionEntity | Models.IWorkflowVersionEntityAttributes>;
	currentVersionId?: string;
	currentVersion?: Models.WorkflowVersionEntity | Models.IWorkflowVersionEntityAttributes;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('WorkflowEntity', 'Workflow')
// % protected region % [Customise your entity metadata here] end
export default class WorkflowEntity extends Model implements IWorkflowEntityAttributes {
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

	// % protected region % [Modify props to the crud options here for attribute 'Name'] off begin
	/**
	 * The name of the workflow
	 */
	@Validators.Required()
	@Validators.Unique()
	@observable
	@attribute()
	@CRUD({
		name: 'Name',
		displayType: 'textfield',
		order: 10,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public name: string;
	// % protected region % [Modify props to the crud options here for attribute 'Name'] end

	/**
	 * Version of a workflow
	 */
	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Versions'] off begin
		name: "Versionss",
		displayType: 'reference-multicombobox',
		order: 20,
		referenceTypeFunc: () => Models.WorkflowVersionEntity,
		disableDefaultOptionRemoval: true,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'versionss',
			oppositeEntity: () => Models.WorkflowVersionEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'Versions'] end
	})
	public versionss: Models.WorkflowVersionEntity[] = [];

	/**
	 * The current version of this workflow
	 */
	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Current Version'] off begin
		name: 'Current Version',
		displayType: 'reference-combobox',
		order: 30,
		referenceTypeFunc: () => Models.WorkflowVersionEntity,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'currentVersions',
			oppositeEntity: () => Models.WorkflowVersionEntity,
		})
		// % protected region % [Modify props to the crud options here for reference 'Current Version'] end
	})
	public currentVersionId?: string;
	@observable
	@attribute({isReference: true})
	public currentVersion: Models.WorkflowVersionEntity;

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	// eslint-disable-next-line @typescript-eslint/no-useless-constructor
	constructor(attributes?: Partial<IWorkflowEntityAttributes>) {
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
	public assignAttributes(attributes?: Partial<IWorkflowEntityAttributes>) {
		// % protected region % [Override assign attributes here] off begin
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.name !== undefined) {
				this.name = attributes.name;
			}
			if (attributes.versionss !== undefined && Array.isArray(attributes.versionss)) {
				for (const model of attributes.versionss) {
					if (model instanceof Models.WorkflowVersionEntity) {
						this.versionss.push(model);
					} else {
						this.versionss.push(new Models.WorkflowVersionEntity(model));
					}
				}
			}
			if (attributes.currentVersionId !== undefined) {
				this.currentVersionId = attributes.currentVersionId;
			}
			if (attributes.currentVersion !== undefined) {
				if (attributes.currentVersion === null) {
					this.currentVersion = attributes.currentVersion;
				} else {
					if (attributes.currentVersion instanceof Models.WorkflowVersionEntity) {
						this.currentVersion = attributes.currentVersion;
						this.currentVersionId = attributes.currentVersion.id;
					} else {
						this.currentVersion = new Models.WorkflowVersionEntity(attributes.currentVersion);
						this.currentVersionId = this.currentVersion.id;
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
		versionss {
			${Models.WorkflowVersionEntity.getAttributes().join('\n')}
		}
		currentVersion {
			${Models.WorkflowVersionEntity.getAttributes().join('\n')}
		}
	`;
	// % protected region % [Customize Default Expands here] end

	/**
	 * The save method that is called from the admin CRUD components.
	 */
	// % protected region % [Customize Save From Crud here] off begin
	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
			versionss: {},
		};
		return this.save(
			relationPath,
			{
				options: [
					{
						key: 'mergeReferences',
						graphQlType: '[String]',
						value: [
							'currentVersion',
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
CRUD(createdAttr)(WorkflowEntity.prototype, 'created');
CRUD(modifiedAttr)(WorkflowEntity.prototype, 'modified');
// % protected region % [Modify the create and modified CRUD attributes here] end
