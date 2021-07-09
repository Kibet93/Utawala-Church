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
	public class CategoryGroupLeaderEntity : User, IOwnerAbstractModel 
	// % protected region % [Modify class declaration here] end
	{
		// % protected region % [Customise MemberID here] off begin
		[EntityAttribute]
		public int? MemberID { get; set; }
		// % protected region % [Customise MemberID here] end

		// % protected region % [Customise CategoryID here] off begin
		[EntityAttribute]
		public int? CategoryID { get; set; }
		// % protected region % [Customise CategoryID here] end

		// % protected region % [Customise GroupName here] off begin
		[EntityAttribute]
		public String GroupName { get; set; }
		// % protected region % [Customise GroupName here] end

		// % protected region % [Add any further attributes here] off begin
		// % protected region % [Add any further attributes here] end

		public CategoryGroupLeaderEntity()
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
			new VisitorsCategoryGroupLeaderEntity(),
			new AdminCategoryGroupLeaderEntity(),
			new MemberCategoryGroupLeaderEntity(),
			new CategoryGroupLeaderCategoryGroupLeaderEntity(),
			new UsherCategoryGroupLeaderEntity(),
			new ProtocolCategoryGroupLeaderEntity(),
			new GroupCategoryCategoryGroupLeaderEntity(),
			// % protected region % [Override ACLs here] end
			// % protected region % [Add any further ACL entries here] off begin
			// % protected region % [Add any further ACL entries here] end
		};

		// % protected region % [Customise GroupCategory here] off begin
		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.GroupCategoryEntity"/>
		public Guid GroupCategoryId { get; set; }
		[EntityForeignKey("GroupCategory", "CategoryGroupLeaderss", true, typeof(GroupCategoryEntity))]
		public GroupCategoryEntity GroupCategory { get; set; }
		// % protected region % [Customise GroupCategory here] end

		// % protected region % [Customise Member here] off begin
		/// <summary>
		/// Outgoing one to one reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.MemberEntity"/>
		public Guid MemberId { get; set; }
		[EntityForeignKey("Member", "CategoryGroupLeader", true, typeof(MemberEntity))]
		public MemberEntity Member { get; set; }
		// % protected region % [Customise Member here] end

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
			var modelList = models.Cast<CategoryGroupLeaderEntity>().ToList();
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