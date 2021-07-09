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
import * as uuid from 'uuid';
import WorkflowStepConfiguration from './WorkflowStepConfiguration';
import WorkflowDesignerStatesSidebar from './WorkflowDesignerStatesSidebar';
import { action, computed, observable, runInAction } from 'mobx';
import { Button, Colors, Display } from 'Views/Components/Button/Button';
import { observer } from 'mobx-react';
import { WorkflowStateEntity } from 'Models/Entities';
import { IWorkflowStateEntityAttributes } from 'Models/Entities/WorkflowStateEntity';
import { IWorkflowTabProps } from 'Workflows/Designer/WorkflowDesigner';
import { IWorkflowVersionEntityAttributes } from 'Models/Entities/WorkflowVersionEntity';
import { IEntityValidationErrors } from 'Validators/Util';

export interface IWorkflowDesignerStatesProps extends IWorkflowTabProps {
	errors? : Array<IEntityValidationErrors>;
}

@observer
export default class WorkflowDesignerStates extends React.Component<IWorkflowDesignerStatesProps>{
	@observable
	private versionClone : IWorkflowVersionEntityAttributes;

	@observable
	private sidebarStepClone : IWorkflowStateEntityAttributes | undefined;

	@action
	private onAddNewStep = () => {
		this.props.workflowVersion.statess.push(new WorkflowStateEntity({
			id: uuid.v4(),
			isStartState: this.props.workflowVersion.statess.length === 0,
			workflowVersionId: this.props.workflowVersion.id,
			displayIndex: this.props.workflowVersion.statess.length + 1
		}));
	};

	@action
	private onStepRemove = (index: number) => {
		if (this.props.workflowVersion.statess[index].isStartState) {
			const newStartState = this.props.workflowVersion.statess[index === 0 ? 1 : 0];
			if (newStartState) {
				newStartState.isStartState = true;
			}
		}
		this.props.workflowVersion.statess.forEach(s =>
		s.outgoingTransitionss = s.outgoingTransitionss.filter(t => t.targetState.id !== this.props.workflowVersion.statess[index].id));
		this.props.workflowVersion.statess.splice(index, 1);
		this.props.workflowVersion.statess.forEach((s,loopIndex) => s.displayIndex = loopIndex + 1);
		this.sidebarStepClone = undefined;
	};

	private onEditStep = async (index: number) => {
		await this.props.validateModel();
		if (!this.props.errors) {
			const sidebarStepId = this.props.workflowVersion.statess[index].id;
			runInAction(() => this.versionClone = _.cloneDeep(this.props.workflowVersion));
			runInAction(() => this.sidebarStepClone = this.versionClone.statess.find(s => s.id === sidebarStepId))

			this.updateVersionClone();
		}
	};

	@action
	private onSaveSidebar = (version: IWorkflowVersionEntityAttributes) => {
		for (const state of version.statess) {
			const existingState = this.props.workflowVersion.statess.find(s => s.id === state.id);
			if (existingState) {
				existingState.outgoingTransitionss = state.outgoingTransitionss;
			}
		}
		this.sidebarStepClone = undefined
	};

	@action
	private onCloseSidebar = () => {
		this.sidebarStepClone = undefined
	};

	@computed
	private get workflowDesignerStateSidebar() {
		if (this.sidebarStepClone){
			return <WorkflowDesignerStatesSidebar
				step={this.sidebarStepClone}
				version={this.versionClone}
				onSave={this.onSaveSidebar}
				onClose={this.onCloseSidebar}/>
		}
		return null;
	}

	@action
	private updateVersionClone = () => {
		if (this.versionClone) {
			for (const clonedState of this.versionClone.statess) {
				const originalStep = this.props.workflowVersion.statess.find(state => state.id === clonedState.id);
				if (originalStep) {
					clonedState.stepName = originalStep.stepName
				}
			}
		}
	};

	public render(){
		return (
			<>
				<section className="workflow__states workflow__states__create">
					{this.workflowDesignerStateSidebar}
					<div className="workflow-states__wrapper">
						<h3>States</h3>
						{this.props.workflowVersion.statess.map((state, index) =>
							<WorkflowStepConfiguration
								key={index}
								step={state}
								index={index}
								version={this.props.workflowVersion}
								onDelete={this.onStepRemove}
								onEdit={this.onEditStep}
								stateErrors={this.props.errors}
								validateStates={this.props.validateModel}/>
						)}

						<div className="workflow__new-state">
							<Button
								onClick={this.onAddNewStep}
								className="btn--icon icon-top icon-plus"
								aria-label="Add new state"
								colors={Colors.Primary}
								display={Display.Text}>
								Add a New State
							</Button>
						</div>
					</div>
				</section>
			</>
		)};
}