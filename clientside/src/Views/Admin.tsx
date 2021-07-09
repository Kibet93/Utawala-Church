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
import { action } from 'mobx';
import * as AdminPages from './Pages/Admin/Entity';
import Auth from "./Components/Auth/Auth";
import AllUsersPage from './Pages/Admin/AllUsersPage';
import AdminPage from './Pages/Admin/AdminPage';
import Topbar from "./Components/Topbar/Topbar";
import PageLinks from './Pages/Admin/PageLinks';
import StyleguidePage from './Pages/Admin/StyleguidePage';
import Spinner from 'Views/Components/Spinner/Spinner';
import { Redirect, Route, RouteComponentProps, Switch } from 'react-router';
import { store } from "Models/Store";
import FormsPage from "./Pages/Admin/Forms/FormsPage";
import WorkflowsPage from "./Pages/Admin/Workflows/WorkflowsPage";
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

// % protected region % [Customize lazy imports here] off begin
const GraphiQlLazy = React.lazy(() => import("./Pages/Admin/Graphiql"));
// % protected region % [Customize lazy imports here] end

export default class Admin extends React.Component<RouteComponentProps> {
	private path = this.props.match.path === '/' ? '' : this.props.match.path;
	@action
	private setAppLocation = () => {
		store.appLocation = 'admin';
	}

	public componentDidMount() {
		this.setAppLocation();
	}

	public render() {
		return (
			<>
				<div className="body-container">
					{
					// % protected region % [Modify Topbar] off begin
					}
					<Topbar currentLocation="admin" />
					{
					// % protected region % [Modify Topbar] end
					}
					<Switch>
						<Route path={`${this.path}/styleguide`} >
							<Auth {...this.props}>
								<div className="admin">
									<PageLinks {...this.props} />
								</div>
								<div className="frontend">
									<div className="body-content">
										<StyleguidePage {...this.props}/>
									</div>
								</div>
							</Auth>
						</Route>
						<Route>
							<div className="admin">
								<Auth {...this.props}>
									<Switch>
										{
										// % protected region % [Modify top level admin routing here] off begin
										}
										<Route path={`${this.path}/graphiql`}>
											<React.Suspense fallback={<Spinner />}>
												<GraphiQlLazy />
											</React.Suspense>
										</Route>
										<Route component={this.adminSwitch} />
										{
										// % protected region % [Modify top level admin routing here] end
										}
									</Switch>
								</Auth>
							</div>
						</Route>
					</Switch>
				</div>
			</>
		);
	}

	private adminSwitch = () => {
		if (!store.userGroups.some(ug => ug.hasBackendAccess)) {
			return <Redirect to="/404" />;
		}

		return (
			<>
				{
				// % protected region % [Override contents here] off begin
				}
				<PageLinks {...this.props} />
				{
				// % protected region % [Override contents here] end
				}
				<div className="body-content">
					<Switch>
						{/* These routes require a login to view */}

						{/* Admin entity pages */}
						<Route exact={true} path={`${this.path}`} component={AdminPage} />
						<Route path={`${this.path}/User`} component={AllUsersPage} />
						<Route path={`${this.path}/forms`} component={FormsPage} />
						<Route path={`${this.path}/workflows`} component={WorkflowsPage} />
						<Route path={`${this.path}/AccountabilityGroupEntity`} component={AdminPages.AccountabilityGroupEntityPage} />
						<Route path={`${this.path}/CategoryGroupLeaderEntity`} component={AdminPages.CategoryGroupLeaderEntityPage} />
						<Route path={`${this.path}/GroupCategoryEntity`} component={AdminPages.GroupCategoryEntityPage} />
						<Route path={`${this.path}/MemberEntity`} component={AdminPages.MemberEntityPage} />
						<Route path={`${this.path}/NoOfServiceEntity`} component={AdminPages.NoOfServiceEntityPage} />
						<Route path={`${this.path}/AdminEntity`} component={AdminPages.AdminEntityPage} />
						<Route path={`${this.path}/AttendanceEntity`} component={AdminPages.AttendanceEntityPage} />
						<Route path={`${this.path}/HomeFellowshipEntity`} component={AdminPages.HomeFellowshipEntityPage} />
						<Route path={`${this.path}/ProtocolEntity`} component={AdminPages.ProtocolEntityPage} />
						<Route path={`${this.path}/SeatsEntity`} component={AdminPages.SeatsEntityPage} />
						<Route path={`${this.path}/ServicesEntity`} component={AdminPages.ServicesEntityPage} />
						<Route path={`${this.path}/UsherEntity`} component={AdminPages.UsherEntityPage} />
						<Route path={`${this.path}/WorkflowEntity`} component={AdminPages.WorkflowEntityPage} />
						<Route path={`${this.path}/WorkflowStateEntity`} component={AdminPages.WorkflowStateEntityPage} />
						<Route path={`${this.path}/WorkflowTransitionEntity`} component={AdminPages.WorkflowTransitionEntityPage} />
						<Route path={`${this.path}/WorkflowVersionEntity`} component={AdminPages.WorkflowVersionEntityPage} />
						<Route path={`${this.path}/MemberSubmissionEntity`} component={AdminPages.MemberSubmissionEntityPage} />
						<Route path={`${this.path}/MemberEntityFormTileEntity`} component={AdminPages.MemberEntityFormTileEntityPage} />

						{
						// % protected region % [Add any extra page routes here] off begin
						}
						{
						// % protected region % [Add any extra page routes here] end
						}
					</Switch>
				</div>
				{
				// % protected region % [Add any admin footer content here] off begin
				}
				{
				// % protected region % [Add any admin footer content here] end
				}
			</>
		);
	}
}