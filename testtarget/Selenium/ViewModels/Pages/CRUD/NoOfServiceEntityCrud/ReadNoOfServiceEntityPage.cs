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
using System;
using System.Linq;
using APITests.EntityObjects.Models;
using EntityObject.Enums;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using SeleniumTests.ViewModels.Pages.CRUD.NoOfServiceEntityCrud.Internal;
// % protected region % [Custom imports] off begin
// % protected region % [Custom imports] end

namespace SeleniumTests.ViewModels.Pages.CRUD.NoOfServiceEntityCrud
{
	public class ReadNoOfServiceEntityPage : NoOfServiceEntityAttributeCollection
	{
		// % protected region % [Override class properties here] off begin
		public string Url => ContextConfiguration.BaseUrl + "/admin/noOfServiceEntity/view";
		// % protected region % [Override class properties here] end

		// % protected region % [Override constructor here] off begin
		public ReadNoOfServiceEntityPage(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
		}
		// % protected region % [Override constructor here] end

		// % protected region % [Override get values here] off begin
		public NoOfServiceEntity GetValues()
		{
			var noOfServiceEntity =  new NoOfServiceEntity
			{
				Name = Name.Value,
				Status = Status.Value.ToEnum<Status>(),
			};

			return noOfServiceEntity;
		}
		// % protected region % [Override get values here] end

		// % protected region % [Add class methods here] off begin
		// % protected region % [Add class methods here] end
	}
}