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
import axios from 'axios';
import alert from 'Util/ToastifyUtils';
import WorkflowDesignerDetails from './Details/WorkflowDesignerDetails';
import WorkflowDesignerHeader from './ActionBars/WorkflowDesignerHeader';
import WorkflowDesignerFooter from './ActionBars/WorkflowDesignerFooter';
import WorkflowDesignerStates from './States/WorkflowDesignerStates';
import WorkflowVersion, { IWorkflowVersionEntityAttributes } from 'Models/Entities/WorkflowVersionEntity';
import WorkflowState from 'Models/Entities/WorkflowStateEntity';
import { action, computed, observable, runInAction } from 'mobx';
import { observer } from 'mobx-react';
import { store } from 'Models/Store';
import { gql } from 'apollo-boost';
import { IWorkflowEntityAttributes } from 'Models/Entities/WorkflowEntity';
import { IEntityValidationErrors } from 'Validators/Util';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import { SERVER_URL } from 'Constants';

export enum WorkflowDesignerTab {
	Details,
	States
}

export interface IWorkflowDesignerProps {
	workflowVersion: IWorkflowVersionEntityAttributes;
	mode?: EntityFormMode;
}

export interface IWorkflowTabProps extends IWorkflowDesignerProps {
	validateModel : () => void;
}

@observer
export default class WorkflowDesigner extends React.Component<IWorkflowDesignerProps>{
	@observable
	private currentTab: WorkflowDesignerTab = WorkflowDesignerTab.Details;

	@observable
	workflowVersionErrors: IEntityValidationErrors | undefined = undefined;

	@observable
	workflowStatesErrors: Array<IEntityValidationErrors> | undefined = undefined;

	private validateWorkflowVersion = () => {
		return (this.props.workflowVersion as WorkflowVersion).validate()
			.then(e => {
				if ( Object.keys(e).length){
					runInAction(() => this.workflowVersionErrors = e);
				} else {
					runInAction(() => this.workflowVersionErrors = undefined);
				}
			});
	};

	private validateWorkflowStates = async () => {
		const states = this.props.workflowVersion.statess;
		const errors: Array<IEntityValidationErrors> = [];
		const promises = states.map(state => {
			return (state as WorkflowState).validate()
				.then(e => {
					errors.push(e);
				})
		});
		await Promise.all(promises);
		if (errors.some(e => Object.keys(e).length)){
			runInAction(() => this.workflowStatesErrors = errors);
		} else {
			runInAction(() => this.workflowStatesErrors = undefined);
		}
	};

	private validateModel = () => {
		if (this.currentTab === WorkflowDesignerTab.Details) {
			return this.validateWorkflowVersion();
		} else if (this.currentTab === WorkflowDesignerTab.States) {
			if (this.props.workflowVersion.statess.length){
				return this.validateWorkflowStates();
			}
		}
		return;
	};

	@action
	private onTabChange = async (tab: WorkflowDesignerTab) => {
		await this.validateModel();
		if (!this.workflowVersionErrors && !this.workflowStatesErrors) {
			runInAction(() =>this.currentTab = tab);
		}
	};

	@action
	private onSaveWorkflowVersion = async () => {
		await this.validateModel();
		if (!this.workflowVersionErrors && !this.workflowStatesErrors) {
			if (this.props.mode === EntityFormMode.CREATE) {
				await this.createNewWorkflow();
				await this.sendVersionToServer();
				alert(`Successfully created new workflow: ${this.props.workflowVersion.workflowName}`, "success", {});
			} else if (this.props.mode === EntityFormMode.EDIT) {
				await this.sendUpdatedVersionToServer();
				alert(`Successfully updated workflow: ${this.props.workflowVersion.workflowName}`, "success", {});
			}
		} else {
			alert(`Workflow contains errors`, "error", {});
		}
	};

	private createNewWorkflow = () => {
		return store.apolloClient
			.mutate<{createWorkflowEntity: IWorkflowEntityAttributes[]}>({
				mutation: gql`
					mutation createWorkflow($workflow: [WorkflowEntityInput], $mergeReferences: [String]) {
						createWorkflowEntity(workflowEntitys: $workflow, mergeReferences: $mergeReferences) {
							id
							created
							modified
							name
							__typename
						}
					}
				`,
				variables: {"workflow":[{"id":uuid.v4(), "name":this.props.workflowVersion.workflowName}], "mergeReferences":[]}
			})
			.then((result) => {
				if (result.data && result.data.createWorkflowEntity[0].id){
					this.props.workflowVersion.workflowId = result.data.createWorkflowEntity[0].id;
				}
			});
	};

	private sendVersionToServer = () => {
		const states = _.flatMap(this.props.workflowVersion.statess, state => state);
		const transitions = _.flatMap(this.props.workflowVersion.statess, state => state.outgoingTransitionss);

		return axios.post(`${SERVER_URL}/api/behaviours/workflow`, {
			'version': this.props.workflowVersion,
			'states': states,
			'transitions': transitions
		}).then(() => store.routerHistory.push('/admin/workflows'))

	};

	private sendUpdatedVersionToServer = () => {
		const states = _.flatMap(this.props.workflowVersion.statess, state => state);
		const transitions = _.flatMap(this.props.workflowVersion.statess, state => state.outgoingTransitionss);

		return axios.put(`${SERVER_URL}/api/behaviours/workflow`, {
			'version': this.props.workflowVersion,
			'states': states,
			'transitions': transitions
		}).then(() => store.routerHistory.push('/admin/workflows'))

	};

	@computed
	private get workflowDesignerContents() {
		switch(this.currentTab) {
			case WorkflowDesignerTab.Details:
				return <WorkflowDesignerDetails
					{...this.props}
					validateModel={this.validateModel}
					errors={this.workflowVersionErrors}/>;
			case WorkflowDesignerTab.States:
				return(
					<WorkflowDesignerStates
						{...this.props}
						validateModel={this.validateModel}
						errors={this.workflowStatesErrors}/>
				);

		}
		return <h2>No Tab Selected</h2>;
	}

	public render(){
		return (
			<section className = 'workflow-behaviour'>
				<WorkflowDesignerHeader changeTab={this.onTabChange} selectedTab={this.currentTab}/>
				{this.workflowDesignerContents}
				<WorkflowDesignerFooter onSave={this.onSaveWorkflowVersion}/>
			</section>
		)
	}
}