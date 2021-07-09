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
	public class WorkflowStateEntityConfiguration : IEntityTypeConfiguration<WorkflowStateEntity>
	{
		public void Configure(EntityTypeBuilder<WorkflowStateEntity> builder)
		{
			AbstractModelConfiguration.Configure(builder);

			// % protected region % [Override WorkflowVersion Statess configuration here] off begin
			builder
				.HasOne(e => e.WorkflowVersion)
				.WithMany(e => e.Statess)
				.OnDelete(DeleteBehavior.Cascade);
			// % protected region % [Override WorkflowVersion Statess configuration here] end

			// % protected region % [Override OutgoingTransitionss SourceState configuration here] off begin
			builder
				.HasMany(e => e.OutgoingTransitionss)
				.WithOne(e => e.SourceState)
				.OnDelete(DeleteBehavior.Cascade);
			// % protected region % [Override OutgoingTransitionss SourceState configuration here] end

			// % protected region % [Override IncomingTransitionss TargetState configuration here] off begin
			builder
				.HasMany(e => e.IncomingTransitionss)
				.WithOne(e => e.TargetState)
				.OnDelete(DeleteBehavior.Cascade);
			// % protected region % [Override IncomingTransitionss TargetState configuration here] end

			// % protected region % [Add any extra db model config options here] off begin
			// % protected region % [Add any extra db model config options here] end
		}
	}
}