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
	[Table("HomeFellowship")]
	// % protected region % [Configure entity attributes here] end
	// % protected region % [Modify class declaration here] off begin
	public class HomeFellowshipEntity : IOwnerAbstractModel 
	// % protected region % [Modify class declaration here] end
	{
		[Key]
		public Guid Id { get; set; }
		public Guid Owner { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		// % protected region % [Customise FellowshipID here] off begin
		[EntityAttribute]
		public int? FellowshipID { get; set; }
		// % protected region % [Customise FellowshipID here] end

		// % protected region % [Customise FellowshipName here] off begin
		[EntityAttribute]
		public String FellowshipName { get; set; }
		// % protected region % [Customise FellowshipName here] end

		// % protected region % [Customise FellowshipPastor here] off begin
		[EntityAttribute]
		public String FellowshipPastor { get; set; }
		// % protected region % [Customise FellowshipPastor here] end

		// % protected region % [Add any further attributes here] off begin
		// % protected region % [Add any further attributes here] end

		public HomeFellowshipEntity()
		{
			// % protected region % [Add any constructor logic here] off begin
			// % protected region % [Add any constructor logic here] end
		}

		// % protected region % [Customise ACL attributes here] off begin
		[NotMapped]
		[JsonIgnore]
		// % protected region % [Customise ACL attributes here] end
		public IEnumerable<IAcl> Acls => new List<IAcl>
		{
			// % protected region % [Override ACLs here] off begin
			new SuperAdministratorsScheme(),
			new VisitorsHomeFellowshipEntity(),
			new AdminHomeFellowshipEntity(),
			new MemberHomeFellowshipEntity(),
			new CategoryGroupLeaderHomeFellowshipEntity(),
			new UsherHomeFellowshipEntity(),
			new ProtocolHomeFellowshipEntity(),
			new GroupCategoryHomeFellowshipEntity(),
			// % protected region % [Override ACLs here] end
			// % protected region % [Add any further ACL entries here] off begin
			// % protected region % [Add any further ACL entries here] end
		};

		// % protected region % [Customise Membersfellowships here] off begin
		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Utawalaaltar.Models.MemberEntity"/>
		[EntityForeignKey("Membersfellowships", "HomeFellowship", false, typeof(MemberEntity))]
		public ICollection<MemberEntity> Membersfellowships { get; set; }
		// % protected region % [Customise Membersfellowships here] end

		public async Task BeforeSave(
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

		public async Task AfterSave(
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

		public async Task<int> CleanReference<T>(
			string reference,
			IEnumerable<T> models,
			UtawalaaltarDBContext dbContext,
			CancellationToken cancellation = default)
			where T : IOwnerAbstractModel
			{
			var modelList = models.Cast<HomeFellowshipEntity>().ToList();
			var ids = modelList.Select(t => t.Id).ToList();

			switch (reference)
			{
				case "Membersfellowships":
					var membersfellowshipIds = modelList.SelectMany(x => x.Membersfellowships.Select(m => m.Id)).ToList();
					var oldmembersfellowship = await dbContext.MemberEntity
						.Where(m => m.HomeFellowshipId.HasValue && ids.Contains(m.HomeFellowshipId.Value))
						.Where(m => !membersfellowshipIds.Contains(m.Id))
						.ToListAsync(cancellation);

					foreach (var membersfellowship in oldmembersfellowship)
					{
						membersfellowship.HomeFellowshipId = null;
					}

					dbContext.MemberEntity.UpdateRange(oldmembersfellowship);
					return oldmembersfellowship.Count;
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