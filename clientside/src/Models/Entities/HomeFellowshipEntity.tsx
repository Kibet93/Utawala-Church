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
import { VisitorsHomeFellowshipEntity } from 'Models/Security/Acl/VisitorsHomeFellowshipEntity';
import { AdminHomeFellowshipEntity } from 'Models/Security/Acl/AdminHomeFellowshipEntity';
import { MemberHomeFellowshipEntity } from 'Models/Security/Acl/MemberHomeFellowshipEntity';
import { CategoryGroupLeaderHomeFellowshipEntity } from 'Models/Security/Acl/CategoryGroupLeaderHomeFellowshipEntity';
import { UsherHomeFellowshipEntity } from 'Models/Security/Acl/UsherHomeFellowshipEntity';
import { ProtocolHomeFellowshipEntity } from 'Models/Security/Acl/ProtocolHomeFellowshipEntity';
import { GroupCategoryHomeFellowshipEntity } from 'Models/Security/Acl/GroupCategoryHomeFellowshipEntity';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import {SuperAdministratorScheme} from '../Security/Acl/SuperAdministratorScheme';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface IHomeFellowshipEntityAttributes extends IModelAttributes {
	fellowshipID: number;
	fellowshipName: string;
	fellowshipPastor: string;

	membersfellowships: Array<Models.MemberEntity | Models.IMemberEntityAttributes>;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('HomeFellowshipEntity', 'Home Fellowship')
// % protected region % [Customise your entity metadata here] end
export default class HomeFellowshipEntity extends Model implements IHomeFellowshipEntityAttributes {
	public static acls: IAcl[] = [
		new SuperAdministratorScheme(),
		new VisitorsHomeFellowshipEntity(),
		new AdminHomeFellowshipEntity(),
		new MemberHomeFellowshipEntity(),
		new CategoryGroupLeaderHomeFellowshipEntity(),
		new UsherHomeFellowshipEntity(),
		new ProtocolHomeFellowshipEntity(),
		new GroupCategoryHomeFellowshipEntity(),
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

	// % protected region % [Modify props to the crud options here for attribute 'Fellowship ID'] off begin
	@Validators.Integer()
	@Validators.Unique()
	@observable
	@attribute()
	@CRUD({
		name: 'Fellowship ID',
		displayType: 'textfield',
		order: 10,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public fellowshipID: number;
	// % protected region % [Modify props to the crud options here for attribute 'Fellowship ID'] end

	// % protected region % [Modify props to the crud options here for attribute 'Fellowship Name'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'Fellowship Name',
		displayType: 'textfield',
		order: 20,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public fellowshipName: string;
	// % protected region % [Modify props to the crud options here for attribute 'Fellowship Name'] end

	// % protected region % [Modify props to the crud options here for attribute 'Fellowship Pastor'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'Fellowship Pastor',
		displayType: 'textfield',
		order: 30,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public fellowshipPastor: string;
	// % protected region % [Modify props to the crud options here for attribute 'Fellowship Pastor'] end

	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'MEMBERsFellowship'] off begin
		name: "MEMBERsFellowships",
		displayType: 'reference-multicombobox',
		order: 40,
		referenceTypeFunc: () => Models.MemberEntity,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'membersfellowships',
			oppositeEntity: () => Models.MemberEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'MEMBERsFellowship'] end
	})
	public membersfellowships: Models.MemberEntity[] = [];

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	// eslint-disable-next-line @typescript-eslint/no-useless-constructor
	constructor(attributes?: Partial<IHomeFellowshipEntityAttributes>) {
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
	public assignAttributes(attributes?: Partial<IHomeFellowshipEntityAttributes>) {
		// % protected region % [Override assign attributes here] off begin
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.fellowshipID !== undefined) {
				this.fellowshipID = attributes.fellowshipID;
			}
			if (attributes.fellowshipName !== undefined) {
				this.fellowshipName = attributes.fellowshipName;
			}
			if (attributes.fellowshipPastor !== undefined) {
				this.fellowshipPastor = attributes.fellowshipPastor;
			}
			if (attributes.membersfellowships !== undefined && Array.isArray(attributes.membersfellowships)) {
				for (const model of attributes.membersfellowships) {
					if (model instanceof Models.MemberEntity) {
						this.membersfellowships.push(model);
					} else {
						this.membersfellowships.push(new Models.MemberEntity(model));
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
		membersfellowships {
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
			membersfellowships: {},
		};
		return this.save(
			relationPath,
			{
				options: [
					{
						key: 'mergeReferences',
						graphQlType: '[String]',
						value: [
							'membersfellowships',
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
CRUD(createdAttr)(HomeFellowshipEntity.prototype, 'created');
CRUD(modifiedAttr)(HomeFellowshipEntity.prototype, 'modified');
// % protected region % [Modify the create and modified CRUD attributes here] end
