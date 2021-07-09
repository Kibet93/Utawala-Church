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
	public class MemberEntity : UserBaseEntity 
	{
		// Form Name
		public string Name { get; set; }
		// 
		[Unique]
		[EntityAttribute]
		public int? MemberID { get; set; }
		// 
		[Required]
		[EntityAttribute]
		public String FullName { get; set; }
		// 
		[EntityAttribute]
		public String NationalID { get; set; }
		// 
		[EntityAttribute]
		public String Residence { get; set; }
		// For age Calculation
		[EntityAttribute]
		public DateTime? DateOfBirth { get; set; }
		// Current Age
		[EntityAttribute]
		public int? Age { get; set; }
		// 
		[EntityAttribute]
		public int? CategoryID { get; set; }
		// 
		[EntityAttribute]
		public Status Status { get; set; }
		// 
		[EntityAttribute]
		public Membershipstatus MembershipStatus { get; set; }

		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.AccountabilityGroup"/>
		public Guid? AccountabilityGroupId { get; set; }

		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.GroupCategory"/>
		public Guid? GroupCategoryId { get; set; }

		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.HomeFellowship"/>
		public Guid? HomeFellowshipId { get; set; }

		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.FormPage"/>
		public List<Guid> FormPageIds { get; set; }
		public ICollection<MemberEntityFormTileEntity> FormPages { get; set; }

		/// <summary>
		/// Outgoing one to one
		/// </summary>
		/// <see cref="Utawalaaltar.Models.CategoryGroupLeader"/>
		public Guid? CategoryGroupLeaderId { get; set; }

		/// <summary>
		/// Outgoing one to one
		/// </summary>
		/// <see cref="Utawalaaltar.Models.Protocol"/>
		public Guid? ProtocolId { get; set; }

		/// <summary>
		/// Outgoing one to one
		/// </summary>
		/// <see cref="Utawalaaltar.Models.Ushers"/>
		public Guid? UshersId { get; set; }


		public MemberEntity()
		{
			EntityName = "MemberEntity";
			EndpointName = "member-entity";
			InitialiseReferences();
		}

		public MemberEntity(ConfigureOptions option)
		{
			EndpointName = "member-entity";
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
				EntityName = "AccountabilityGroupEntity",
				OppositeName = "AccountabilityGroup",
				Name = "Membersaccountabilitygroup",
				Optional = true,
				Type = ReferenceType.ONE,
				OppositeType = ReferenceType.MANY
			});
			References.Add(new Reference
			{
				EntityName = "GroupCategoryEntity",
				OppositeName = "GroupCategory",
				Name = "Memberscategories",
				Optional = true,
				Type = ReferenceType.ONE,
				OppositeType = ReferenceType.MANY
			});
			References.Add(new Reference
			{
				EntityName = "HomeFellowshipEntity",
				OppositeName = "HomeFellowship",
				Name = "Membersfellowship",
				Optional = true,
				Type = ReferenceType.ONE,
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
			return GetInvalidEntities<MemberEntity>()
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
				{"name" , Name},
				{"memberID" , MemberID.ToString()},
				{"fullName" , FullName},
				{"nationalID" , NationalID},
				{"residence" , Residence},
				{"dateOfBirth" ,((DateTime)DateOfBirth).ToIsoString()},
				{"age" , Age.ToString()},
				{"categoryID" , CategoryID.ToString()},
				{"status" , Status.ToString()},
				{"membershipStatus" , MembershipStatus.ToString()},
			};

			if (AccountabilityGroupId != default)
			{
				entityVar["accountabilityGroupId"] = AccountabilityGroupId.ToString();
			}
			if (GroupCategoryId != default)
			{
				entityVar["groupCategoryId"] = GroupCategoryId.ToString();
			}
			if (HomeFellowshipId != default)
			{
				entityVar["homeFellowshipId"] = HomeFellowshipId.ToString();
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
				["name"] = Name,
			};
			if(MemberID != null) 
			{
				entityVar["memberID"] = MemberID;
			}
			if(FullName != null) 
			{
				entityVar["fullName"] = FullName.ToString();
			}
			if(NationalID != null) 
			{
				entityVar["nationalID"] = NationalID.ToString();
			}
			if(Residence != null) 
			{
				entityVar["residence"] = Residence.ToString();
			}
			if(DateOfBirth != null) 
			{
				entityVar["dateOfBirth"] = DateOfBirth?.ToString("s");
			}
			if(Age != null) 
			{
				entityVar["age"] = Age;
			}
			if(CategoryID != null) 
			{
				entityVar["categoryID"] = CategoryID;
			}
			if(Status != null) 
			{
				entityVar["status"] = Status.ToString();
			}
			if(MembershipStatus != null) 
			{
				entityVar["membershipStatus"] = MembershipStatus.ToString();
			}
			if (AccountabilityGroupId  != default)
			{
				entityVar["accountabilityGroupId"] = AccountabilityGroupId.ToString();
			}
			if (GroupCategoryId  != default)
			{
				entityVar["groupCategoryId"] = GroupCategoryId.ToString();
			}
			if (HomeFellowshipId  != default)
			{
				entityVar["homeFellowshipId"] = HomeFellowshipId.ToString();
			}
			if (FormPageIds != default)
			{
				entityVar["formPages"] = FormPages.Select(x => x.ToJson());
			}
			if (CategoryGroupLeaderId  != default)
			{
				entityVar["categoryGroupLeaderId"] = CategoryGroupLeaderId.ToString();
			}
			if (ProtocolId  != default)
			{
				entityVar["protocolId"] = ProtocolId.ToString();
			}
			if (UshersId  != default)
			{
				entityVar["ushersId"] = UshersId.ToString();
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
					case "AccountabilityGroupId":
						ReferenceIdDictionary.Add("AccountabilityGroupId", guidCollection.FirstOrDefault());
						SetOneReference(key, guidCollection.FirstOrDefault());
						break;
					case "GroupCategoryId":
						ReferenceIdDictionary.Add("GroupCategoryId", guidCollection.FirstOrDefault());
						SetOneReference(key, guidCollection.FirstOrDefault());
						break;
					case "HomeFellowshipId":
						ReferenceIdDictionary.Add("HomeFellowshipId", guidCollection.FirstOrDefault());
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
				case "AccountabilityGroupId":
					AccountabilityGroupId = guid;
					break;
				case "GroupCategoryId":
					GroupCategoryId = guid;
					break;
				case "HomeFellowshipId":
					HomeFellowshipId = guid;
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
		public static MemberEntity GetEntity(bool isValid, string fixedValue = null)
		{
			if (isValid && !string.IsNullOrEmpty(fixedValue))
			{
				return GetValidEntity(fixedValue);
			}
			return isValid ? GetValidEntity() : GetInvalidEntity<MemberEntity>().entity;
		}

		/// <summary>
		/// Created parents entities and set the association id's of this entity
		/// to those of the created parents.
		/// </summary>
		private void SetValidEntityAssociations()
		{
		}

		/// <summary>
		/// Gets an entity with attributes that conform to any attribute validators.
		/// </summary>
		private void SetValidEntityAttributes()
		{
			// % protected region % [Override generated entity attributes here] off begin
			Name = Guid.NewGuid().ToString();
			PopulateAttributes();
			// % protected region % [Override generated entity attributes here] end
		}

		/// <summary>
		/// Gets an entity with attributes that conform to any attribute validators.
		/// </summary>
		public static MemberEntity GetValidEntity(string fixedStrValue = null)
		{
			var memberEntity = new MemberEntity
			{
				Name = Guid.NewGuid().ToString(),
			};
			memberEntity.PopulateAttributes();
			// % protected region % [Customize valid entity before return here] off begin
			// % protected region % [Customize valid entity before return here] end

			return memberEntity;
		}

		public override Guid Save()
		{
			return CreateUser();
		}
	}
}
