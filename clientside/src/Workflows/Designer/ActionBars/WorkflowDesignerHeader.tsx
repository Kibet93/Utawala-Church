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
import { WorkflowDesignerTab } from '../WorkflowDesigner';
import { Button, Display, Sizes } from 'Views/Components/Button/Button';

export interface IWorkflowDesignerHeaderProps {
	selectedTab: WorkflowDesignerTab;
	changeTab : (tab: WorkflowDesignerTab) => void;
}

export default class WorkflowDesignerHeader extends React.Component<IWorkflowDesignerHeaderProps>{
	public render(){
		return (
			<section className="header-bar workflow__header">
				<div className="version-details">
					<h2>Create workflow</h2>
				</div>
				<div className="tabs">
					<ul>
						<li>
							<Button display={Display.Text} sizes={Sizes.Large}
								className={this.props.selectedTab === WorkflowDesignerTab.Details ? 'active' : ''}
								onClick={() => this.props.changeTab(WorkflowDesignerTab.Details)}>
								Details
							</Button>
						</li>
						<li>
							<Button display={Display.Text} sizes={Sizes.Large}
								className={this.props.selectedTab === WorkflowDesignerTab.States ? 'active' : ''}
								onClick={() => this.props.changeTab(WorkflowDesignerTab.States)}>
								States
							</Button>
						</li>
					</ul>
				</div>
			</section>
	)
	}
}