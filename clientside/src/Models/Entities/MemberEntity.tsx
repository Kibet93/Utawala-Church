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
import moment from 'moment';
import { action, observable, runInAction } from 'mobx';
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
import { VisitorsMemberEntity } from 'Models/Security/Acl/VisitorsMemberEntity';
import { AdminMemberEntity } from 'Models/Security/Acl/AdminMemberEntity';
import { MemberMemberEntity } from 'Models/Security/Acl/MemberMemberEntity';
import { CategoryGroupLeaderMemberEntity } from 'Models/Security/Acl/CategoryGroupLeaderMemberEntity';
import { UsherMemberEntity } from 'Models/Security/Acl/UsherMemberEntity';
import { ProtocolMemberEntity } from 'Models/Security/Acl/ProtocolMemberEntity';
import { GroupCategoryMemberEntity } from 'Models/Security/Acl/GroupCategoryMemberEntity';
import * as Enums from '../Enums';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import { FormEntityData, FormEntityDataAttributes, getAllVersionsFn, getPublishedVersionFn } from 'Forms/FormEntityData';
import { FormVersion } from 'Forms/FormVersion';
import { fetchFormVersions, fetchPublishedVersion } from 'Forms/Forms';
import {SuperAdministratorScheme} from '../Security/Acl/SuperAdministratorScheme';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface IMemberEntityAttributes extends IModelAttributes, FormEntityDataAttributes {
	name: string;
	email: string;
	memberID: number;
	fullName: string;
	nationalID: string;
	residence: string;
	dateOfBirth: Date;
	age: number;
	categoryID: number;
	status: Enums.status;
	membershipStatus: Enums.membershipstatus;

	accountabilityGroupId?: string;
	accountabilityGroup?: Models.AccountabilityGroupEntity | Models.IAccountabilityGroupEntityAttributes;
	groupCategoryId?: string;
	groupCategory?: Models.GroupCategoryEntity | Models.IGroupCategoryEntityAttributes;
	homeFellowshipId?: string;
	homeFellowship?: Models.HomeFellowshipEntity | Models.IHomeFellowshipEntityAttributes;
	formPages: Array<Models.MemberEntityFormTileEntity | Models.IMemberEntityFormTileEntityAttributes>;
	categoryGroupLeader?: Models.CategoryGroupLeaderEntity | Models.ICategoryGroupLeaderEntityAttributes;
	protocol?: Models.ProtocolEntity | Models.IProtocolEntityAttributes;
	ushers?: Models.UsherEntity | Models.IUsherEntityAttributes;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('MemberEntity', 'MEMBER')
// % protected region % [Customise your entity metadata here] end
export default class MemberEntity extends Model implements IMemberEntityAttributes, FormEntityData  {
	public static acls: IAcl[] = [
		new SuperAdministratorScheme(),
		new VisitorsMemberEntity(),
		new AdminMemberEntity(),
		new MemberMemberEntity(),
		new CategoryGroupLeaderMemberEntity(),
		new UsherMemberEntity(),
		new ProtocolMemberEntity(),
		new GroupCategoryMemberEntity(),
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
	@Validators.Custom('Password Match', (e: string, target: MemberEntity) => {
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
	@Validators.Required()
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

	// % protected region % [Modify props to the crud options here for attribute 'Member ID'] off begin
	@Validators.Integer()
	@Validators.Unique()
	@observable
	@attribute()
	@CRUD({
		name: 'Member ID',
		displayType: 'textfield',
		order: 50,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public memberID: number;
	// % protected region % [Modify props to the crud options here for attribute 'Member ID'] end

	// % protected region % [Modify props to the crud options here for attribute 'Full Name'] off begin
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		name: 'Full Name',
		displayType: 'textfield',
		order: 60,
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
		order: 70,
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
		order: 80,
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
		order: 90,
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
		order: 100,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public age: number;
	// % protected region % [Modify props to the crud options here for attribute 'Age'] end

	// % protected region % [Modify props to the crud options here for attribute 'Category ID'] off begin
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'Category ID',
		displayType: 'textfield',
		order: 110,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public categoryID: number;
	// % protected region % [Modify props to the crud options here for attribute 'Category ID'] end

	// % protected region % [Modify props to the crud options here for attribute 'Status'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'Status',
		displayType: 'enum-combobox',
		order: 120,
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
		order: 130,
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

	@observable
	@attribute({isReference: true})
	public formVersions: FormVersion[] = [];

	@observable
	@attribute()
	public publishedVersionId?: string;

	@observable
	@attribute({isReference: true})
	public publishedVersion?: FormVersion;

	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Accountability Group'] off begin
		name: 'Accountability Group',
		displayType: 'reference-combobox',
		order: 140,
		referenceTypeFunc: () => Models.AccountabilityGroupEntity,
		// % protected region % [Modify props to the crud options here for reference 'Accountability Group'] end
	})
	public accountabilityGroupId?: string;
	@observable
	@attribute({isReference: true})
	public accountabilityGroup: Models.AccountabilityGroupEntity;

	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Group Category'] off begin
		name: 'Group Category',
		displayType: 'reference-combobox',
		order: 150,
		referenceTypeFunc: () => Models.GroupCategoryEntity,
		// % protected region % [Modify props to the crud options here for reference 'Group Category'] end
	})
	public groupCategoryId?: string;
	@observable
	@attribute({isReference: true})
	public groupCategory: Models.GroupCategoryEntity;

	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Home Fellowship'] off begin
		name: 'Home Fellowship',
		displayType: 'reference-combobox',
		order: 160,
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
		// % protected region % [Modify props to the crud options here for reference 'Form Page'] off begin
		name: "Form Pages",
		displayType: 'hidden',
		order: 170,
		referenceTypeFunc: () => Models.MemberEntityFormTileEntity,
		disableDefaultOptionRemoval: true,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'formPages',
			oppositeEntity: () => Models.MemberEntityFormTileEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'Form Page'] end
	})
	public formPages: Models.MemberEntityFormTileEntity[] = [];

	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Category Group Leader'] off begin
		name: 'Category Group Leader',
		displayType: 'displayfield',
		order: 180,
		inputProps: {
			displayFunction: (model?: Models.CategoryGroupLeaderEntity) => model ? model.getDisplayName() : null,
		},
		// % protected region % [Modify props to the crud options here for reference 'Category Group Leader'] end
	})
	public categoryGroupLeader?: Models.CategoryGroupLeaderEntity;

	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Protocol'] off begin
		name: 'Protocol',
		displayType: 'displayfield',
		order: 190,
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
		order: 200,
		inputProps: {
			displayFunction: (model?: Models.UsherEntity) => model ? model.getDisplayName() : null,
		},
		// % protected region % [Modify props to the crud options here for reference 'Ushers'] end
	})
	public ushers?: Models.UsherEntity;

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	// eslint-disable-next-line @typescript-eslint/no-useless-constructor
	constructor(attributes?: Partial<IMemberEntityAttributes>) {
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
	public assignAttributes(attributes?: Partial<IMemberEntityAttributes>) {
		// % protected region % [Override assign attributes here] off begin
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.email !== undefined) {
				this.email = attributes.email;
			}
			if (attributes.memberID !== undefined) {
				this.memberID = attributes.memberID;
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
			if (attributes.categoryID !== undefined) {
				this.categoryID = attributes.categoryID;
			}
			if (attributes.status !== undefined) {
				this.status = attributes.status;
			}
			if (attributes.membershipStatus !== undefined) {
				this.membershipStatus = attributes.membershipStatus;
			}
			if (attributes.publishedVersionId !== undefined) {
				this.publishedVersionId = attributes.publishedVersionId;
			}
			if (attributes.publishedVersion !== undefined) {
				if (attributes.publishedVersion === null) {
					this.publishedVersion = attributes.publishedVersion;
				} else {
					if (typeof attributes.publishedVersion.formData === 'string') {
						attributes.publishedVersion.formData = JSON.parse(attributes.publishedVersion.formData);
					}
					this.publishedVersion = attributes.publishedVersion;
					this.publishedVersionId = attributes.publishedVersion.id;
				}
			}
			if (attributes.formVersions !== undefined) {
				this.formVersions.push(...attributes.formVersions);
			}
			if (attributes.name !== undefined) {
				this.name = attributes.name;
			}
			if (attributes.accountabilityGroupId !== undefined) {
				this.accountabilityGroupId = attributes.accountabilityGroupId;
			}
			if (attributes.accountabilityGroup !== undefined) {
				if (attributes.accountabilityGroup === null) {
					this.accountabilityGroup = attributes.accountabilityGroup;
				} else {
					if (attributes.accountabilityGroup instanceof Models.AccountabilityGroupEntity) {
						this.accountabilityGroup = attributes.accountabilityGroup;
						this.accountabilityGroupId = attributes.accountabilityGroup.id;
					} else {
						this.accountabilityGroup = new Models.AccountabilityGroupEntity(attributes.accountabilityGroup);
						this.accountabilityGroupId = this.accountabilityGroup.id;
					}
				}
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
			if (attributes.formPages !== undefined && Array.isArray(attributes.formPages)) {
				for (const model of attributes.formPages) {
					if (model instanceof Models.MemberEntityFormTileEntity) {
						this.formPages.push(model);
					} else {
						this.formPages.push(new Models.MemberEntityFormTileEntity(model));
					}
				}
			}
			if (attributes.categoryGroupLeader !== undefined) {
				if (attributes.categoryGroupLeader === null) {
					this.categoryGroupLeader = attributes.categoryGroupLeader;
				} else {
					if (attributes.categoryGroupLeader instanceof Models.CategoryGroupLeaderEntity) {
						this.categoryGroupLeader = attributes.categoryGroupLeader;
					} else {
						this.categoryGroupLeader = new Models.CategoryGroupLeaderEntity(attributes.categoryGroupLeader);
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
		publishedVersion {
			id
			created
			modified
			formData
		}
		accountabilityGroup {
			${Models.AccountabilityGroupEntity.getAttributes().join('\n')}
		}
		groupCategory {
			${Models.GroupCategoryEntity.getAttributes().join('\n')}
		}
		homeFellowship {
			${Models.HomeFellowshipEntity.getAttributes().join('\n')}
		}
		formPages {
			${Models.MemberEntityFormTileEntity.getAttributes().join('\n')}
		}
		categoryGroupLeader {
			${Models.CategoryGroupLeaderEntity.getAttributes().join('\n')}
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
			formPages: {},
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

	/**
	 * Gets all the versions for this form.
	 */
	public getAllVersions: getAllVersionsFn = (includeSubmissions?, conditions?) => {
		// % protected region % [Modify the getAllVersionsFn here] off begin
		return fetchFormVersions(this, includeSubmissions, conditions)
			.then(d => {
				runInAction(() => this.formVersions = d);
				return d.map(x => x.formData)
			});
		// % protected region % [Modify the getAllVersionsFn here] end
	};

	/**
	 * Gets the published version for this form.
	 */
	public getPublishedVersion: getPublishedVersionFn = includeSubmissions => {
		// % protected region % [Modify the getPublishedVersionFn here] off begin
		return fetchPublishedVersion(this, includeSubmissions)
			.then(d => {
				runInAction(() => this.publishedVersion = d);
				return d ? d.formData : undefined;
			});
		// % protected region % [Modify the getPublishedVersionFn here] end
	};

	/**
	 * Gets the submission entity type for this form.
	 */
	public getSubmissionEntity = () => {
		// % protected region % [Modify the getSubmissionEntity here] off begin
		return Models.MemberSubmissionEntity;
		// % protected region % [Modify the getSubmissionEntity here] end
	}


	// % protected region % [Add any further custom model features here] off begin
	// % protected region % [Add any further custom model features here] end
}

// % protected region % [Modify the create and modified CRUD attributes here] off begin
/*
 * Retrieve the created and modified CRUD attributes for defining the CRUD views and decorate the class with them.
 */
const [ createdAttr, modifiedAttr ] = getCreatedModifiedCrudOptions();
CRUD(createdAttr)(MemberEntity.prototype, 'created');
CRUD(modifiedAttr)(MemberEntity.prototype, 'modified');
// % protected region % [Modify the create and modified CRUD attributes here] end
