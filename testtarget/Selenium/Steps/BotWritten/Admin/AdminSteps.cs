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

using SeleniumTests.PageObjects.BotWritten.Admin;
using SeleniumTests.Setup;
using TechTalk.SpecFlow;
// % protected region % [Add further imports here] off begin
// % protected region % [Add further imports here] end

namespace SeleniumTests.Steps.BotWritten.Admin
{
	[Binding]
	public sealed class AdminSteps  : BaseStepDefinition
	{
		// % protected region % [Override Class Properties here] off begin
		private readonly ContextConfiguration _contextConfiguration;
		private readonly AdminPage _adminPage;
		// % protected region % [Override Class Properties here] end
		// % protected region % [Override class constructor here] off begin
		public AdminSteps(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
			_adminPage = new AdminPage(_contextConfiguration);
		}
		// % protected region % [Override class constructor here] end

		// % protected region % [OverrideINavigateToTheAdminPage here] off begin
		[StepDefinition(@"I navigate to the admin page")]
		public void INavigateToTheAdminPage	()
		{
			_adminPage.Navigate();
		}
		// % protected region % [OverrideINavigateToTheAdminPage here] end
	}
}
