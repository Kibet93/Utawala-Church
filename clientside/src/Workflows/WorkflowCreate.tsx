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
import Spinner from 'Views/Components/Spinner/Spinner';
import SecuredPage from 'Views/Components/Security/SecuredPage';
import WorkflowDesigner from 'Workflows/Designer/WorkflowDesigner';
import { gql } from 'apollo-boost';
import { action, observable, runInAction } from 'mobx';
import { observer } from 'mobx-react';
import { WorkflowVersionEntity } from 'Models/Entities';
import { IWorkflowVersionEntityAttributes } from 'Models/Entities/WorkflowVersionEntity';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import { store } from 'Models/Store';

const workflowVersionQuery = gql`
	query fetch($id: ID) {
		workflowVersionEntity(id: $id)
		{
			id
			modified
			workflowId
			workflowName
			workflowDescription
			seatsAssociation
			statess {
				id
				workflowVersionId
				isStartState
				stepName
				displayIndex
				outgoingTransitionss {
					id
					transitionName
					targetStateId
					sourceStateId
				}
			}
		}
	}`;

interface IWorkflowBuilderProps {
	workflowVersionId?: string
	mode: EntityFormMode
}

@observer
export default class WorkflowCreate extends React.Component<IWorkflowBuilderProps>{

	@observable
	private workflowVersion: WorkflowVersionEntity;

	@observable
	private loadingState: 'loading' | 'error' | 'done' = 'loading';

	@observable
	private errors: object;

	@action
	public componentDidMount(): void {
		if (this.props.mode === EntityFormMode.CREATE || !this.props.workflowVersionId) {
			this.workflowVersion = new WorkflowVersionEntity({id: uuid.v4(), workflowId: uuid.v4()});
			this.loadingState = "done";
		} else{
			store.apolloClient.query<{workflowVersionEntity: IWorkflowVersionEntityAttributes}>({
					query: workflowVersionQuery,
					fetchPolicy: "network-only",
					variables: {id: this.props.workflowVersionId}
				})
				.then(({data}) => {
					this.workflowVersion = new WorkflowVersionEntity(data.workflowVersionEntity);
					this.workflowVersion.statess = _.sortBy(this.workflowVersion.statess, ['displayIndex']);
					const transitions = _.flatMap(this.workflowVersion.statess, s => s.outgoingTransitionss);
					const stateMap = this.workflowVersion.statess.reduce((a, s) => ({...a, [s.id as string]: s}), {});
					for (const transition of transitions) {
						transition.targetState = stateMap[transition.targetStateId];
						transition.sourceState = stateMap[transition.sourceStateId];
					}
					runInAction(() => this.loadingState = "done");
				})
				.catch((e: any) => {
					runInAction(() =>this.errors = e);
					runInAction(() => this.loadingState = "error");
				});
		}
	};

	private renderContents = () => {
		switch (this.loadingState) {
			case 'loading': return <Spinner/>;
			case 'error': return 'Something went wrong with loading the data. ' + JSON.stringify(this.errors);
			case 'done': return <WorkflowDesigner workflowVersion={this.workflowVersion} mode={this.props.mode}/>;
		}
	};

	public render(){
		return (
			<SecuredPage>
				<div className="body-content">
					{this.renderContents()}
				</div>
			</SecuredPage>
		)
	}
}