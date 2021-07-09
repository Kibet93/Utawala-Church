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
using OpenQA.Selenium;
using SeleniumTests.PageObjects.Pages;
using SeleniumTests.Setup;
using SeleniumTests.ViewModels.Components.Attribute;
// % protected region % [Custom imports] off begin
// % protected region % [Custom imports] end

namespace SeleniumTests.ViewModels.Pages.CRUD.AttendanceEntityCrud.Internal
{
	public class AttendanceEntityAttributeCollection : Page
	{
		// % protected region % [Override class properties here] off begin
		public AttributeDatePicker DateOfService => new(By.CssSelector("div.dateOfService"), ContextConfiguration);
		public AttributeTextField ServiceID => new(By.CssSelector("div.serviceID"), ContextConfiguration);
		public AttributeTextField SeatNoID => new(By.CssSelector("div.seatNoID"), ContextConfiguration);
		public AttributeTextField Temperature => new(By.CssSelector("div.temperature"), ContextConfiguration);
		public AttributeCheckbox AttendedService => new(By.CssSelector("div.attendedService"), ContextConfiguration);
		public AttributeTextField ReasonForNotAttending => new(By.CssSelector("div.reasonForNotAttending"), ContextConfiguration);
		public AttributeTextArea Comment => new(By.CssSelector("div.comment"), ContextConfiguration);
		// % protected region % [Override class properties here] end

		// % protected region % [Override constructor here] off begin
		public AttendanceEntityAttributeCollection(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
		}
		// % protected region % [Override constructor here] end

		// % protected region % [Add class methods here] off begin
		// % protected region % [Add class methods here] end
	}
}