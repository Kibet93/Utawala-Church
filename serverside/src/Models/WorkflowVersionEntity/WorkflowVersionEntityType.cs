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
	public class WorkflowVersionEntityType : ObjectGraphType<WorkflowVersionEntity>
	{
		public WorkflowVersionEntityType()
		{
			Description = @"Version of Workflow";

			// Add model fields to type
			Field(o => o.Id, type: typeof(NonNullGraphType<IdGraphType>));
			Field(o => o.Created, type: typeof(NonNullGraphType<DateTimeGraphType>));
			Field(o => o.Modified, type: typeof(NonNullGraphType<DateTimeGraphType>));
			Field(o => o.WorkflowName, type: typeof(StringGraphType)).Description(@"Workflow Name");
			Field(o => o.WorkflowDescription, type: typeof(StringGraphType)).Description(@"Description of Workflow");
			Field(o => o.VersionNumber, type: typeof(IntGraphType)).Description(@"Version Number of Workflow Version");
			Field(o => o.SeatsAssociation, type: typeof(BooleanGraphType)).Description(@"If Seats's are associated with this workflow version");
			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

			// Add entity references
			Field(o => o.WorkflowId, type: typeof(IdGraphType));

			// GraphQL reference to entity WorkflowStateEntity via reference States
			Field<ListGraphType<NonNullGraphType<WorkflowStateEntityType>>, IEnumerable<WorkflowStateEntity>>()
				.Name("Statess")
				.AddCommonArguments()
				.ResolveAsync(async context =>
				{
					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();

					var loader = accessor.Context.GetOrAddCollectionBatchLoader<Guid, WorkflowStateEntity>(
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetStatessForWorkflowVersionEntity",
						async keys =>
						{
							var args = new CommonArguments(context);
							var query = QueryHelpers.CreateResolveFunction<WorkflowStateEntity>(context, new ReadOptions {DisableAudit = true});
							var results = await query
								.Where(x => keys.Contains(x.WorkflowVersionId))
								.Select(x => x.WorkflowVersionId)
								.Distinct()
								.SelectMany(x => query
									.Where(y => y.WorkflowVersionId == x)
									.AddIdCondition(args.Id)
									.AddIdsCondition(args.Ids)
									.AddWhereFilter(args.Where)
									.AddConditionalWhereFilter(args.Conditions)
									.AddConditionalHasFilter(args.Has, ((UtawalaaltarGraphQlContext) context.UserContext).ServiceProvider)
									.AddOrderBys(args.OrderBy)
									.AddSkip(args.Skip)
									.AddTake(args.Take))
								.ToListAsync(context.CancellationToken);
							return results.ToLookup(x => x.WorkflowVersionId, x => x);
						});

					return loader.LoadAsync(context.Source.Id);
				});

			// GraphQL reference to entity WorkflowEntity via reference Workflow
			Field<WorkflowEntityType, WorkflowEntity>()
				.Name("Workflow")
				.ResolveAsync(async context =>
				{
					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();

					var loader = accessor.Context.GetOrAddBatchLoader<Guid, WorkflowEntity>(
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetWorkflowForWorkflowVersionEntity",
						async keys =>
						{
							var results = await QueryHelpers.BuildQueryResolver<WorkflowEntity>(
								context,
								x => keys.Contains(x.Id));
							return results.ToDictionary(x => x.Id, x => x);
						});

					return loader.LoadAsync(context.Source.WorkflowId);
				});

			// GraphQL reference to entity WorkflowEntity via reference CurrentWorkflow
			Field<WorkflowEntityType, WorkflowEntity>()
				.Name("CurrentWorkflow")
				.ResolveAsync(async context =>
				{
					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();

					var loader = accessor.Context.GetOrAddBatchLoader<Guid?, WorkflowEntity>(
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetCurrentWorkflowForWorkflowVersionEntity",
						async keys =>
						{
							var results = await QueryHelpers.BuildQueryResolver<WorkflowEntity>(
								context,
								x => keys.Contains(x.CurrentVersionId));
							return results.ToDictionary(x => x.CurrentVersionId, x => x);
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
	public class WorkflowVersionEntityInputType : InputObjectGraphType<WorkflowVersionEntity>
	{
		public WorkflowVersionEntityInputType()
		{
			Name = "WorkflowVersionEntityInput";
			Description = "The input object for adding a new WorkflowVersionEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<StringGraphType>("WorkflowName").Description = @"Workflow Name";
			Field<StringGraphType>("WorkflowDescription").Description = @"Description of Workflow";
			Field<IntGraphType>("VersionNumber").Description = @"Version Number of Workflow Version";
			Field<BooleanGraphType>("SeatsAssociation").Description = @"If Seats's are associated with this workflow version";

			// Add entity references
			Field<IdGraphType>("WorkflowId");

			// Add references to foreign models to allow nested creation
			Field<ListGraphType<WorkflowStateEntityInputType>>("Statess");
			Field<WorkflowEntityInputType>("Workflow");
			Field<WorkflowEntityInputType>("CurrentWorkflow");

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}

}