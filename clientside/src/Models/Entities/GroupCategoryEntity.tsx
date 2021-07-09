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
import { VisitorsGroupCategoryEntity } from 'Models/Security/Acl/VisitorsGroupCategoryEntity';
import { AdminGroupCategoryEntity } from 'Models/Security/Acl/AdminGroupCategoryEntity';
import { MemberGroupCategoryEntity } from 'Models/Security/Acl/MemberGroupCategoryEntity';
import { CategoryGroupLeaderGroupCategoryEntity } from 'Models/Security/Acl/CategoryGroupLeaderGroupCategoryEntity';
import { UsherGroupCategoryEntity } from 'Models/Security/Acl/UsherGroupCategoryEntity';
import { ProtocolGroupCategoryEntity } from 'Models/Security/Acl/ProtocolGroupCategoryEntity';
import { GroupCategoryGroupCategoryEntity } from 'Models/Security/Acl/GroupCategoryGroupCategoryEntity';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import {SuperAdministratorScheme} from '../Security/Acl/SuperAdministratorScheme';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface IGroupCategoryEntityAttributes extends IModelAttributes {
	email: string;
	name: string;
	leaderID: number;

	categoryGroupLeaderss: Array<Models.CategoryGroupLeaderEntity | Models.ICategoryGroupLeaderEntityAttributes>;
	memberscategoriess: Array<Models.MemberEntity | Models.IMemberEntityAttributes>;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('GroupCategoryEntity', 'Group Category')
// % protected region % [Customise your entity metadata here] end
export default class GroupCategoryEntity extends Model implements IGroupCategoryEntityAttributes {
	public static acls: IAcl[] = [
		new SuperAdministratorScheme(),
		new VisitorsGroupCategoryEntity(),
		new AdminGroupCategoryEntity(),
		new MemberGroupCategoryEntity(),
		new CategoryGroupLeaderGroupCategoryEntity(),
		new UsherGroupCategoryEntity(),
		new ProtocolGroupCategoryEntity(),
		new GroupCategoryGroupCategoryEntity(),
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
		'email',
		// % protected region % [Add any custom update exclusions here] off begin
		// % protected region % [Add any custom update exclusions here] end
	];

	// % protected region % [Modify props to the crud options here for attribute 'Email'] off begin
	@Validators.Email()
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		name: 'Email',
		displayType: 'displayfield',
		order: 10,
		createFieldType: 'textfield',
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public email: string;
	// % protected region % [Modify props to the crud options here for attribute 'Email'] end

	// % protected region % [Modify props to the crud options here for attribute 'Password'] off begin
	@Validators.Length(6)
	@observable
	@CRUD({
		name: 'Password',
		displayType: 'hidden',
		order: 20,
		createFieldType: 'password',
	})
	public password: string;
	// % protected region % [Modify props to the crud options here for attribute 'Password'] end

	// % protected region % [Modify props to the crud options here for attribute 'Confirm Password'] off begin
	@Validators.Custom('Password Match', (e: string, target: GroupCategoryEntity) => {
		return new Promise(resolve => resolve(target.password !== e ? "Password fields do not match" : null))
	})
	@observable
	@CRUD({
		name: 'Confirm Password',
		displayType: 'hidden',
		order: 30,
		createFieldType: 'password',
	})
	public _confirmPassword: string;
	// % protected region % [Modify props to the crud options here for attribute 'Confirm Password'] end

	// % protected region % [Modify props to the crud options here for attribute 'Name'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'Name',
		displayType: 'textfield',
		order: 40,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public name: string;
	// % protected region % [Modify props to the crud options here for attribute 'Name'] end

	// % protected region % [Modify props to the crud options here for attribute 'Leader ID'] off begin
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'Leader ID',
		displayType: 'textfield',
		order: 50,
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
		// % protected region % [Modify props to the crud options here for reference 'Category Group Leaders'] off begin
		name: "Category Group Leaderss",
		displayType: 'reference-multicombobox',
		order: 60,
		referenceTypeFunc: () => Models.CategoryGroupLeaderEntity,
		disableDefaultOptionRemoval: true,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'categoryGroupLeaderss',
			oppositeEntity: () => Models.CategoryGroupLeaderEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'Category Group Leaders'] end
	})
	public categoryGroupLeaderss: Models.CategoryGroupLeaderEntity[] = [];

	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'MEMBERsCategories'] off begin
		name: "MEMBERsCategoriess",
		displayType: 'reference-multicombobox',
		order: 70,
		referenceTypeFunc: () => Models.MemberEntity,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'memberscategoriess',
			oppositeEntity: () => Models.MemberEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'MEMBERsCategories'] end
	})
	public memberscategoriess: Models.MemberEntity[] = [];

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	// eslint-disable-next-line @typescript-eslint/no-useless-constructor
	constructor(attributes?: Partial<IGroupCategoryEntityAttributes>) {
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
	public assignAttributes(attributes?: Partial<IGroupCategoryEntityAttributes>) {
		// % protected region % [Override assign attributes here] off begin
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.email !== undefined) {
				this.email = attributes.email;
			}
			if (attributes.name !== undefined) {
				this.name = attributes.name;
			}
			if (attributes.leaderID !== undefined) {
				this.leaderID = attributes.leaderID;
			}
			if (attributes.categoryGroupLeaderss !== undefined && Array.isArray(attributes.categoryGroupLeaderss)) {
				for (const model of attributes.categoryGroupLeaderss) {
					if (model instanceof Models.CategoryGroupLeaderEntity) {
						this.categoryGroupLeaderss.push(model);
					} else {
						this.categoryGroupLeaderss.push(new Models.CategoryGroupLeaderEntity(model));
					}
				}
			}
			if (attributes.memberscategoriess !== undefined && Array.isArray(attributes.memberscategoriess)) {
				for (const model of attributes.memberscategoriess) {
					if (model instanceof Models.MemberEntity) {
						this.memberscategoriess.push(model);
					} else {
						this.memberscategoriess.push(new Models.MemberEntity(model));
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
		categoryGroupLeaderss {
			${Models.CategoryGroupLeaderEntity.getAttributes().join('\n')}
		}
		memberscategoriess {
			${Models.MemberEntity.getAttributes().join('\n')}
		}
	`;
	// % protected region % [Customize Default Expands here] end

	/**
	 * The save method that is called from the admin CRUD components.
	 */
	// % protected region % [Customize Save From Crud here] off begin
	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
			categoryGroupLeaderss: {},
			memberscategoriess: {},
		};

		if (formMode === 'create') {
			relationPath['password'] = {};

			if (this.password !== this._confirmPassword) {
				throw Error("Password fields do not match");
			}
		}
		return this.save(
			relationPath,
			{
				graphQlInputType: formMode === 'create'
					? `[${this.getModelName()}CreateInput]`
					: `[${this.getModelName()}Input]`,
				options: [
					{
						key: 'mergeReferences',
						graphQlType: '[String]',
						value: [
							'memberscategoriess',
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
		return this.email;
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
CRUD(createdAttr)(GroupCategoryEntity.prototype, 'created');
CRUD(modifiedAttr)(GroupCategoryEntity.prototype, 'modified');
// % protected region % [Modify the create and modified CRUD attributes here] end
