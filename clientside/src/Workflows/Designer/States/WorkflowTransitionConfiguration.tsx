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
import { action, computed, observable } from 'mobx';
import { observer } from 'mobx-react';
import { TextField } from 'Views/Components/TextBox/TextBox';
import { DisplayType } from 'Views/Components/Models/Enums';
import { Checkbox } from 'Views/Components/Checkbox/Checkbox';
import { IWorkflowTransitionEntityAttributes } from 'Models/Entities/WorkflowTransitionEntity';
import { Combobox } from 'Views/Components/Combobox/Combobox';
import { IWorkflowStateEntityAttributes } from 'Models/Entities/WorkflowStateEntity';
import { IWorkflowVersionEntityAttributes } from 'Models/Entities/WorkflowVersionEntity';
import { Button, Colors, Display } from 'Views/Components/Button/Button';
import { IEntityValidationErrors } from 'Validators/Util';

export interface IWorkflowTransitionProps {
	index: number
	state: IWorkflowStateEntityAttributes
	version: IWorkflowVersionEntityAttributes
	transition: IWorkflowTransitionEntityAttributes
	sidebarErrors: Array<IEntityValidationErrors>
	onUpdateReturnFlow: (returnFlow: boolean, index: number) => void
	validateTransitions: () => void;
}

@observer
export default class WorkflowTransitionConfiguration extends React.Component<IWorkflowTransitionProps>{
	@computed
	private get destination() {
		return {
			state: this.props.transition.targetState
		}
	};

	@observable
	private returnFlow = {
		returnFlow: _.flatMap(this.props.version.statess, state => state.outgoingTransitionss)
			.filter(t => t.targetState === this.props.state && t.sourceState === this.destination.state).length > 0
	};

	@computed
	private get targetStateOptions() : { display: string; value: IWorkflowStateEntityAttributes }[] {
		return this.props.version.statess
			.filter(state => state !== this.props.state && !this.props.state.outgoingTransitionss.some(t => t !== this.props.transition && t.targetState === state))
			.map(state => {return {display: state.stepName, value: state}});
	}

	@action
	private updateTargetState = () => {
		if (this.destination.state && this.destination.state.id){
			if (this.returnFlow.returnFlow) {
				this.props.onUpdateReturnFlow(false, this.props.index);
				this.props.transition.targetState = this.destination.state;
				this.props.transition.targetStateId = this.destination.state.id;
				this.props.onUpdateReturnFlow(true, this.props.index);
			} else {
				this.props.transition.targetState = this.destination.state;
				this.props.transition.targetStateId = this.destination.state.id;
			}
		}
	};

	@action
	private removeTransition = () => {
		this.props.state.outgoingTransitionss.splice(this.props.index, 1)
	};

	private getAttributeErrors = (attributeName: string) => {
		const attributeErrors = [];
		if (this.props.sidebarErrors
			&& this.props.sidebarErrors.length > this.props.index
			&& this.props.sidebarErrors[this.props.index]
			&& Object.keys(this.props.sidebarErrors[this.props.index]).some(k => k === attributeName)){
				const errors = this.props.sidebarErrors[this.props.index][attributeName].errors;
				for(const key of Object.keys(errors)) {
					attributeErrors.push(errors[key])
				}
		}
		return _.flatMap(attributeErrors, e => e)
	};

	private validateInput = () => {
		if (this.props.sidebarErrors && this.props.sidebarErrors.length > 0) {
			this.props.validateTransitions();
		}
	};

	public render(){
		return (
			<div className="workflow-properties__option">
				<TextField
					onAfterChange={() => this.validateInput()}
					model={this.props.transition}
					modelProperty='transitionName'
					displayType={DisplayType.BLOCK}
					label="Transition name"
					errors={this.getAttributeErrors('transitionName')}/>

				<Combobox
					onAfterChange={() => {this.updateTargetState(); this.validateInput()}}
					model={this.destination}
					modelProperty='state'
					label='Destination'
					options={this.targetStateOptions}
					getOptionValue={e => e ? e.id : undefined}
					errors={this.getAttributeErrors('targetStateId')}/>

				<Checkbox
					onAfterChecked={(event, checked) => this.props.onUpdateReturnFlow(checked, this.props.index)}
					model={this.returnFlow}
					modelProperty={'returnFlow'}
					label={"Return flow"}/>

				<Button
					onClick={this.removeTransition}
					className="btn--icon icon-left icon-minus"
					colors={Colors.Primary}
					display={Display.Text}>
					Remove transition
				</Button>
			</div>
		);
	}
}