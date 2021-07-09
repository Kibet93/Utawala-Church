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
	public class CategoryGroupLeaderEntity : UserBaseEntity 
	{
		// 
		[EntityAttribute]
		public int? MemberID { get; set; }
		// 
		[EntityAttribute]
		public int? CategoryID { get; set; }
		// 
		[EntityAttribute]
		public String GroupName { get; set; }

		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.GroupCategory"/>
		public Guid GroupCategoryId { get; set; }

		/// <summary>
		/// Incoming one to one reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.Member"/>
		public Guid MemberId { get; set; }


		public CategoryGroupLeaderEntity()
		{
			EntityName = "CategoryGroupLeaderEntity";
			EndpointName = "category-group-leader-entity";
			InitialiseReferences();
		}

		public CategoryGroupLeaderEntity(ConfigureOptions option)
		{
			EndpointName = "category-group-leader-entity";
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
				EntityName = "GroupCategoryEntity",
				OppositeName = "GroupCategory",
				Name = "CategoryGroupLeaders",
				Optional = false,
				Type = ReferenceType.ONE,
				OppositeType = ReferenceType.MANY
			});
			References.Add(new Reference
			{
				EntityName = "MemberEntity",
				OppositeName = "Member",
				Name = "CategoryGroupLeader",
				Optional = false,
				Type = ReferenceType.ONE,
				OppositeType = ReferenceType.ONE
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
			return GetInvalidEntities<CategoryGroupLeaderEntity>()
				.Select(x => (x.error, x.entity.ToJson()));
		}
		// % protected region % [Customize GetInvalidMutatedJsons here] end

		public override Dictionary<string, string> ToDictionary()
		{
			var entityVar = new Dictionary<string, string>()
			{
				{"id" , Id.ToString()},
				{"email" , EmailAddress},
				{"password" , Password},
				{"memberID" , MemberID.ToString()},
				{"categoryID" , CategoryID.ToString()},
				{"groupName" , GroupName},
			};

			if (GroupCategoryId != default)
			{
				entityVar["groupCategoryId"] = GroupCategoryId.ToString();
			}
			if (MemberId != default)
			{
				entityVar["memberId"] = MemberId.ToString();
			}

			return entityVar;
		}

		// % protected region % [Customize ToJson here] off begin
		public override RestSharp.JsonObject ToJson()
		{
			var entityVar = new RestSharp.JsonObject
			{
				["id"] = Id,
				["email"] = EmailAddress,
				["password"] = Password,
			};
			if(MemberID != null) 
			{
				entityVar["memberID"] = MemberID;
			}
			if(CategoryID != null) 
			{
				entityVar["categoryID"] = CategoryID;
			}
			if(GroupName != null) 
			{
				entityVar["groupName"] = GroupName.ToString();
			}
			if (GroupCategoryId  != default)
			{
				entityVar["groupCategoryId"] = GroupCategoryId.ToString();
			}
			if (MemberId  != default)
			{
				entityVar["memberId"] = MemberId.ToString();
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
					case "GroupCategoryId":
						ReferenceIdDictionary.Add("GroupCategoryId", guidCollection.FirstOrDefault());
						SetOneReference(key, guidCollection.FirstOrDefault());
						break;
					case "MemberId":
						ReferenceIdDictionary.Add("MemberId", guidCollection.FirstOrDefault());
						SetOneReference(key, guidCollection.FirstOrDefault());
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
				case "GroupCategoryId":
					GroupCategoryId = guid;
					break;
				case "MemberId":
					MemberId = guid;
					break;
				default:
					throw new Exception($"{key} not valid reference key");
			}
		}

		private void SetManyReference (string key, ICollection<Guid> guids)
		{
			switch (key)
			{
				default:
					throw new Exception($"{key} not valid reference key");
			}
		}

		public override List<Guid> GetManyToManyReferences (string reference)
		{
			switch (reference)
			{
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
		public static CategoryGroupLeaderEntity GetEntity(bool isValid, string fixedValue = null)
		{
			if (isValid && !string.IsNullOrEmpty(fixedValue))
			{
				return GetValidEntity(fixedValue);
			}
			return isValid ? GetValidEntity() : GetInvalidEntity<CategoryGroupLeaderEntity>().entity;
		}

		/// <summary>
		/// Created parents entities and set the association id's of this entity
		/// to those of the created parents.
		/// </summary>
		private void SetValidEntityAssociations()
		{

			GroupCategoryId = new GroupCategoryEntity(ConfigureOptions.CREATE_ATTRIBUTES_AND_REFERENCES).Save();


			MemberId = new MemberEntity(ConfigureOptions.CREATE_ATTRIBUTES_AND_REFERENCES).Save();

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
		public static CategoryGroupLeaderEntity GetValidEntity(string fixedStrValue = null)
		{
			var categoryGroupLeaderEntity = new CategoryGroupLeaderEntity
			{
			};
			categoryGroupLeaderEntity.PopulateAttributes();
			// % protected region % [Customize valid entity before return here] off begin
			// % protected region % [Customize valid entity before return here] end

			return categoryGroupLeaderEntity;
		}

		public override Guid Save()
		{
			return CreateUser();
		}
	}
}