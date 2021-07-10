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
import { VisitorsAccountabilityGroupsEntity } from 'Models/Security/Acl/VisitorsAccountabilityGroupsEntity';
import { AdminAccountabilityGroupsEntity } from 'Models/Security/Acl/AdminAccountabilityGroupsEntity';
import { MembersAccountabilityGroupsEntity } from 'Models/Security/Acl/MembersAccountabilityGroupsEntity';
import { CategoryLeadersAccountabilityGroupsEntity } from 'Models/Security/Acl/CategoryLeadersAccountabilityGroupsEntity';
import { UsherAccountabilityGroupsEntity } from 'Models/Security/Acl/UsherAccountabilityGroupsEntity';
import { ProtocolAccountabilityGroupsEntity } from 'Models/Security/Acl/ProtocolAccountabilityGroupsEntity';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import {SuperAdministratorScheme} from '../Security/Acl/SuperAdministratorScheme';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface IAccountabilityGroupsEntityAttributes extends IModelAttributes {
	name: string;
	category: number;
	leaderID: number;

	membersaccountabilitygroups: Array<Models.MembersEntity | Models.IMembersEntityAttributes>;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('AccountabilityGroupsEntity', 'Accountability Groups')
// % protected region % [Customise your entity metadata here] end
export default class AccountabilityGroupsEntity extends Model implements IAccountabilityGroupsEntityAttributes {
	public static acls: IAcl[] = [
		new SuperAdministratorScheme(),
		new VisitorsAccountabilityGroupsEntity(),
		new AdminAccountabilityGroupsEntity(),
		new MembersAccountabilityGroupsEntity(),
		new CategoryLeadersAccountabilityGroupsEntity(),
		new UsherAccountabilityGroupsEntity(),
		new ProtocolAccountabilityGroupsEntity(),
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

	// % protected region % [Modify props to the crud options here for attribute 'Category'] off begin
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'Category',
		displayType: 'textfield',
		order: 20,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public category: number;
	// % protected region % [Modify props to the crud options here for attribute 'Category'] end

	// % protected region % [Modify props to the crud options here for attribute 'Leader ID'] off begin
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'Leader ID',
		displayType: 'textfield',
		order: 30,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public leaderID: number;
	// % protected region % [Modify props to the crud options here for attribute 'Leader ID'] end

	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'MEMBERsAccountabilityGroup'] off begin
		name: "MEMBERsAccountabilityGroups",
		displayType: 'reference-multicombobox',
		order: 40,
		referenceTypeFunc: () => Models.MembersEntity,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'membersaccountabilitygroups',
			oppositeEntity: () => Models.MembersEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'MEMBERsAccountabilityGroup'] end
	})
	public membersaccountabilitygroups: Models.MembersEntity[] = [];

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	// eslint-disable-next-line @typescript-eslint/no-useless-constructor
	constructor(attributes?: Partial<IAccountabilityGroupsEntityAttributes>) {
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
	public assignAttributes(attributes?: Partial<IAccountabilityGroupsEntityAttributes>) {
		// % protected region % [Override assign attributes here] off begin
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.name !== undefined) {
				this.name = attributes.name;
			}
			if (attributes.category !== undefined) {
				this.category = attributes.category;
			}
			if (attributes.leaderID !== undefined) {
				this.leaderID = attributes.leaderID;
			}
			if (attributes.membersaccountabilitygroups !== undefined && Array.isArray(attributes.membersaccountabilitygroups)) {
				for (const model of attributes.membersaccountabilitygroups) {
					if (model instanceof Models.MembersEntity) {
						this.membersaccountabilitygroups.push(model);
					} else {
						this.membersaccountabilitygroups.push(new Models.MembersEntity(model));
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
		membersaccountabilitygroups {
			${Models.MembersEntity.getAttributes().join('\n')}
			${Models.MembersEntity.getFiles().map(f => f.name).join('\n')}
		}
	`;
	// % protected region % [Customize Default Expands here] end

	/**
	 * The save method that is called from the admin CRUD components.
	 */
	// % protected region % [Customize Save From Crud here] off begin
	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
			membersaccountabilitygroups: {},
		};
		return this.save(
			relationPath,
			{
				options: [
					{
						key: 'mergeReferences',
						graphQlType: '[String]',
						value: [
							'membersaccountabilitygroups',
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
CRUD(createdAttr)(AccountabilityGroupsEntity.prototype, 'created');
CRUD(modifiedAttr)(AccountabilityGroupsEntity.prototype, 'modified');
// % protected region % [Modify the create and modified CRUD attributes here] end
