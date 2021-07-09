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
import { VisitorsCategoryGroupLeaderEntity } from 'Models/Security/Acl/VisitorsCategoryGroupLeaderEntity';
import { AdminCategoryGroupLeaderEntity } from 'Models/Security/Acl/AdminCategoryGroupLeaderEntity';
import { MemberCategoryGroupLeaderEntity } from 'Models/Security/Acl/MemberCategoryGroupLeaderEntity';
import { CategoryGroupLeaderCategoryGroupLeaderEntity } from 'Models/Security/Acl/CategoryGroupLeaderCategoryGroupLeaderEntity';
import { UsherCategoryGroupLeaderEntity } from 'Models/Security/Acl/UsherCategoryGroupLeaderEntity';
import { ProtocolCategoryGroupLeaderEntity } from 'Models/Security/Acl/ProtocolCategoryGroupLeaderEntity';
import { GroupCategoryCategoryGroupLeaderEntity } from 'Models/Security/Acl/GroupCategoryCategoryGroupLeaderEntity';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import {SuperAdministratorScheme} from '../Security/Acl/SuperAdministratorScheme';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface ICategoryGroupLeaderEntityAttributes extends IModelAttributes {
	email: string;
	memberID: number;
	categoryID: number;
	groupName: string;

	groupCategoryId: string;
	groupCategory: Models.GroupCategoryEntity | Models.IGroupCategoryEntityAttributes;
	memberId: string;
	member: Models.MemberEntity | Models.IMemberEntityAttributes;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('CategoryGroupLeaderEntity', 'Category Group Leader')
// % protected region % [Customise your entity metadata here] end
export default class CategoryGroupLeaderEntity extends Model implements ICategoryGroupLeaderEntityAttributes {
	public static acls: IAcl[] = [
		new SuperAdministratorScheme(),
		new VisitorsCategoryGroupLeaderEntity(),
		new AdminCategoryGroupLeaderEntity(),
		new MemberCategoryGroupLeaderEntity(),
		new CategoryGroupLeaderCategoryGroupLeaderEntity(),
		new UsherCategoryGroupLeaderEntity(),
		new ProtocolCategoryGroupLeaderEntity(),
		new GroupCategoryCategoryGroupLeaderEntity(),
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
	@Validators.Custom('Password Match', (e: string, target: CategoryGroupLeaderEntity) => {
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

	// % protected region % [Modify props to the crud options here for attribute 'Member ID'] off begin
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'Member ID',
		displayType: 'textfield',
		order: 40,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public memberID: number;
	// % protected region % [Modify props to the crud options here for attribute 'Member ID'] end

	// % protected region % [Modify props to the crud options here for attribute 'Category ID'] off begin
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'Category ID',
		displayType: 'textfield',
		order: 50,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public categoryID: number;
	// % protected region % [Modify props to the crud options here for attribute 'Category ID'] end

	// % protected region % [Modify props to the crud options here for attribute 'Group Name'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'Group Name',
		displayType: 'textfield',
		order: 60,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public groupName: string;
	// % protected region % [Modify props to the crud options here for attribute 'Group Name'] end

	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Group Category'] off begin
		name: 'Group Category',
		displayType: 'reference-combobox',
		order: 70,
		referenceTypeFunc: () => Models.GroupCategoryEntity,
		// % protected region % [Modify props to the crud options here for reference 'Group Category'] end
	})
	public groupCategoryId: string;
	@observable
	@attribute({isReference: true})
	public groupCategory: Models.GroupCategoryEntity;

	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'MEMBER'] off begin
		name: 'MEMBER',
		displayType: 'reference-combobox',
		order: 80,
		referenceTypeFunc: () => Models.MemberEntity,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'members',
			oppositeEntity: () => Models.MemberEntity,
		})
		// % protected region % [Modify props to the crud options here for reference 'MEMBER'] end
	})
	public memberId: string;
	@observable
	@attribute({isReference: true})
	public member: Models.MemberEntity;

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	// eslint-disable-next-line @typescript-eslint/no-useless-constructor
	constructor(attributes?: Partial<ICategoryGroupLeaderEntityAttributes>) {
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
	public assignAttributes(attributes?: Partial<ICategoryGroupLeaderEntityAttributes>) {
		// % protected region % [Override assign attributes here] off begin
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.email !== undefined) {
				this.email = attributes.email;
			}
			if (attributes.memberID !== undefined) {
				this.memberID = attributes.memberID;
			}
			if (attributes.categoryID !== undefined) {
				this.categoryID = attributes.categoryID;
			}
			if (attributes.groupName !== undefined) {
				this.groupName = attributes.groupName;
			}
			if (attributes.groupCategoryId !== undefined) {
				this.groupCategoryId = attributes.groupCategoryId;
			}
			if (attributes.groupCategory !== undefined) {
				if (attributes.groupCategory === null) {
					this.groupCategory = attributes.groupCategory;
				} else {
					if (attributes.groupCategory instanceof Models.GroupCategoryEntity) {
						this.groupCategory = attributes.groupCategory;
						this.groupCategoryId = attributes.groupCategory.id;
					} else {
						this.groupCategory = new Models.GroupCategoryEntity(attributes.groupCategory);
						this.groupCategoryId = this.groupCategory.id;
					}
				}
			}
			if (attributes.memberId !== undefined) {
				this.memberId = attributes.memberId;
			}
			if (attributes.member !== undefined) {
				if (attributes.member === null) {
					this.member = attributes.member;
				} else {
					if (attributes.member instanceof Models.MemberEntity) {
						this.member = attributes.member;
						this.memberId = attributes.member.id;
					} else {
						this.member = new Models.MemberEntity(attributes.member);
						this.memberId = this.member.id;
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
		groupCategory {
			${Models.GroupCategoryEntity.getAttributes().join('\n')}
		}
		member {
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
							'member',
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
CRUD(createdAttr)(CategoryGroupLeaderEntity.prototype, 'created');
CRUD(modifiedAttr)(CategoryGroupLeaderEntity.prototype, 'modified');
// % protected region % [Modify the create and modified CRUD attributes here] end
