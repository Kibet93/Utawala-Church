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
using System.Linq;
using Utawalaaltar.Graphql.Helpers;
using Utawalaaltar.Helpers;
using Utawalaaltar.Services;
using GraphQL.DataLoader;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

namespace Utawalaaltar.Models
{
	/// <summary>
	/// The GraphQL type for returning data in GraphQL queries
	/// </summary>
	public class WorkflowTransitionEntityType : ObjectGraphType<WorkflowTransitionEntity>
	{
		public WorkflowTransitionEntityType()
		{
			Description = @"The transtions within the workflows";

			// Add model fields to type
			Field(o => o.Id, type: typeof(NonNullGraphType<IdGraphType>));
			Field(o => o.Created, type: typeof(NonNullGraphType<DateTimeGraphType>));
			Field(o => o.Modified, type: typeof(NonNullGraphType<DateTimeGraphType>));
			Field(o => o.TransitionName, type: typeof(StringGraphType)).Description(@"The name of transition");
			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

			// Add entity references
			Field(o => o.SourceStateId, type: typeof(IdGraphType));
			Field(o => o.TargetStateId, type: typeof(IdGraphType));

			// GraphQL reference to entity WorkflowStateEntity via reference SourceState
			Field<WorkflowStateEntityType, WorkflowStateEntity>()
				.Name("SourceState")
				.ResolveAsync(async context =>
				{
					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();

					var loader = accessor.Context.GetOrAddBatchLoader<Guid, WorkflowStateEntity>(
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetSourceStateForWorkflowTransitionEntity",
						async keys =>
						{
							var results = await QueryHelpers.BuildQueryResolver<WorkflowStateEntity>(
								context,
								x => keys.Contains(x.Id));
							return results.ToDictionary(x => x.Id, x => x);
						});

					return loader.LoadAsync(context.Source.SourceStateId);
				});

			// GraphQL reference to entity WorkflowStateEntity via reference TargetState
			Field<WorkflowStateEntityType, WorkflowStateEntity>()
				.Name("TargetState")
				.ResolveAsync(async context =>
				{
					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();

					var loader = accessor.Context.GetOrAddBatchLoader<Guid, WorkflowStateEntity>(
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetTargetStateForWorkflowTransitionEntity",
						async keys =>
						{
							var results = await QueryHelpers.BuildQueryResolver<WorkflowStateEntity>(
								context,
								x => keys.Contains(x.Id));
							return results.ToDictionary(x => x.Id, x => x);
						});

					return loader.LoadAsync(context.Source.TargetStateId);
				});

			// % protected region % [Add any extra GraphQL references here] off begin
			// % protected region % [Add any extra GraphQL references here] end
		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class WorkflowTransitionEntityInputType : InputObjectGraphType<WorkflowTransitionEntity>
	{
		public WorkflowTransitionEntityInputType()
		{
			Name = "WorkflowTransitionEntityInput";
			Description = "The input object for adding a new WorkflowTransitionEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<StringGraphType>("TransitionName").Description = @"The name of transition";

			// Add entity references
			Field<IdGraphType>("SourceStateId");
			Field<IdGraphType>("TargetStateId");

			// Add references to foreign models to allow nested creation
			Field<WorkflowStateEntityInputType>("SourceState");
			Field<WorkflowStateEntityInputType>("TargetState");

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}

}