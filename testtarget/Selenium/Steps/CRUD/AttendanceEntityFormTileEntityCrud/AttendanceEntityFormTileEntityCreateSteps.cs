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
using APITests.EntityObjects.Models;
using SeleniumTests.Setup;
using SeleniumTests.ViewModels.Pages.CRUD.AttendanceEntityFormTileEntityCrud;
using TechTalk.SpecFlow;
// % protected region % [Custom imports] off begin
// % protected region % [Custom imports] end

namespace SeleniumTests.Steps.CRUD.AttendanceEntityFormTileEntityCrud
{
	[Binding]
	public class AttendanceEntityFormTileEntityCreateSteps : StepDefinition
	{
		// % protected region % [Override class properties here] off begin
		public CreateAttendanceEntityFormTileEntityPage CreateAttendanceEntityFormTileEntityPage => new(ContextConfiguration);
		// % protected region % [Override class properties here] end

		// % protected region % [Override constructor here] off begin
		public AttendanceEntityFormTileEntityCreateSteps(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
		}
		// % protected region % [Override constructor here] end

		// % protected region % [Override CreateAValidEntity here] off begin
		[StepDefinition("I create a valid AttendanceEntityFormTileEntity")]
		public void CreateAValidAttendanceEntityFormTileEntity()
		{
			var entity = new AttendanceEntityFormTileEntity(BaseEntity.ConfigureOptions.CREATE_ATTRIBUTES_AND_REFERENCES);
			CreateAttendanceEntityFormTileEntityPage.SetValues(entity);
			CreateAttendanceEntityFormTileEntityPage.ActionButtons.Submit.Click();
		}
		// % protected region % [Override CreateAValidEntity here] end

		// % protected region % [Add class methods here] off begin
		// % protected region % [Add class methods here] end
	}
}