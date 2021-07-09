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
import * as React from "react";
import _ from 'lodash';
import alert from 'Util/ToastifyUtils';
import { observer } from 'mobx-react';
import { contextMenu } from 'react-contexify';
import { gql } from 'apollo-boost';
import { Link } from 'react-router-dom';
import { IWorkflowEntityAttributes } from 'Models/Entities/WorkflowEntity';
import { IModelAttributes } from 'Models/Model';
import { ContextMenu } from 'Views/Components/ContextMenu/ContextMenu';
import { Button } from 'Views/Components/Button/Button';
import { observable, runInAction } from 'mobx';
import { MenuItemEventHandler } from 'react-contexify/lib/types';
import { confirmModal } from 'Views/Components/Modal/ModalUtils';
import { WorkflowEntity } from 'Models/Entities';
import { store } from 'Models/Store';

export function getWorkflowListQuery () {
	return gql`
		query fetch {
			workflows: workflowEntitys {
				id
				name
				currentVersionId
				versionss {
					id
					modified
					workflowName
					seatsAssociation
				}
			}
		}`;
}

type workflowResponse = {
	workflows: Array<IWorkflowEntityAttributes & IModelAttributes>
};

@observer
export default class WorkflowCollection extends React.Component<{showCreateTile: Boolean}>{
	@observable
	private requestState: 'loading' | 'error' | 'done' = 'loading';

	@observable
	private data: workflowResponse;

	@observable
	private error: string;

	private getWorkflowVersion = (workflow: IWorkflowEntityAttributes & IModelAttributes) => {
		if (workflow.currentVersionId) {
			return workflow.versionss.find(v => v.id === workflow.currentVersionId);
		} else if (workflow.versionss) {
			let lastUpdatedVersion = _.maxBy(workflow.versionss, v => v.modified);
			if (lastUpdatedVersion) {
				return lastUpdatedVersion
			}
		}
		return undefined;
	};

		private onContextMenuClick = (menuId: string, workflowId: string) => (event: React.MouseEvent) => {
		event.stopPropagation();
		event.preventDefault();
		contextMenu.show({
			id: menuId,
			event: event,
			props: {workflowId},
		});
	};

	private onConfirmDeleteWorkflow = (args: MenuItemEventHandler) => {
		confirmModal('Confirm', 'Do you want to delete this workflow?')
			.then(() => {
				if (args.props) {
					const workflowId = args.props['workflowId'] as string;
					this.deleteWorkflow(workflowId);
				}
			})
			.catch(() => console.log('Not deleting workflow'));
	};

	private deleteWorkflow = (workflowId: string) => {
		new WorkflowEntity({id: workflowId}).delete()
			.then(() => {
				const idx = this.data.workflows.findIndex(w => w.id === workflowId);
				runInAction(() => this.data.workflows.splice(idx, 1));
				alert('Deleted Workflow', 'success');
			})
			.catch(error => {
				console.error(error);
				alert('Could not delete workflow', 'error');
			});
	};

	private getWorkflowTile = (workflow: IWorkflowEntityAttributes & IModelAttributes, index: number) => {
		let workflowVersion = this.getWorkflowVersion(workflow);
		let workflowName = workflowVersion ? workflowVersion.workflowName : workflow.name;
		let workflowVersionLink = workflowVersion && workflowVersion.id
			? `/admin/workflows/edit/${workflowVersion.id}`
			: '/admin/workflows/create';

		let entityAssociations: string[] = [];

		if (workflowVersion) {
			if (workflowVersion.seatsAssociation) {
				entityAssociations.push('seats');
			}
		}

		if (workflow.id != null) {
			const menuId = 'workflow-item-more-' + workflow.id;
			return (
				<React.Fragment key={workflow.id}>
					<ContextMenu
						menuId={menuId}
						actions={[
							{label: 'Delete', onClick: this.onConfirmDeleteWorkflow},
						]} />
					<Link key={index} to={workflowVersionLink}>
						<div className='workflow-item'>
							<div className='workflow-item__heading'>
								<h3> {workflowName} </h3>
								<p> Entities: {entityAssociations.map((e, index) =>
									index === entityAssociations.length - 1 ? [e] : [e, ', '])} </p>
							</div>
							<div className='workflow-item__footer'>
								<Button
									icon={{icon: 'more-horizontal', iconPos: 'icon-left'}}
									onClick={this.onContextMenuClick(menuId, workflow.id)}
								/>
							</div>
						</div>
					</Link>
				</React.Fragment>
			);
		}
		return null;
	};

	private createTile = (
		<Link to= {`/admin/workflows/create`} >
			<div className='workflow-item__new'>
				<h3 className="icon-plus icon-bottom">New Workflow </h3>
			</div>
		</Link>
	);

	public componentDidMount() {
		store.apolloClient
			.query<workflowResponse>({
				query: getWorkflowListQuery(),
				fetchPolicy: 'network-only',
			})
			.then(r => {
				runInAction(() => {
					this.requestState = 'done';
					this.data = r.data;
				});
			})
			.catch(e => {
				runInAction(() => {
					this.requestState = 'error';
					this.error = e;
				})
			});
	}

	private renderContents = (body: React.ReactNode) => {
		return (
			<section className='workflow-block-items'>
				{this.createTile}
				{body}
			</section>
		);
	};

	public render(){
		switch (this.requestState) {
			case 'loading':
				return this.renderContents(null);
			case 'error':
				return this.renderContents(
					'Something went wrong while connecting to the server. The error is '
					+ JSON.stringify(this.error)
				);
			case 'done':
				return this.renderContents(
					this.data.workflows.map((workflow, index) => this.getWorkflowTile(workflow, index))
				);

		}
	}
}