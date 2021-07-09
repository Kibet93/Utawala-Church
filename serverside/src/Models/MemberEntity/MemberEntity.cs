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
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utawalaaltar.Enums;
using Utawalaaltar.Security;
using Utawalaaltar.Security.Acl;
using Utawalaaltar.Validators;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Z.EntityFramework.Plus;
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

namespace Utawalaaltar.Models {
	// % protected region % [Configure entity attributes here] off begin
	// % protected region % [Configure entity attributes here] end
	// % protected region % [Modify class declaration here] off begin
	public class MemberEntity : User, IOwnerAbstractModel 
	// % protected region % [Modify class declaration here] end
	{
		[Required]
		[EntityAttribute]
		public string Name { get; set; }

		// % protected region % [Customise MemberID here] off begin
		[EntityAttribute]
		public int? MemberID { get; set; }
		// % protected region % [Customise MemberID here] end

		[Required]
		// % protected region % [Customise FullName here] off begin
		[EntityAttribute]
		public String FullName { get; set; }
		// % protected region % [Customise FullName here] end

		// % protected region % [Customise NationalID here] off begin
		[EntityAttribute]
		public String NationalID { get; set; }
		// % protected region % [Customise NationalID here] end

		// % protected region % [Customise Residence here] off begin
		[EntityAttribute]
		public String Residence { get; set; }
		// % protected region % [Customise Residence here] end

		/// <summary>
		/// For age Calculation
		/// </summary>
		// % protected region % [Customise DateOfBirth here] off begin
		[EntityAttribute]
		public DateTime? DateOfBirth { get; set; }
		// % protected region % [Customise DateOfBirth here] end

		/// <summary>
		/// Current Age
		/// </summary>
		// % protected region % [Customise Age here] off begin
		[EntityAttribute]
		public int? Age { get; set; }
		// % protected region % [Customise Age here] end

		// % protected region % [Customise CategoryID here] off begin
		[EntityAttribute]
		public int? CategoryID { get; set; }
		// % protected region % [Customise CategoryID here] end

		// % protected region % [Customise Status here] off begin
		[EntityAttribute]
		public Status Status { get; set; }
		// % protected region % [Customise Status here] end

		// % protected region % [Customise MembershipStatus here] off begin
		[EntityAttribute]
		public Membershipstatus MembershipStatus { get; set; }
		// % protected region % [Customise MembershipStatus here] end

		// % protected region % [Add any further attributes here] off begin
		// % protected region % [Add any further attributes here] end

		public MemberEntity()
		{
			// % protected region % [Add any constructor logic here] off begin
			// % protected region % [Add any constructor logic here] end
		}

		// % protected region % [Customise ACL attributes here] off begin
		[NotMapped]
		[JsonIgnore]
		// % protected region % [Customise ACL attributes here] end
		public override IEnumerable<IAcl> Acls => new List<IAcl>
		{
			// % protected region % [Override ACLs here] off begin
			new SuperAdministratorsScheme(),
			new VisitorsMemberEntity(),
			new AdminMemberEntity(),
			new MemberMemberEntity(),
			new CategoryGroupLeaderMemberEntity(),
			new UsherMemberEntity(),
			new ProtocolMemberEntity(),
			new GroupCategoryMemberEntity(),
			// % protected region % [Override ACLs here] end
			// % protected region % [Add any further ACL entries here] off begin
			// % protected region % [Add any further ACL entries here] end
		};

		/// <summary>
		/// Reference to the versions for this form
		/// </summary>
		/// <see cref="Utawalaaltar.Models.MemberEntityFormVersion"/>
		[EntityForeignKey("FormVersions", "Form", false, typeof(MemberEntityFormVersion))]
		public ICollection<MemberEntityFormVersion> FormVersions { get; set; }

		/// <summary>
		/// The current published version for the form
		/// </summary>
		/// <see cref="Utawalaaltar.Models.MemberEntityFormVersion"/>
		public Guid? PublishedVersionId { get; set; }
		[EntityForeignKey("PublishedVersion", "PublishedForm", false, typeof(MemberEntityFormVersion))]
		public MemberEntityFormVersion PublishedVersion { get; set; }

		// % protected region % [Customise AccountabilityGroup here] off begin
		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.AccountabilityGroupEntity"/>
		public Guid? AccountabilityGroupId { get; set; }
		[EntityForeignKey("AccountabilityGroup", "Membersaccountabilitygroups", false, typeof(AccountabilityGroupEntity))]
		public AccountabilityGroupEntity AccountabilityGroup { get; set; }
		// % protected region % [Customise AccountabilityGroup here] end

		// % protected region % [Customise GroupCategory here] off begin
		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.GroupCategoryEntity"/>
		public Guid? GroupCategoryId { get; set; }
		[EntityForeignKey("GroupCategory", "Memberscategoriess", false, typeof(GroupCategoryEntity))]
		public GroupCategoryEntity GroupCategory { get; set; }
		// % protected region % [Customise GroupCategory here] end

		// % protected region % [Customise HomeFellowship here] off begin
		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.HomeFellowshipEntity"/>
		public Guid? HomeFellowshipId { get; set; }
		[EntityForeignKey("HomeFellowship", "Membersfellowships", false, typeof(HomeFellowshipEntity))]
		public HomeFellowshipEntity HomeFellowship { get; set; }
		// % protected region % [Customise HomeFellowship here] end

		// % protected region % [Customise FormPages here] off begin
		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.MemberEntityFormTileEntity"/>
		[EntityForeignKey("FormPages", "Form", false, typeof(MemberEntityFormTileEntity))]
		public ICollection<MemberEntityFormTileEntity> FormPages { get; set; }
		// % protected region % [Customise FormPages here] end

		// % protected region % [Customise CategoryGroupLeader here] off begin
		/// <summary>
		/// Incoming one to one reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.CategoryGroupLeaderEntity"/>
		[EntityForeignKey("CategoryGroupLeader", "Member", false, typeof(CategoryGroupLeaderEntity))]
		public CategoryGroupLeaderEntity CategoryGroupLeader { get; set; }
		// % protected region % [Customise CategoryGroupLeader here] end

		// % protected region % [Customise Protocol here] off begin
		/// <summary>
		/// Incoming one to one reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.ProtocolEntity"/>
		[EntityForeignKey("Protocol", "Member", false, typeof(ProtocolEntity))]
		public ProtocolEntity Protocol { get; set; }
		// % protected region % [Customise Protocol here] end

		// % protected region % [Customise Ushers here] off begin
		/// <summary>
		/// Incoming one to one reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.UsherEntity"/>
		[EntityForeignKey("Ushers", "Member", false, typeof(UsherEntity))]
		public UsherEntity Ushers { get; set; }
		// % protected region % [Customise Ushers here] end

		public override async Task BeforeSave(
			EntityState operation,
			UtawalaaltarDBContext dbContext,
			IServiceProvider serviceProvider,
			CancellationToken cancellationToken = default)
		{
			// % protected region % [Add any initial before save logic here] off begin
			// % protected region % [Add any initial before save logic here] end

			// % protected region % [Add any before save logic here] off begin
			// % protected region % [Add any before save logic here] end
		}

		public override async Task AfterSave(
			EntityState operation,
			UtawalaaltarDBContext dbContext,
			IServiceProvider serviceProvider,
			ICollection<ChangeState> changes,
			CancellationToken cancellationToken = default)
		{
			// % protected region % [Add any initial after save logic here] off begin
			// % protected region % [Add any initial after save logic here] end

			// % protected region % [Add any after save logic here] off begin
			// % protected region % [Add any after save logic here] end
		}

		public async override Task<int> CleanReference<T>(
			string reference,
			IEnumerable<T> models,
			UtawalaaltarDBContext dbContext,
			CancellationToken cancellation = default)
			{
			var modelList = models.Cast<MemberEntity>().ToList();
			var ids = modelList.Select(t => t.Id).ToList();

			switch (reference)
			{
				// % protected region % [Add any extra clean reference logic here] off begin
				// % protected region % [Add any extra clean reference logic here] end
				default:
					return 0;
			}
		}

		// % protected region % [Add any further references here] off begin
		// % protected region % [Add any further references here] end
	}
}