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
	public class WorkflowStateEntityType : ObjectGraphType<WorkflowStateEntity>
	{
		public WorkflowStateEntityType()
		{
			Description = @"State within a workflow";

			// Add model fields to type
			Field(o => o.Id, type: typeof(NonNullGraphType<IdGraphType>));
			Field(o => o.Created, type: typeof(NonNullGraphType<DateTimeGraphType>));
			Field(o => o.Modified, type: typeof(NonNullGraphType<DateTimeGraphType>));
			Field(o => o.DisplayIndex, type: typeof(IntGraphType));
			Field(o => o.StepName, type: typeof(StringGraphType)).Description(@"The name of the state");
			Field(o => o.StateDescription, type: typeof(StringGraphType));
			Field(o => o.IsStartState, type: typeof(BooleanGraphType));
			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

			// Add entity references
			Field(o => o.WorkflowVersionId, type: typeof(IdGraphType));

			// GraphQL reference to entity WorkflowVersionEntity via reference WorkflowVersion
			Field<WorkflowVersionEntityType, WorkflowVersionEntity>()
				.Name("WorkflowVersion")
				.ResolveAsync(async context =>
				{
					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();

					var loader = accessor.Context.GetOrAddBatchLoader<Guid, WorkflowVersionEntity>(
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetWorkflowVersionForWorkflowStateEntity",
						async keys =>
						{
							var results = await QueryHelpers.BuildQueryResolver<WorkflowVersionEntity>(
								context,
								x => keys.Contains(x.Id));
							return results.ToDictionary(x => x.Id, x => x);
						});

					return loader.LoadAsync(context.Source.WorkflowVersionId);
				});

			// GraphQL reference to entity WorkflowTransitionEntity via reference OutgoingTransitions
			Field<ListGraphType<NonNullGraphType<WorkflowTransitionEntityType>>, IEnumerable<WorkflowTransitionEntity>>()
				.Name("OutgoingTransitionss")
				.AddCommonArguments()
				.ResolveAsync(async context =>
				{
					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();

					var loader = accessor.Context.GetOrAddCollectionBatchLoader<Guid, WorkflowTransitionEntity>(
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetOutgoingTransitionssForWorkflowStateEntity",
						async keys =>
						{
							var args = new CommonArguments(context);
							var query = QueryHelpers.CreateResolveFunction<WorkflowTransitionEntity>(context, new ReadOptions {DisableAudit = true});
							var results = await query
								.Where(x => keys.Contains(x.SourceStateId))
								.Select(x => x.SourceStateId)
								.Distinct()
								.SelectMany(x => query
									.Where(y => y.SourceStateId == x)
									.AddIdCondition(args.Id)
									.AddIdsCondition(args.Ids)
									.AddWhereFilter(args.Where)
									.AddConditionalWhereFilter(args.Conditions)
									.AddConditionalHasFilter(args.Has, ((UtawalaaltarGraphQlContext) context.UserContext).ServiceProvider)
									.AddOrderBys(args.OrderBy)
									.AddSkip(args.Skip)
									.AddTake(args.Take))
								.ToListAsync(context.CancellationToken);
							return results.ToLookup(x => x.SourceStateId, x => x);
						});

					return loader.LoadAsync(context.Source.Id);
				});

			// GraphQL reference to entity WorkflowTransitionEntity via reference IncomingTransitions
			Field<ListGraphType<NonNullGraphType<WorkflowTransitionEntityType>>, IEnumerable<WorkflowTransitionEntity>>()
				.Name("IncomingTransitionss")
				.AddCommonArguments()
				.ResolveAsync(async context =>
				{
					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();

					var loader = accessor.Context.GetOrAddCollectionBatchLoader<Guid, WorkflowTransitionEntity>(
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetIncomingTransitionssForWorkflowStateEntity",
						async keys =>
						{
							var args = new CommonArguments(context);
							var query = QueryHelpers.CreateResolveFunction<WorkflowTransitionEntity>(context, new ReadOptions {DisableAudit = true});
							var results = await query
								.Where(x => keys.Contains(x.TargetStateId))
								.Select(x => x.TargetStateId)
								.Distinct()
								.SelectMany(x => query
									.Where(y => y.TargetStateId == x)
									.AddIdCondition(args.Id)
									.AddIdsCondition(args.Ids)
									.AddWhereFilter(args.Where)
									.AddConditionalWhereFilter(args.Conditions)
									.AddConditionalHasFilter(args.Has, ((UtawalaaltarGraphQlContext) context.UserContext).ServiceProvider)
									.AddOrderBys(args.OrderBy)
									.AddSkip(args.Skip)
									.AddTake(args.Take))
								.ToListAsync(context.CancellationToken);
							return results.ToLookup(x => x.TargetStateId, x => x);
						});

					return loader.LoadAsync(context.Source.Id);
				});

			// GraphQL many to many reference to entity  via reference WorkflowStates
			Field<ListGraphType<NonNullGraphType<SeatsWorkflowStatesType>>, IEnumerable<SeatsWorkflowStates>>()
				.Name("Seatss")
				.AddCommonArguments()
				.ResolveAsync(async context =>
				{
					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();

					var loader = accessor.Context.GetOrAddCollectionBatchLoader<Guid, SeatsWorkflowStates>(
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetSeatssForWorkflowStateEntity",
						async keys =>
						{
							var args = new CommonArguments(context);
							var query = QueryHelpers.CreateResolveFunction<SeatsWorkflowStates>(context, new ReadOptions {DisableAudit = true});
							var results = await query
								.Where(x => keys.Contains(x.WorkflowStatesId))
								.Select(x => x.WorkflowStatesId)
								.Distinct()
								.SelectMany(x => query
									.Where(y => y.WorkflowStatesId == x)
									.AddIdCondition(args.Id)
									.AddIdsCondition(args.Ids)
									.AddWhereFilter(args.Where)
									.AddConditionalWhereFilter(args.Conditions)
									.AddConditionalHasFilter(args.Has, ((UtawalaaltarGraphQlContext) context.UserContext).ServiceProvider)
									.AddOrderBys(args.OrderBy)
									.AddSkip(args.Skip)
									.AddTake(args.Take))
								.ToListAsync(context.CancellationToken);
							return results.ToLookup(x => x.WorkflowStatesId, x => x);
						});

					return loader.LoadAsync(context.Source.Id);
				});

			// % protected region % [Add any extra GraphQL references here] off begin
			// % protected region % [Add any extra GraphQL references here] end
		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class WorkflowStateEntityInputType : InputObjectGraphType<WorkflowStateEntity>
	{
		public WorkflowStateEntityInputType()
		{
			Name = "WorkflowStateEntityInput";
			Description = "The input object for adding a new WorkflowStateEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<IntGraphType>("DisplayIndex");
			Field<StringGraphType>("StepName").Description = @"The name of the state";
			Field<StringGraphType>("StateDescription");
			Field<BooleanGraphType>("IsStartState");

			// Add entity references
			Field<IdGraphType>("WorkflowVersionId");

			// Add references to foreign models to allow nested creation
			Field<WorkflowVersionEntityInputType>("WorkflowVersion");
			Field<ListGraphType<WorkflowTransitionEntityInputType>>("OutgoingTransitionss");
			Field<ListGraphType<WorkflowTransitionEntityInputType>>("IncomingTransitionss");
			Field<ListGraphType<SeatsWorkflowStatesInputType>>("Seatss");

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}

}