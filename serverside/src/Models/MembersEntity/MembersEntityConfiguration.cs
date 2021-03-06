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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

namespace Utawalaaltar.Models {
	public class MembersEntityConfiguration : IEntityTypeConfiguration<MembersEntity>
	{
		public void Configure(EntityTypeBuilder<MembersEntity> builder)
		{
			AbstractModelConfiguration.Configure(builder);

			// % protected region % [Override AccountabilityGroup Membersaccountabilitygroups configuration here] off begin
			builder
				.HasOne(e => e.AccountabilityGroup)
				.WithMany(e => e.Membersaccountabilitygroups)
				.OnDelete(DeleteBehavior.Restrict);
			// % protected region % [Override AccountabilityGroup Membersaccountabilitygroups configuration here] end

			// % protected region % [Override HomeFellowship Membersfellowships configuration here] off begin
			builder
				.HasOne(e => e.HomeFellowship)
				.WithMany(e => e.Membersfellowships)
				.OnDelete(DeleteBehavior.Restrict);
			// % protected region % [Override HomeFellowship Membersfellowships configuration here] end

			// % protected region % [Override CategoryGroupLeader Member configuration here] off begin
			builder
				.HasOne(e => e.CategoryGroupLeader)
				.WithOne(e => e.Member)
				.HasForeignKey<CategoryLeadersEntity>(e => e.MemberId)
				.OnDelete(DeleteBehavior.Restrict);
			// % protected region % [Override CategoryGroupLeader Member configuration here] end

			// % protected region % [Override Protocol Member configuration here] off begin
			builder
				.HasOne(e => e.Protocol)
				.WithOne(e => e.Member)
				.HasForeignKey<ProtocolEntity>(e => e.MemberId)
				.OnDelete(DeleteBehavior.Restrict);
			// % protected region % [Override Protocol Member configuration here] end

			// % protected region % [Override Ushers Member configuration here] off begin
			builder
				.HasOne(e => e.Ushers)
				.WithOne(e => e.Member)
				.HasForeignKey<UsherEntity>(e => e.MemberId)
				.OnDelete(DeleteBehavior.Restrict);
			// % protected region % [Override Ushers Member configuration here] end

			// % protected region % [Override Picture configuration here] off begin
			builder
				.HasOne(e => e.Picture)
				.WithOne(e => e.MembersPicture)
				.OnDelete(DeleteBehavior.SetNull);
			// % protected region % [Override Picture configuration here] end

			// % protected region % [Override FullName index configuration here] off begin
			builder.HasIndex(e => e.FullName);
			// % protected region % [Override FullName index configuration here] end

			// % protected region % [Override NationalID index configuration here] off begin
			builder.HasIndex(e => e.NationalID);
			// % protected region % [Override NationalID index configuration here] end

			// % protected region % [Override Residence index configuration here] off begin
			builder.HasIndex(e => e.Residence);
			// % protected region % [Override Residence index configuration here] end

			// % protected region % [Override MembershipStatus index configuration here] off begin
			builder.HasIndex(e => e.MembershipStatus);
			// % protected region % [Override MembershipStatus index configuration here] end

			// % protected region % [Override CategoryChoice index configuration here] off begin
			builder.HasIndex(e => e.CategoryChoice);
			// % protected region % [Override CategoryChoice index configuration here] end

			// % protected region % [Override AccountabilityGrp index configuration here] off begin
			builder.HasIndex(e => e.AccountabilityGrp);
			// % protected region % [Override AccountabilityGrp index configuration here] end

			// % protected region % [Add any extra db model config options here] off begin
			// % protected region % [Add any extra db model config options here] end
		}
	}
}