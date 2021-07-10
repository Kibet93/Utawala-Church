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
	getCreatedModifiedCrudOptions,
} from 'Util/EntityUtils';
import { VisitorsAttendanceEntity } from 'Models/Security/Acl/VisitorsAttendanceEntity';
import { AdminAttendanceEntity } from 'Models/Security/Acl/AdminAttendanceEntity';
import { MembersAttendanceEntity } from 'Models/Security/Acl/MembersAttendanceEntity';
import { CategoryLeadersAttendanceEntity } from 'Models/Security/Acl/CategoryLeadersAttendanceEntity';
import { UsherAttendanceEntity } from 'Models/Security/Acl/UsherAttendanceEntity';
import { ProtocolAttendanceEntity } from 'Models/Security/Acl/ProtocolAttendanceEntity';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import { FormEntityData, FormEntityDataAttributes, getAllVersionsFn, getPublishedVersionFn } from 'Forms/FormEntityData';
import { FormVersion } from 'Forms/FormVersion';
import { fetchFormVersions, fetchPublishedVersion } from 'Forms/Forms';
import {SuperAdministratorScheme} from '../Security/Acl/SuperAdministratorScheme';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface IAttendanceEntityAttributes extends IModelAttributes, FormEntityDataAttributes {
	name: string;
	dateOfService: Date;
	serviceID: number;
	seatNoID: number;
	temperature: number;
	attendedService: boolean;
	reasonForNotAttending: string;
	comment: string;

	formPages: Array<Models.AttendanceEntityFormTileEntity | Models.IAttendanceEntityFormTileEntityAttributes>;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('AttendanceEntity', 'Attendance')
// % protected region % [Customise your entity metadata here] end
export default class AttendanceEntity extends Model implements IAttendanceEntityAttributes, FormEntityData  {
	public static acls: IAcl[] = [
		new SuperAdministratorScheme(),
		new VisitorsAttendanceEntity(),
		new AdminAttendanceEntity(),
		new MembersAttendanceEntity(),
		new CategoryLeadersAttendanceEntity(),
		new UsherAttendanceEntity(),
		new ProtocolAttendanceEntity(),
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
	@Validators.Required()
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

	// % protected region % [Modify props to the crud options here for attribute 'Date Of Service'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'Date Of Service',
		displayType: 'datepicker',
		order: 20,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseDate,
	})
	public dateOfService: Date;
	// % protected region % [Modify props to the crud options here for attribute 'Date Of Service'] end

	// % protected region % [Modify props to the crud options here for attribute 'Service ID'] off begin
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'Service ID',
		displayType: 'textfield',
		order: 30,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public serviceID: number;
	// % protected region % [Modify props to the crud options here for attribute 'Service ID'] end

	// % protected region % [Modify props to the crud options here for attribute 'Seat No ID'] off begin
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'Seat No ID',
		displayType: 'textfield',
		order: 40,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public seatNoID: number;
	// % protected region % [Modify props to the crud options here for attribute 'Seat No ID'] end

	// % protected region % [Modify props to the crud options here for attribute 'Temperature'] off begin
	@Validators.Numeric()
	@observable
	@attribute()
	@CRUD({
		name: 'Temperature',
		displayType: 'textfield',
		order: 50,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseFloat,
	})
	public temperature: number;
	// % protected region % [Modify props to the crud options here for attribute 'Temperature'] end

	// % protected region % [Modify props to the crud options here for attribute 'Attended Service'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'Attended Service',
		displayType: 'checkbox',
		order: 60,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseBoolean,
		displayFunction: attr => attr ? 'True' : 'False',
	})
	public attendedService: boolean;
	// % protected region % [Modify props to the crud options here for attribute 'Attended Service'] end

	// % protected region % [Modify props to the crud options here for attribute 'Reason For Not Attending'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'Reason For Not Attending',
		displayType: 'textfield',
		order: 70,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public reasonForNotAttending: string;
	// % protected region % [Modify props to the crud options here for attribute 'Reason For Not Attending'] end

	// % protected region % [Modify props to the crud options here for attribute 'Comment'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'Comment',
		displayType: 'textarea',
		order: 80,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public comment: string;
	// % protected region % [Modify props to the crud options here for attribute 'Comment'] end

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
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Form Page'] off begin
		name: "Form Pages",
		displayType: 'hidden',
		order: 90,
		referenceTypeFunc: () => Models.AttendanceEntityFormTileEntity,
		disableDefaultOptionRemoval: true,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'formPages',
			oppositeEntity: () => Models.AttendanceEntityFormTileEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'Form Page'] end
	})
	public formPages: Models.AttendanceEntityFormTileEntity[] = [];

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	// eslint-disable-next-line @typescript-eslint/no-useless-constructor
	constructor(attributes?: Partial<IAttendanceEntityAttributes>) {
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
	public assignAttributes(attributes?: Partial<IAttendanceEntityAttributes>) {
		// % protected region % [Override assign attributes here] off begin
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.dateOfService !== undefined) {
				if (attributes.dateOfService === null) {
					this.dateOfService = attributes.dateOfService;
				} else {
					this.dateOfService = moment(attributes.dateOfService).toDate();
				}
			}
			if (attributes.serviceID !== undefined) {
				this.serviceID = attributes.serviceID;
			}
			if (attributes.seatNoID !== undefined) {
				this.seatNoID = attributes.seatNoID;
			}
			if (attributes.temperature !== undefined) {
				this.temperature = attributes.temperature;
			}
			if (attributes.attendedService !== undefined) {
				this.attendedService = attributes.attendedService;
			}
			if (attributes.reasonForNotAttending !== undefined) {
				this.reasonForNotAttending = attributes.reasonForNotAttending;
			}
			if (attributes.comment !== undefined) {
				this.comment = attributes.comment;
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
			if (attributes.formPages !== undefined && Array.isArray(attributes.formPages)) {
				for (const model of attributes.formPages) {
					if (model instanceof Models.AttendanceEntityFormTileEntity) {
						this.formPages.push(model);
					} else {
						this.formPages.push(new Models.AttendanceEntityFormTileEntity(model));
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
		formPages {
			${Models.AttendanceEntityFormTileEntity.getAttributes().join('\n')}
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
		return this.name;
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
		return Models.AttendanceSubmissionEntity;
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
CRUD(createdAttr)(AttendanceEntity.prototype, 'created');
CRUD(modifiedAttr)(AttendanceEntity.prototype, 'modified');
// % protected region % [Modify the create and modified CRUD attributes here] end
