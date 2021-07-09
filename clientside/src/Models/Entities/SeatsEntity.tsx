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
	makeEnumFetchFunction,
	getCreatedModifiedCrudOptions,
} from 'Util/EntityUtils';
import { VisitorsSeatsEntity } from 'Models/Security/Acl/VisitorsSeatsEntity';
import { AdminSeatsEntity } from 'Models/Security/Acl/AdminSeatsEntity';
import { MemberSeatsEntity } from 'Models/Security/Acl/MemberSeatsEntity';
import { CategoryGroupLeaderSeatsEntity } from 'Models/Security/Acl/CategoryGroupLeaderSeatsEntity';
import { UsherSeatsEntity } from 'Models/Security/Acl/UsherSeatsEntity';
import { ProtocolSeatsEntity } from 'Models/Security/Acl/ProtocolSeatsEntity';
import { GroupCategorySeatsEntity } from 'Models/Security/Acl/GroupCategorySeatsEntity';
import * as Enums from '../Enums';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import {SuperAdministratorScheme} from '../Security/Acl/SuperAdministratorScheme';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface ISeatsEntityAttributes extends IModelAttributes {
	seatNumber: number;
	reservation: Enums.reservation;

	workflowStatess: Array<Models.SeatsWorkflowStates | Models.ISeatsWorkflowStatesAttributes>;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('SeatsEntity', 'Seats')
// % protected region % [Customise your entity metadata here] end
export default class SeatsEntity extends Model implements ISeatsEntityAttributes {
	public static acls: IAcl[] = [
		new SuperAdministratorScheme(),
		new VisitorsSeatsEntity(),
		new AdminSeatsEntity(),
		new MemberSeatsEntity(),
		new CategoryGroupLeaderSeatsEntity(),
		new UsherSeatsEntity(),
		new ProtocolSeatsEntity(),
		new GroupCategorySeatsEntity(),
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

	// % protected region % [Modify props to the crud options here for attribute 'Seat Number'] off begin
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'Seat Number',
		displayType: 'textfield',
		order: 10,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public seatNumber: number;
	// % protected region % [Modify props to the crud options here for attribute 'Seat Number'] end

	// % protected region % [Modify props to the crud options here for attribute 'Reservation'] off begin
	/**
	 * Seat status Open or Reserved
	 */
	@observable
	@attribute()
	@CRUD({
		name: 'Reservation',
		displayType: 'enum-combobox',
		order: 20,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: (attr: string) => {
			return AttrUtils.standardiseEnum(attr, Enums.reservationOptions);
		},
		enumResolveFunction: makeEnumFetchFunction(Enums.reservationOptions),
		displayFunction: (attribute: Enums.reservation) => Enums.reservationOptions[attribute],
	})
	public reservation: Enums.reservation;
	// % protected region % [Modify props to the crud options here for attribute 'Reservation'] end

	@observable
	@CRUD({
		// % protected region % [Modify props to the crud options here for attribute 'Worflow-States'] off begin
		name: 'Workflow States',
		displayType: 'workflow-data',
		order: 30,
		// % protected region % [Modify props to the crud options here for attribute 'Worflow-States'] end
	})
	public workflowStatess: Models.SeatsWorkflowStates[] = [];
	@observable
	public workflowBehaviourStateIds: Array<string>;
	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	// eslint-disable-next-line @typescript-eslint/no-useless-constructor
	constructor(attributes?: Partial<ISeatsEntityAttributes>) {
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
	public assignAttributes(attributes?: Partial<ISeatsEntityAttributes>) {
		// % protected region % [Override assign attributes here] off begin
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.seatNumber !== undefined) {
				this.seatNumber = attributes.seatNumber;
			}
			if (attributes.reservation !== undefined) {
				this.reservation = attributes.reservation;
			}
			if (attributes.workflowStatess !== undefined && Array.isArray(attributes.workflowStatess)) {
				for (const model of attributes.workflowStatess) {
					if (model instanceof Models.SeatsWorkflowStates) {
						this.workflowStatess.push(model);
					} else {
						this.workflowStatess.push(new Models.SeatsWorkflowStates(model));
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
		workflowStatess {
			${Models.SeatsWorkflowStates.getAttributes().join('\n')}
			workflowStates {
				${Models.WorkflowStateEntity.getAttributes().join('\n')}
			}
		}
	`;
	// % protected region % [Customize Default Expands here] end

	/**
	 * The save method that is called from the admin CRUD components.
	 */
	// % protected region % [Customize Save From Crud here] off begin
	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
			workflowStatess: {},
		};

		relationPath['workflowBehaviourStateIds'] = {};

		return this.save(
			relationPath,
			{
				options: [
					{
						key: 'mergeReferences',
						graphQlType: '[String]',
						value: [
							'workflowStatess',
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
CRUD(createdAttr)(SeatsEntity.prototype, 'created');
CRUD(modifiedAttr)(SeatsEntity.prototype, 'modified');
// % protected region % [Modify the create and modified CRUD attributes here] end
