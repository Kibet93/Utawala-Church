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
import * as uuid from 'uuid';
import WorkflowTransitionEntity from 'Models/Entities/WorkflowTransitionEntity';
import WorkflowTransitionConfiguration from 'Workflows/Designer/States/WorkflowTransitionConfiguration';
import { Button, Display, Colors } from 'Views/Components/Button/Button';
import { observer } from 'mobx-react';
import { action, observable, runInAction } from 'mobx';
import { IWorkflowStateEntityAttributes } from 'Models/Entities/WorkflowStateEntity';
import { IWorkflowVersionEntityAttributes } from 'Models/Entities/WorkflowVersionEntity';
import { IEntityValidationErrors } from 'Validators/Util';

export interface IWorkflowDesignerStatesSidebarProps {
	step: IWorkflowStateEntityAttributes
	version: IWorkflowVersionEntityAttributes
	onSave: (version: IWorkflowVersionEntityAttributes) => void;
	onClose: () => void;
}

@observer
export default class WorkflowDesignerStatesSidebar extends React.Component<IWorkflowDesignerStatesSidebarProps>{

	@action
	private onAddTransition = () => {
		this.props.step.outgoingTransitionss.push(new WorkflowTransitionEntity({id: uuid.v4(), sourceState: this.props.step}));
	};

	@action
	private onUpdateReturnFlow = (returnFlow: boolean, index: number) => {
		const destination = this.props.step.outgoingTransitionss[index].targetState;
		if (destination){
			if (returnFlow){
				destination.outgoingTransitionss.push(
					new WorkflowTransitionEntity({
						sourceState: destination,
						targetState: this.props.step,
						transitionName: this.props.step.outgoingTransitionss[index].transitionName
					}))
			} else {
				destination.outgoingTransitionss = destination.outgoingTransitionss
					.filter(t => t.targetState.id !== this.props.step.id)
			}
		}
	};

	@observable
	private validationErrors: Array<IEntityValidationErrors>;

	@action
	private validateTransitions = () => {
		this.validationErrors = [];

		const promises = this.props.step.outgoingTransitionss.map(transition => {
			return (transition as WorkflowTransitionEntity).validate().then(errors => {
				if (transition.id) {
					runInAction(() => this.validationErrors.push(errors));
				}
			});
		});

		return Promise.all(promises);
	};


	@action
	private validateAndSave = async () => {
		await this.validateTransitions();
		if (this.validationErrors.every(e => Object.keys(e).length === 0)) {
			this.props.onSave(this.props.version);
		}
	};

	public render(){
		return (
			<section className="workflow-properties">

				<div className="workflow-properties__header">
					<h3>Outgoing Transitions</h3>
					<Button
						onClick={() => this.props.onClose()}
						icon={{icon:"x-circle", iconPos:"icon-left"}}
						label-visible="false"/>
				</div>

				<div className="workflow-properties__commands">
					{this.props.step.outgoingTransitionss.map((transition, index) =>
						<WorkflowTransitionConfiguration
							key={index}
							index={index}
							version={this.props.version}
							state={this.props.step}
							transition={transition}
							sidebarErrors={this.validationErrors}
							onUpdateReturnFlow={this.onUpdateReturnFlow}
							validateTransitions={this.validateTransitions}/>
					)}
				</div>

				<div className="workflow-properties__add">
					<Button
						onClick={this.onAddTransition}
						className="btn--icon icon-left icon-plus" colors={Colors.Primary} display={Display.Outline}>
						Add transition
					</Button>
				</div>

				<div className="workflow-properties__save">
					<Button
						onClick={() => this.validateAndSave()}
						className="btn--icon icon-left icon-save" colors={Colors.Primary} display={Display.Solid}>
						Save
					</Button>
				</div>
			</section>
		)
	}
}