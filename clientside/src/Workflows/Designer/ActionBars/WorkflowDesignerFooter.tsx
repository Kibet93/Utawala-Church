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
import { store } from 'Models/Store';
import { Button, Colors, Display } from 'Views/Components/Button/Button';

export default class WorkflowDesignerFooter extends React.Component<{onSave: () => void}>{
	public render(){
		return (
			<section className="action-bar workflow__action-bar">
				<div className="workflow__status">
				</div>

				<section className="workflow__actions btn-group btn-group--horizontal">
					<Button onClick={() => store.routerHistory.push('/admin/workflows')} colors={Colors.Primary} display={Display.Outline}>Cancel</Button>
					<Button onClick={this.props.onSave} colors={Colors.Primary} display={Display.Solid}>Save</Button>
				</section>
			</section>
		);
	}
}