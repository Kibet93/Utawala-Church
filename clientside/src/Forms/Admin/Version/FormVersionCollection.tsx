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
import {Query} from "react-apollo";
import {gql} from "apollo-boost";
import {FormEntityDataAttributes} from "Forms/FormEntityData";
import {NewFormVersionTile} from "Forms/Admin/Version/NewFormVersionTile";
import {IModelAttributes} from "Models/Model";
import {FormVersionTile} from "Forms/Admin/Version/FormVersionTile";
import { lowerCaseFirst } from 'Util/StringUtils';
import { observer } from 'mobx-react';
import { computed } from 'mobx';
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

// % protected region % [Override getFormListQuery here] off begin
export function getFormListQuery (formType: string) {
	return gql`query fetch {
		forms: ${formType}s{
			id
			created
			modified
			name
			publishedVersionId
			publishedVersion {
				id
				created
				modified
				version
			}
			formVersions{
				id
				created
				modified
				version
			}
		}
	}`;
}
// % protected region % [Override getFormListQuery here] end

// % protected region % [Override IFormVersionCollectionProps here] off begin
export interface IFormVersionCollectionProps {
	formName: string;
	formDisplayName: string;
	showCreateTile : boolean;
}
// % protected region % [Override IFormVersionCollectionProps here] end

// % protected region % [Override types here] off begin
type responseData = FormEntityDataAttributes & IModelAttributes;
type formResponse = {
	forms: responseData[]
};
// % protected region % [Override types here] end

// % protected region % [Add any extra types here] off begin
// % protected region % [Add any extra types here] end

// % protected region % [Override FormVersionCollection here] off begin
@observer
export default class FormVersionCollection extends React.Component<IFormVersionCollectionProps>{
// % protected region % [Override FormVersionCollection here] end

	// % protected region % [Override createTile here] off begin
	@computed
	private get createTile() {
		return this.props.showCreateTile
			? <NewFormVersionTile formName={this.props.formName} formDisplayName={this.props.formDisplayName} />
			: null;
	}
	// % protected region % [Override createTile here] end

	// % protected region % [Override getFormTile here] off begin
	private getFormTile = (form: responseData) => {
		const currentVersion = !form.formVersions ? undefined : _.maxBy(form.formVersions, 'version');
		const currentVersionNumber = !currentVersion ? undefined : currentVersion.version;
		const publishedVersionNumber = !form.publishedVersion ? undefined : form.publishedVersion.version;

		if (form.id){
			return <FormVersionTile
				formVersionName={form.name}
				formEntityName={this.props.formName}
				id={form.id}
				key={form.id}
				currentVersion={currentVersionNumber}
				publishedVersion={publishedVersionNumber}
			/>;
		}
		return null;
	};
	// % protected region % [Override getFormTile here] end
	// % protected region % [Add any extra functions here] off begin
	// % protected region % [Add any extra functions here] end

	// % protected region % [Override render here] off begin
	public render(){
		return(
			<section className='forms-block-items'>
				{this.createTile}
				<Query<formResponse> query={getFormListQuery(lowerCaseFirst(this.props.formName))} fetchPolicy="network-only">
					{({loading, error, data}) => {
						if (loading){
							return null;
						}
						if (error){
							console.error(error);
							return 'Something went wrong while connecting to the server. The error is ' + JSON.stringify(error);
						}
						if (data) {
							return data.forms.map(form =>
								this.getFormTile(form)
							);
						}
						return null
					}}
				</Query>
			</section>
		)
	}
	// % protected region % [Override render here] end
}