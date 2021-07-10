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
import * as React from 'react';
import { SERVER_URL } from 'Constants';
import moment from 'moment';
import { action, observable } from 'mobx';
import { Model, IModelAttributes, attribute, entity } from 'Models/Model';
import * as Models from 'Models/Entities';
import * as Validators from 'Validators';
import { CRUD } from '../CRUDOptions';
import * as AttrUtils from "Util/AttributeUtils";
import { IAcl } from 'Models/Security/IAcl';
import {
	makeFetchOneToManyFunc,
	makeEnumFetchFunction,
	getCreatedModifiedCrudOptions,
} from 'Util/EntityUtils';
import { VisitorsMembersEntity } from 'Models/Security/Acl/VisitorsMembersEntity';
import { AdminMembersEntity } from 'Models/Security/Acl/AdminMembersEntity';
import { MembersMembersEntity } from 'Models/Security/Acl/MembersMembersEntity';
import { CategoryLeadersMembersEntity } from 'Models/Security/Acl/CategoryLeadersMembersEntity';
import { UsherMembersEntity } from 'Models/Security/Acl/UsherMembersEntity';
import { ProtocolMembersEntity } from 'Models/Security/Acl/ProtocolMembersEntity';
import * as Enums from '../Enums';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import {SuperAdministratorScheme} from '../Security/Acl/SuperAdministratorScheme';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface IMembersEntityAttributes extends IModelAttributes {
	email: string;
	fullName: string;
	nationalID: string;
	residence: string;
	dateOfBirth: Date;
	age: number;
	status: Enums.status;
	membershipStatus: Enums.membershipstatus;
	categoryChoice: Enums.categoryGroups;
	accountabilityGrp: number;
	pictureId: string;
	picture: Blob;

	accountabilityGroupId?: string;
	accountabilityGroup?: Models.AccountabilityGroupsEntity | Models.IAccountabilityGroupsEntityAttributes;
	homeFellowshipId?: string;
	homeFellowship?: Models.HomeFellowshipEntity | Models.IHomeFellowshipEntityAttributes;
	categoryGroupLeader?: Models.CategoryLeadersEntity | Models.ICategoryLeadersEntityAttributes;
	protocol?: Models.ProtocolEntity | Models.IProtocolEntityAttributes;
	ushers?: Models.UsherEntity | Models.IUsherEntityAttributes;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('MembersEntity', 'MEMBERS')
// % protected region % [Customise your entity metadata here] end
export default class MembersEntity extends Model implements IMembersEntityAttributes {
	public static acls: IAcl[] = [
		new SuperAdministratorScheme(),
		new VisitorsMembersEntity(),
		new AdminMembersEntity(),
		new MembersMembersEntity(),
		new CategoryLeadersMembersEntity(),
		new UsherMembersEntity(),
		new ProtocolMembersEntity(),
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
	@Validators.Custom('Password Match', (e: string, target: MembersEntity) => {
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

	// % protected region % [Modify props to the crud options here for attribute 'Full Name'] off begin
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		name: 'Full Name',
		displayType: 'textfield',
		order: 40,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public fullName: string;
	// % protected region % [Modify props to the crud options here for attribute 'Full Name'] end

	// % protected region % [Modify props to the crud options here for attribute 'National ID'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'National ID',
		displayType: 'textfield',
		order: 50,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public nationalID: string;
	// % protected region % [Modify props to the crud options here for attribute 'National ID'] end

	// % protected region % [Modify props to the crud options here for attribute 'Residence'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'Residence',
		displayType: 'textfield',
		order: 60,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public residence: string;
	// % protected region % [Modify props to the crud options here for attribute 'Residence'] end

	// % protected region % [Modify props to the crud options here for attribute 'Date of Birth'] off begin
	/**
	 * For age Calculation
	 */
	@observable
	@attribute()
	@CRUD({
		name: 'Date of Birth',
		displayType: 'datepicker',
		order: 70,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseDate,
	})
	public dateOfBirth: Date;
	// % protected region % [Modify props to the crud options here for attribute 'Date of Birth'] end

	// % protected region % [Modify props to the crud options here for attribute 'Age'] off begin
	/**
	 * Current Age
	 */
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'Age',
		displayType: 'textfield',
		order: 80,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public age: number;
	// % protected region % [Modify props to the crud options here for attribute 'Age'] end

	// % protected region % [Modify props to the crud options here for attribute 'Status'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'Status',
		displayType: 'enum-combobox',
		order: 90,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: (attr: string) => {
			return AttrUtils.standardiseEnum(attr, Enums.statusOptions);
		},
		enumResolveFunction: makeEnumFetchFunction(Enums.statusOptions),
		displayFunction: (attribute: Enums.status) => Enums.statusOptions[attribute],
	})
	public status: Enums.status;
	// % protected region % [Modify props to the crud options here for attribute 'Status'] end

	// % protected region % [Modify props to the crud options here for attribute 'Membership Status'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'Membership Status',
		displayType: 'enum-combobox',
		order: 100,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: (attr: string) => {
			return AttrUtils.standardiseEnum(attr, Enums.membershipstatusOptions);
		},
		enumResolveFunction: makeEnumFetchFunction(Enums.membershipstatusOptions),
		displayFunction: (attribute: Enums.membershipstatus) => Enums.membershipstatusOptions[attribute],
	})
	public membershipStatus: Enums.membershipstatus;
	// % protected region % [Modify props to the crud options here for attribute 'Membership Status'] end

	// % protected region % [Modify props to the crud options here for attribute 'Category Choice'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'Category Choice',
		displayType: 'enum-combobox',
		order: 110,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: (attr: string) => {
			return AttrUtils.standardiseEnum(attr, Enums.categoryGroupsOptions);
		},
		enumResolveFunction: makeEnumFetchFunction(Enums.categoryGroupsOptions),
		displayFunction: (attribute: Enums.categoryGroups) => Enums.categoryGroupsOptions[attribute],
	})
	public categoryChoice: Enums.categoryGroups;
	// % protected region % [Modify props to the crud options here for attribute 'Category Choice'] end

	// % protected region % [Modify props to the crud options here for attribute 'Accountability Grp'] off begin
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'Accountability Grp',
		displayType: 'textfield',
		order: 120,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public accountabilityGrp: number;
	// % protected region % [Modify props to the crud options here for attribute 'Accountability Grp'] end

	// % protected region % [Modify props to the crud options here for attribute 'Picture'] off begin
	/**
	 * Profile Picture
	 */
	@observable
	@attribute({file: 'picture'})
	@CRUD({
		name: 'Picture',
		displayType: 'file',
		order: 130,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseUuid,
		inputProps: {
			imageOnly: true,
		},
		fileAttribute: 'picture',
		displayFunction: attr => attr 
			? <img src={`${SERVER_URL}/api/files/${attr}`} alt='Profile Picture' style={{maxWidth: '300px'}} />
			: 'No File Attached',
	})
	public pictureId: string;
	@observable
	public picture: Blob;
	// % protected region % [Modify props to the crud options here for attribute 'Picture'] end

	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Accountability Group'] off begin
		name: 'Accountability Group',
		displayType: 'reference-combobox',
		order: 140,
		referenceTypeFunc: () => Models.AccountabilityGroupsEntity,
		// % protected region % [Modify props to the crud options here for reference 'Accountability Group'] end
	})
	public accountabilityGroupId?: string;
	@observable
	@attribute({isReference: true})
	public accountabilityGroup: Models.AccountabilityGroupsEntity;

	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Home Fellowship'] off begin
		name: 'Home Fellowship',
		displayType: 'reference-combobox',
		order: 150,
		referenceTypeFunc: () => Models.HomeFellowshipEntity,
		// % protected region % [Modify props to the crud options here for reference 'Home Fellowship'] end
	})
	public homeFellowshipId?: string;
	@observable
	@attribute({isReference: true})
	public homeFellowship: Models.HomeFellowshipEntity;

	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Category Group Leader'] off begin
		name: 'Category Group Leader',
		displayType: 'displayfield',
		order: 160,
		inputProps: {
			displayFunction: (model?: Models.CategoryLeadersEntity) => model ? model.getDisplayName() : null,
		},
		// % protected region % [Modify props to the crud options here for reference 'Category Group Leader'] end
	})
	public categoryGroupLeader?: Models.CategoryLeadersEntity;

	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Protocol'] off begin
		name: 'Protocol',
		displayType: 'displayfield',
		order: 170,
		inputProps: {
			displayFunction: (model?: Models.ProtocolEntity) => model ? model.getDisplayName() : null,
		},
		// % protected region % [Modify props to the crud options here for reference 'Protocol'] end
	})
	public protocol?: Models.ProtocolEntity;

	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Ushers'] off begin
		name: 'Ushers',
		displayType: 'displayfield',
		order: 180,
		inputProps: {
			displayFunction: (model?: Models.UsherEntity) => model ? model.getDisplayName() : null,
		},
		// % protected region % [Modify props to the crud options here for reference 'Ushers'] end
	})
	public ushers?: Models.UsherEntity;

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	// eslint-disable-next-line @typescript-eslint/no-useless-constructor
	constructor(attributes?: Partial<IMembersEntityAttributes>) {
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
	public assignAttributes(attributes?: Partial<IMembersEntityAttributes>) {
		// % protected region % [Override assign attributes here] off begin
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.email !== undefined) {
				this.email = attributes.email;
			}
			if (attributes.fullName !== undefined) {
				this.fullName = attributes.fullName;
			}
			if (attributes.nationalID !== undefined) {
				this.nationalID = attributes.nationalID;
			}
			if (attributes.residence !== undefined) {
				this.residence = attributes.residence;
			}
			if (attributes.dateOfBirth !== undefined) {
				if (attributes.dateOfBirth === null) {
					this.dateOfBirth = attributes.dateOfBirth;
				} else {
					this.dateOfBirth = moment(attributes.dateOfBirth).toDate();
				}
			}
			if (attributes.age !== undefined) {
				this.age = attributes.age;
			}
			if (attributes.status !== undefined) {
				this.status = attributes.status;
			}
			if (attributes.membershipStatus !== undefined) {
				this.membershipStatus = attributes.membershipStatus;
			}
			if (attributes.categoryChoice !== undefined) {
				this.categoryChoice = attributes.categoryChoice;
			}
			if (attributes.accountabilityGrp !== undefined) {
				this.accountabilityGrp = attributes.accountabilityGrp;
			}
			if (attributes.picture !== undefined) {
				this.picture = attributes.picture;
			}
			if (attributes.pictureId !== undefined) {
				this.pictureId = attributes.pictureId;
			}
			if (attributes.accountabilityGroupId !== undefined) {
				this.accountabilityGroupId = attributes.accountabilityGroupId;
			}
			if (attributes.accountabilityGroup !== undefined) {
				if (attributes.accountabilityGroup === null) {
					this.accountabilityGroup = attributes.accountabilityGroup;
				} else {
					if (attributes.accountabilityGroup instanceof Models.AccountabilityGroupsEntity) {
						this.accountabilityGroup = attributes.accountabilityGroup;
						this.accountabilityGroupId = attributes.accountabilityGroup.id;
					} else {
						this.accountabilityGroup = new Models.AccountabilityGroupsEntity(attributes.accountabilityGroup);
						this.accountabilityGroupId = this.accountabilityGroup.id;
					}
				}
			}
			if (attributes.homeFellowshipId !== undefined) {
				this.homeFellowshipId = attributes.homeFellowshipId;
			}
			if (attributes.homeFellowship !== undefined) {
				if (attributes.homeFellowship === null) {
					this.homeFellowship = attributes.homeFellowship;
				} else {
					if (attributes.homeFellowship instanceof Models.HomeFellowshipEntity) {
						this.homeFellowship = attributes.homeFellowship;
						this.homeFellowshipId = attributes.homeFellowship.id;
					} else {
						this.homeFellowship = new Models.HomeFellowshipEntity(attributes.homeFellowship);
						this.homeFellowshipId = this.homeFellowship.id;
					}
				}
			}
			if (attributes.categoryGroupLeader !== undefined) {
				if (attributes.categoryGroupLeader === null) {
					this.categoryGroupLeader = attributes.categoryGroupLeader;
				} else {
					if (attributes.categoryGroupLeader instanceof Models.CategoryLeadersEntity) {
						this.categoryGroupLeader = attributes.categoryGroupLeader;
					} else {
						this.categoryGroupLeader = new Models.CategoryLeadersEntity(attributes.categoryGroupLeader);
					}
				}
			}
			if (attributes.protocol !== undefined) {
				if (attributes.protocol === null) {
					this.protocol = attributes.protocol;
				} else {
					if (attributes.protocol instanceof Models.ProtocolEntity) {
						this.protocol = attributes.protocol;
					} else {
						this.protocol = new Models.ProtocolEntity(attributes.protocol);
					}
				}
			}
			if (attributes.ushers !== undefined) {
				if (attributes.ushers === null) {
					this.ushers = attributes.ushers;
				} else {
					if (attributes.ushers instanceof Models.UsherEntity) {
						this.ushers = attributes.ushers;
					} else {
						this.ushers = new Models.UsherEntity(attributes.ushers);
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
		accountabilityGroup {
			${Models.AccountabilityGroupsEntity.getAttributes().join('\n')}
		}
		homeFellowship {
			${Models.HomeFellowshipEntity.getAttributes().join('\n')}
		}
		categoryGroupLeader {
			${Models.CategoryLeadersEntity.getAttributes().join('\n')}
		}
		protocol {
			${Models.ProtocolEntity.getAttributes().join('\n')}
		}
		ushers {
			${Models.UsherEntity.getAttributes().join('\n')}
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
						]
					},
				],
				contentType: 'multipart/form-data',
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
CRUD(createdAttr)(MembersEntity.prototype, 'created');
CRUD(modifiedAttr)(MembersEntity.prototype, 'modified');
// % protected region % [Modify the create and modified CRUD attributes here] end
