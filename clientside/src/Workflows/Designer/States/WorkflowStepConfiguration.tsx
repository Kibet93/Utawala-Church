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
import _ from 'lodash';
import { action, computed } from 'mobx';
import { observer } from 'mobx-react';
import { TextField } from 'Views/Components/TextBox/TextBox';
import { DisplayType } from 'Views/Components/Models/Enums';
import { Button, Display, Colors } from 'Views/Components/Button/Button';
import { WorkflowStateEntity } from 'Models/Entities';
import { IWorkflowStateEntityAttributes } from 'Models/Entities/WorkflowStateEntity';
import { IWorkflowVersionEntityAttributes } from 'Models/Entities/WorkflowVersionEntity';
import { IEntityValidationErrors } from 'Validators/Util';

export interface IWorkflowStepProps {
	step: WorkflowStateEntity | IWorkflowStateEntityAttributes;
	version: IWorkflowVersionEntityAttributes
	index: number;
	onDelete: (id: number) => void;
	onEdit: (id: number) => void;
	stateErrors?: Array<IEntityValidationErrors>;
	validateStates: () => void;
}

@observer
export default class WorkflowStepConfiguration extends React.Component<IWorkflowStepProps>{

	@computed
	private get incomingTransitionsCount () {
		return _.flatMap(this.props.version.statess, state => state.outgoingTransitionss)
			.filter(t => t.targetState.id === this.props.step.id).length;
	}

	@action
	private onClickStartButton = () => {
		this.props.version.statess.forEach(state => {
			state.isStartState = state === this.props.step;
		});
	};

	private getAttributeErrors = (attributeName: string) => {
		const attributeErrors = [];
		if (this.props.stateErrors
			&& this.props.stateErrors.length > 0
			&& this.props.stateErrors[this.props.index]
			&& Object.keys(this.props.stateErrors[this.props.index]).some(k => k === attributeName)){
			const errors = this.props.stateErrors[this.props.index][attributeName].errors;
			for(const key of Object.keys(errors)) {
				attributeErrors.push(errors[key])
			}
		}
		return _.flatMap(attributeErrors, e => e)
	};

	private validateInput = () => {
		if (this.props.stateErrors && this.props.stateErrors.length > 0) {
			this.props.validateStates();
		}
	};

	public render(){
		return (
			<>
				<div className="workflow__states-step">
					<Button
						onClick={this.onClickStartButton}
						className={`btn--icon icon-top icon-flag workflow__start-state`}
						display={this.props.step.isStartState ? Display.Solid : Display.Outline}
						colors={Colors.Primary}>
						Start
					</Button>

					<TextField
						onAfterChange={() => this.validateInput()}
						model={this.props.step}
						modelProperty='stepName'
						displayType={DisplayType.BLOCK}
						label="Step name"
						errors={this.getAttributeErrors('stepName')}
						inputProps={{
							tabIndex: this.props.index + 1
						}}/>

					<div className="workflow__incoming">
						<p>Incoming transitions</p>
						<p className="transition-number__box">{this.incomingTransitionsCount}</p>
					</div>

					<div className="workflow__outgoing">
						<p>Outgoing transitions</p>
						<div>
							<p className="transition-number__box">{this.props.step.outgoingTransitionss.filter(t => t.targetState).length}</p>
							<Button
								onClick={() => this.props.onEdit(this.props.index)}
								className="btn--icon icon-left icon-edit workflow__edit-state"
								display={Display.Solid}
								colors={Colors.Primary}>
								Edit
							</Button>
						</div>
					</div>

					<Button
						onClick={() => this.props.onDelete(this.props.index)}
						className="btn--icon icon-top icon-delete workflow__delete-state"
						display={Display.Text}
						colors={Colors.Primary}>
						Delete
					</Button>
				</div>
			</>
		)
	}
}