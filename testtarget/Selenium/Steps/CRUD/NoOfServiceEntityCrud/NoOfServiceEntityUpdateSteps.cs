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
using System.Linq;
using APITests.EntityObjects.Models;
using SeleniumTests.Setup;
using SeleniumTests.ViewModels.Pages.CRUD.NoOfServiceEntityCrud;
using TechTalk.SpecFlow;
// % protected region % [Custom imports] off begin
// % protected region % [Custom imports] end

namespace SeleniumTests.Steps.CRUD.NoOfServiceEntityCrud
{
	[Binding]
	public class NoOfServiceEntityUpdateSteps : StepDefinition
	{
		// % protected region % [Override class properties here] off begin
		public EditNoOfServiceEntityPage EditNoOfServiceEntityPage => new(ContextConfiguration);
		public CrudNoOfServiceEntityPage CrudNoOfServiceEntityPage => new (ContextConfiguration);
		// % protected region % [Override class properties here] end

		// % protected region % [Override constructor here] off begin
		public NoOfServiceEntityUpdateSteps(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
		}
		// % protected region % [Override constructor here] end

		// % protected region % [Override UpdateEntityAttributes here] off begin
		[StepDefinition("I insert a valid NoOfServiceEntity, search for it and update")]
		public void InsertSearchAndUpdateNoOfServiceEntity()
		{
			var entity = new NoOfServiceEntity(BaseEntity.ConfigureOptions.CREATE_ATTRIBUTES_AND_REFERENCES);
			var id = entity.Save();
			CrudNoOfServiceEntityPage.SearchInput.Value = id.ToString();
			CrudNoOfServiceEntityPage.SearchButton.Click();
			ContextConfiguration.WebDriverWait.Until(_ => CrudNoOfServiceEntityPage.CrudList.Items.Any(x => x.Id == id));
			CrudNoOfServiceEntityPage.CrudList.Items.First(x => x.Id == id).EditButton.Click();
			var updatedEntity = new NoOfServiceEntity(BaseEntity.ConfigureOptions.CREATE_ATTRIBUTES_AND_REFERENCES);
			EditNoOfServiceEntityPage.SetValues(updatedEntity);
			EditNoOfServiceEntityPage.ActionButtons.Submit.Click();
		}
		// % protected region % [Override UpdateEntityAttributes here] end

		// % protected region % [Add class methods here] off begin
		// % protected region % [Add class methods here] end
	}
}