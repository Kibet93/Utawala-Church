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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using EntityObject.Enums;
using APITests.Classes;
using APITests.Attributes;
using APITests.Attributes.Validators;
using RestSharp;
using TestDataLib;

namespace APITests.EntityObjects.Models
{
	public class WorkflowStateEntity : BaseEntity
	{
		// 
		[EntityAttribute]
		public int? DisplayIndex { get; set; }
		// The name of the state
		[Required]
		[EntityAttribute]
		public String StepName { get; set; }
		// 
		[EntityAttribute]
		public String StateDescription { get; set; }
		// 
		[EntityAttribute]
		public Boolean? IsStartState { get; set; }

		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.WorkflowVersion"/>
		public Guid WorkflowVersionId { get; set; }

		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.OutgoingTransitions"/>
		public List<Guid> OutgoingTransitionsIds { get; set; }
		public ICollection<WorkflowTransitionEntity> OutgoingTransitionss { get; set; }

		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.IncomingTransitions"/>
		public List<Guid> IncomingTransitionsIds { get; set; }
		public ICollection<WorkflowTransitionEntity> IncomingTransitionss { get; set; }

		/// <summary>
		/// Incoming many to many reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.Seats"/>
		public List<Guid> SeatsIds { get; set; }
		public ICollection<SeatsWorkflowStates> Seatss { get; set; }


		public WorkflowStateEntity()
		{
			EntityName = "WorkflowStateEntity";
			InitialiseReferences();
		}

		public WorkflowStateEntity(ConfigureOptions option)
		{
			Configure(option);
			InitialiseReferences();
		}

		public override void Configure(ConfigureOptions option)
		{
			switch (option)
			{
				case ConfigureOptions.CREATE_ATTRIBUTES_AND_REFERENCES:
					SetValidEntityAttributes();
					SetValidEntityAssociations();
					break;
				case ConfigureOptions.CREATE_ATTRIBUTES_ONLY:
					SetValidEntityAttributes();
					break;
				case ConfigureOptions.CREATE_REFERENCES_ONLY:
					SetValidEntityAssociations();
					break;
				case ConfigureOptions.CREATE_INVALID_ATTRIBUTES:
					break;
				case ConfigureOptions.CREATE_INVALID_ATTRIBUTES_VALID_REFERENCES:
					SetValidEntityAssociations();
					break;
			}
		}

		private void InitialiseReferences()
		{
			References.Add(new Reference
			{
				EntityName = "WorkflowVersionEntity",
				OppositeName = "WorkflowVersion",
				Name = "States",
				Optional = false,
				Type = ReferenceType.ONE,
				OppositeType = ReferenceType.MANY
			});
			References.Add(new Reference
			{
				EntityName = "SeatsEntity",
				OppositeName = "Seats",
				Name = "WorkflowStates",
				Optional = true,
				Type = ReferenceType.MANY,
				OppositeType = ReferenceType.MANY
			});
		}

		public override (int min, int max) GetLengthValidatorMinMax(string attribute)
		{
			switch(attribute)
			{
				default:
					throw new Exception($"{attribute} does not exist or does not have a length validator");
			}
		}

		// % protected region % [Customize GetInvalidMutatedJsons here] off begin
		/// <summary>
		/// Returns a list of invalid/mutated jsons and expected errors. The expected errors are the errors that
		/// should be returned when trying to use the invalid/mutated jsons in a create api request.
		/// </summary>
		/// <returns></returns>
		public override IEnumerable<(string error, RestSharp.JsonObject jsonObject)> GetInvalidMutatedJsons()
		{
			return GetInvalidEntities<WorkflowStateEntity>()
				.Select(x => (x.error, x.entity.ToJson()));
		}
		// % protected region % [Customize GetInvalidMutatedJsons here] end

		public override Dictionary<string, string> ToDictionary()
		{
			var entityVar = new Dictionary<string, string>()
			{
				{"id" , Id.ToString()},
				{"displayIndex" , DisplayIndex.ToString()},
				{"stepName" , StepName},
				{"stateDescription" , StateDescription},
				{"isStartState" , IsStartState.ToString()},
			};

			if (WorkflowVersionId != default)
			{
				entityVar["workflowVersionId"] = WorkflowVersionId.ToString();
			}

			return entityVar;
		}

		// % protected region % [Customize ToJson here] off begin
		public override RestSharp.JsonObject ToJson()
		{
			var entityVar = new RestSharp.JsonObject
			{
				["id"] = Id,
			};
			if(DisplayIndex != null) 
			{
				entityVar["displayIndex"] = DisplayIndex;
			}
			if(StepName != null) 
			{
				entityVar["stepName"] = StepName.ToString();
			}
			if(StateDescription != null) 
			{
				entityVar["stateDescription"] = StateDescription.ToString();
			}
			if(IsStartState != null) 
			{
				entityVar["isStartState"] = IsStartState.ToString();
			}
			if (WorkflowVersionId  != default)
			{
				entityVar["workflowVersionId"] = WorkflowVersionId.ToString();
			}
			if (OutgoingTransitionsIds != default)
			{
				entityVar["outgoingTransitionss"] = OutgoingTransitionss.Select(x => x.ToJson());
			}
			if (IncomingTransitionsIds != default)
			{
				entityVar["incomingTransitionss"] = IncomingTransitionss.Select(x => x.ToJson());
			}
			if (SeatsIds != default)
			{
				entityVar["seatss"] = FormatManyToManyJsonList("seatsId", SeatsIds);
			}

			return entityVar;
		}
		// % protected region % [Customize ToJson here] end


		public override void SetReferences (Dictionary<string, ICollection<Guid>> entityReferences)
		{
			foreach (var (key, guidCollection) in entityReferences)
			{
				switch (key)
				{
					case "WorkflowVersionId":
						ReferenceIdDictionary.Add("WorkflowVersionId", guidCollection.FirstOrDefault());
						SetOneReference(key, guidCollection.FirstOrDefault());
						break;
					case "SeatsId":
						SetManyReference(key, guidCollection);
						break;
					default:
						throw new Exception($"{key} not valid reference key");
				}
			}
		}

		private void SetOneReference (string key, Guid guid)
		{
			switch (key)
			{
				case "WorkflowVersionId":
					WorkflowVersionId = guid;
					break;
				default:
					throw new Exception($"{key} not valid reference key");
			}
		}

		private void SetManyReference (string key, ICollection<Guid> guids)
		{
			switch (key)
			{
				case "SeatsId":
					SeatsIds = guids.ToList();
					Seatss  = new List<SeatsWorkflowStates>{};
					foreach(var SeatsId in guids)
					{
						Seatss.Add
						(
							new SeatsWorkflowStates()
							{
								WorkflowStatesId = Id,
								SeatsId = SeatsId,
							}
						);
					}
					break;
				default:
					throw new Exception($"{key} not valid reference key");
			}
		}

		public override List<Guid> GetManyToManyReferences (string reference)
		{
			switch (reference)
			{
				case "Seatss":
					return SeatsIds;
				default:
					throw new Exception($"{reference} not valid many to many reference key");
			}
		}

		private List<RestSharp.JsonObject> FormatManyToManyJsonList(string key, List<Guid> values)
		{
			var manyToManyList = new List<RestSharp.JsonObject>();
			values?.ForEach(x => manyToManyList.Add(new RestSharp.JsonObject {[key] = x }));
			return manyToManyList;
		}

		/// <summary>
		/// Gets an entity that violates the validators of its attributes,
		/// if any attributes have a validator to violate.
		/// </summary>
		// TODO needs some warning if trying to get an invalid entity, and the entity
		// attributes don't actually have any validators to violate.
		public static WorkflowStateEntity GetEntity(bool isValid, string fixedValue = null)
		{
			if (isValid && !string.IsNullOrEmpty(fixedValue))
			{
				return GetValidEntity(fixedValue);
			}
			return isValid ? GetValidEntity() : GetInvalidEntity<WorkflowStateEntity>().entity;
		}

		/// <summary>
		/// Created parents entities and set the association id's of this entity
		/// to those of the created parents.
		/// </summary>
		private void SetValidEntityAssociations()
		{

			WorkflowVersionId = new WorkflowVersionEntity(ConfigureOptions.CREATE_ATTRIBUTES_AND_REFERENCES).Save();

		}

		/// <summary>
		/// Gets an entity with attributes that conform to any attribute validators.
		/// </summary>
		private void SetValidEntityAttributes()
		{
			// % protected region % [Override generated entity attributes here] off begin
			PopulateAttributes();
			// % protected region % [Override generated entity attributes here] end
		}

		/// <summary>
		/// Gets an entity with attributes that conform to any attribute validators.
		/// </summary>
		public static WorkflowStateEntity GetValidEntity(string fixedStrValue = null)
		{
			var workflowStateEntity = new WorkflowStateEntity
			{
			};
			workflowStateEntity.PopulateAttributes();
			// % protected region % [Customize valid entity before return here] off begin
			// % protected region % [Customize valid entity before return here] end

			return workflowStateEntity;
		}

		public override Guid Save()
		{
			return SaveThroughGraphQl(this);
		}
	}
}
