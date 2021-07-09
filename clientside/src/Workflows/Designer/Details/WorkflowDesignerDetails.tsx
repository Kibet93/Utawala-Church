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
import { TextField } from 'Views/Components/TextBox/TextBox';
import { DisplayType } from 'Views/Components/Models/Enums';
import { TextArea } from 'Views/Components/TextArea/TextArea';
import { MultiCombobox } from 'Views/Components/Combobox/MultiCombobox';
import { action, observable} from 'mobx';
import { observer } from 'mobx-react';
import { IWorkflowTabProps } from '../WorkflowDesigner';
import { IEntityValidationErrors } from 'Validators/Util';

export enum workflowEntityOption {
	'Seats',
}

export interface IWorkflowDesignerDetailsProps extends IWorkflowTabProps {
	errors? : IEntityValidationErrors;
}

@observer
export default class WorkflowDesignerDetails extends React.Component<IWorkflowDesignerDetailsProps>{
	@action
	private onSetEntityAssociations = (entities: workflowEntityOption[]) => {
		if (entities.includes(workflowEntityOption.Seats) && !this.props.workflowVersion.seatsAssociation) {
			this.props.workflowVersion.seatsAssociation = true;
		} else if (!entities.includes(workflowEntityOption.Seats) && this.props.workflowVersion.seatsAssociation) {
			this.props.workflowVersion.seatsAssociation = false;
		}
	};

	@action
	private getSelectedWorkflowEntites = () => {
		const entities: workflowEntityOption[] = [];
		if (this.props.workflowVersion.seatsAssociation) {
			entities.push(workflowEntityOption.Seats)
		}
		return entities;
	};

	@observable
	private selectedWorkflowEntities = {
		entities: this.getSelectedWorkflowEntites()
	};

	private workflowEntityOptions = [
		{display: "Seats", value: workflowEntityOption.Seats},
	];

	private getAttributeErrors = (attributeName: string) => {
		const attributeErrors = [];
		if (this.props.errors && this.props.errors[attributeName]) {
			const errors = this.props.errors[attributeName].errors;
			for (const key of Object.keys(errors)) {
				attributeErrors.push(errors[key])
			}
		}
		return attributeErrors;
	};

	public render(){
		return (
			<section className="workflow__create">
				<div className="workflow__details">
					<form>
						<TextField
							onAfterChange={this.props.validateModel}
							model={this.props.workflowVersion}
							modelProperty='workflowName'
							displayType={DisplayType.BLOCK}
							label="Workflow Name"
							errors={this.getAttributeErrors('workflowName')}
						/>
						<TextArea
							model={this.props.workflowVersion}
							modelProperty='workflowDescription'
							displayType={DisplayType.BLOCK}
							label="Workflow Description"
							labelVisible={true}
						/>
						<MultiCombobox
							onAfterChange={() => this.onSetEntityAssociations(this.selectedWorkflowEntities.entities)}
							model={this.selectedWorkflowEntities}
							modelProperty='entities'
							label="Entities"
							options={this.workflowEntityOptions}
						/>
					</form>
				</div>
			</section>
	)
	}
}