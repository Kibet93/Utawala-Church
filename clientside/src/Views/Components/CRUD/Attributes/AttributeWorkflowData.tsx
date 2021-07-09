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
import axios, { AxiosResponse } from 'axios';
import Spinner from 'Views/Components/Spinner/Spinner';
import { Model } from 'Models/Model';
import { IAttributeProps } from 'Views/Components/CRUD/Attributes/IAttributeProps';
import { IWorkflowStateEntityAttributes } from 'Models/Entities/WorkflowStateEntity';
import { observer } from 'mobx-react';
import { action, observable } from 'mobx';
import { Combobox } from 'Views/Components/Combobox/Combobox';
import { SERVER_URL } from 'Constants';

export interface IWorkflowCRUDDisplayProps {
	workflowState: IWorkflowStateEntityAttributes;
	index: number;
	onUpdateNextState: (index: number, nextStateId: string) => void;
	isReadOnly: boolean | undefined;
}

@observer
export default class AttributeWorkflowData<T extends Model> extends React.Component<IAttributeProps<T>> {

	@observable
	private requestState: 'loading' | 'error' | 'done' = 'loading';

	private entityStates : Array<IWorkflowStateEntityAttributes>;

	private requestError?: string;

	async componentDidMount() {
		await this.getWorkflowStates();
	}

	public getWorkflowStates = () => {
		axios.get(`${SERVER_URL}/api/behaviours/workflow/${this.props.model.getModelName()}${this.props.model.id ? `/${this.props.model.id}` : '' }`)
			.then(this.onSuccess)
			.catch(this.onError);
	};

	@action
	private onSuccess = (data: AxiosResponse) => {
		this.entityStates = data.data;
		this.requestState = 'done';
		this.props.model['workflowBehaviourStateIds'] = this.entityStates.map(s => {
			if (s.id) {
				return s.id
			}
			return ''
		});
	};

	@action
	private onError = (data: AxiosResponse) => {
		this.requestState = 'error';
		this.requestError = data.statusText;
	};

	private onUpdateNextState = (index: number, nextStateId: string) => {
		this.props.model['workflowBehaviourStateIds'][index] = nextStateId;
	};

	public renderStates = () => {
		if (this.requestState === 'loading') {
			return <Spinner/>;
		}
		if (this.requestState === 'error') {
			return(
				<>
					<h2>An unexpected error occurred: </h2>
					<div>{this.requestError}</div>
				</>
			)
		}
		return (
			<section className={this.props.className ? `crud__workflow ${this.props.className}` : 'crud__workflow'}>
					{this.entityStates.map((state, index) => {
						return <WorkflowCRUDDisplay
							key={index}
							index={index}
							isReadOnly={this.props.isReadonly}
							onUpdateNextState={this.onUpdateNextState}
							workflowState={state}/>
					})
				}
			</section>
		)
	};

	public render() {
			return this.renderStates();
	}
}

export class WorkflowCRUDDisplay extends React.Component<IWorkflowCRUDDisplayProps>{

	@observable
	public nextWorkflowState = {
		stateId: `Current State: ${this.props.workflowState.stepName}`
	};

	public getTargetStateOptions = () => {
		if (this.props.workflowState.outgoingTransitionss) {
			let options: {display: string, value: string | undefined}[] = this.props.workflowState.outgoingTransitionss.map(transition => {
				return {display: transition.transitionName, value: transition.targetStateId}
			});
			options.push({display: this.props.workflowState.stepName, value: this.props.workflowState.id})
			return options;
		}
		return [];
	};

	private onUpdateNextState = () => {
		if (this.nextWorkflowState.stateId) {
			this.props.onUpdateNextState(this.props.index, this.nextWorkflowState.stateId);
		}
	};

	public render() {
		return (
			<div className="workflow__display">
				<h4> {this.props.workflowState.workflowVersion.workflowName} Workflow </h4>
				<Combobox
					onAfterChange={() => this.onUpdateNextState()}
					isDisabled={this.props.isReadOnly}
					placeholder={`Current State: ${this.props.workflowState.stepName}`}
					model={this.nextWorkflowState}
					modelProperty='stateId'
					label={`Next State`}
					options={this.getTargetStateOptions()}/>
			</div>
		)
	}
}