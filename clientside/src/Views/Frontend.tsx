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
import { store } from "Models/Store";
import { Route, RouteComponentProps, Switch, Redirect } from 'react-router';
import * as Pages from './Pages';
import Logout from "./Components/Logout/Logout";
import Auth from "./Components/Auth/Auth";
import PageNotFound from './Pages/PageNotFound';
import PageAccessDenied from './Pages/PageAccessDenied';
import Topbar from "./Components/Topbar/Topbar";
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

export default class Frontend extends React.Component<RouteComponentProps> {
	@action
	private setAppLocation = () => {
		store.appLocation = 'frontend';
	}

	public componentDidMount() {
		this.setAppLocation();
	}

	public render() {
		const path = this.props.match.path === '/' ? '' : this.props.match.path;

		// % protected region % [Add any extra render logic here] off begin
		// % protected region % [Add any extra render logic here] end

		return (
			<>
				<div className="body-container">
					{
					// % protected region % [Modify Topbar] off begin
					}
					<Topbar currentLocation="frontend" />
					{
					// % protected region % [Modify Topbar] end
					}
					{
					// % protected region % [Modify Frontend] off begin
					}
					<div className="frontend">
					{
					// % protected region % [Modify Frontend] end
					}
						{
						// % protected region % [Add any header content here] off begin
						}
						{
						// % protected region % [Add any header content here] end
						}
						<>
							<Switch>
								{/* Public routes */}
								{
								// % protected region % [customize the universal public routes] off begin
								}
								<Route path="/login" component={Pages.LoginPage} />
								<Route path="/logout" component={Logout} />
								<Route path="/register" component={Pages.RegistrationPage} />
								<Route path="/confirm-email" component={Pages.RegistrationConfirmPage} />
								<Route path="/reset-password-request" component={Pages.ResetPasswordRequestPage} />
								<Route path="/reset-password" component={Pages.ResetPasswordPage} />
								{
								// % protected region % [customize the universal public routes] end
								}

								{
								// % protected region % [customize the SeatBooking public routes] off begin
								}
								<Route path={"/seatbooking"} component={Pages.SeatBookingPage} />
								{
								// % protected region % [customize the SeatBooking public routes] end
								}

								<Route path={`${path}/404`} component={PageNotFound} />
								<Route path={`${path}/403`} component={PageAccessDenied} />

								{
								// % protected region % [Add additional routes here] off begin
								}
								{
								// % protected region % [Add additional routes here] end
								}

								<Auth {...this.props}>
									<Switch>
										{/* These routes require a login to view */}
										{/* Pages from the ui model */}
										{
										// % protected region % [customize the page routes] off begin
										}
										<Redirect exact={true} from={`/`} to={`${path}/home`} />
										<Route path={`${path}/home`} component={Pages.HomePage} />
										<Route path={`${path}/registeredmembers`} component={Pages.RegisteredMembersPage} />
										<Route path={`${path}/serviceattendance`} component={Pages.ServiceAttendancePage} />
										{
										// % protected region % [customize the page routes] end
										}
										{
										// % protected region % [Add any extra page routes here] off begin
										}
										{
										// % protected region % [Add any extra page routes here] end
										}

										<Route component={PageNotFound} />
									</Switch>
								</Auth>
							</Switch>
						</>
						{
						// % protected region % [Add any footer content here] off begin
						}
						{
						// % protected region % [Add any footer content here] end
						}
					</div>
				</div>
			</>
		);
	}
}
