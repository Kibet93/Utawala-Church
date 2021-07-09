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
import {RouteComponentProps} from "react-router";
import { Button, Colors, Display, Sizes } from 'Views/Components/Button/Button';
import WorkflowCollection from "Workflows/Landing/WorkflowCollection";

export default class WorkflowsLandingPage extends React.Component<RouteComponentProps>{

	public render(){
		return (
			<section className='workflow-behaviour workflow-behaviour__landing'>
				<div className='behaviour-header'>
					<h2>Workflows</h2>
					<Button className='icon-help icon-right' sizes={Sizes.Small} colors={Colors.Secondary} display={Display.Solid}>Help Documentation</Button>
				</div>

				<p> Behaviour description here </p>

				<WorkflowCollection showCreateTile={true}/>
			</section>
		)
	}
}