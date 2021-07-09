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
	public class WorkflowEntityType : ObjectGraphType<WorkflowEntity>
	{
		public WorkflowEntityType()
		{
			Description = @"Workflow within the application";

			// Add model fields to type
			Field(o => o.Id, type: typeof(NonNullGraphType<IdGraphType>));
			Field(o => o.Created, type: typeof(NonNullGraphType<DateTimeGraphType>));
			Field(o => o.Modified, type: typeof(NonNullGraphType<DateTimeGraphType>));
			Field(o => o.Name, type: typeof(StringGraphType)).Description(@"The name of the workflow");
			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

			// Add entity references
			Field(o => o.CurrentVersionId, type: typeof(IdGraphType));

			// GraphQL reference to entity WorkflowVersionEntity via reference Versions
			Field<ListGraphType<NonNullGraphType<WorkflowVersionEntityType>>, IEnumerable<WorkflowVersionEntity>>()
				.Name("Versionss")
				.AddCommonArguments()
				.ResolveAsync(async context =>
				{
					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();

					var loader = accessor.Context.GetOrAddCollectionBatchLoader<Guid, WorkflowVersionEntity>(
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetVersionssForWorkflowEntity",
						async keys =>
						{
							var args = new CommonArguments(context);
							var query = QueryHelpers.CreateResolveFunction<WorkflowVersionEntity>(context, new ReadOptions {DisableAudit = true});
							var results = await query
								.Where(x => keys.Contains(x.WorkflowId))
								.Select(x => x.WorkflowId)
								.Distinct()
								.SelectMany(x => query
									.Where(y => y.WorkflowId == x)
									.AddIdCondition(args.Id)
									.AddIdsCondition(args.Ids)
									.AddWhereFilter(args.Where)
									.AddConditionalWhereFilter(args.Conditions)
									.AddConditionalHasFilter(args.Has, ((UtawalaaltarGraphQlContext) context.UserContext).ServiceProvider)
									.AddOrderBys(args.OrderBy)
									.AddSkip(args.Skip)
									.AddTake(args.Take))
								.ToListAsync(context.CancellationToken);
							return results.ToLookup(x => x.WorkflowId, x => x);
						});

					return loader.LoadAsync(context.Source.Id);
				});

			// GraphQL reference to entity WorkflowVersionEntity via reference CurrentVersion
			Field<WorkflowVersionEntityType, WorkflowVersionEntity>()
				.Name("CurrentVersion")
				.ResolveAsync(async context =>
				{
					if (!context.Source.CurrentVersionId.HasValue)
					{
						return null;
					}

					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();

					var loader = accessor.Context.GetOrAddBatchLoader<Guid?, WorkflowVersionEntity>(
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetCurrentVersionForWorkflowEntity",
						async keys =>
						{
							var results = await QueryHelpers.BuildQueryResolver<WorkflowVersionEntity>(
								context,
								x => keys.Contains(x.Id));
							return results.ToDictionary(x => new Guid?(x.Id), x => x);
						});

					return loader.LoadAsync(context.Source.CurrentVersionId);
				});

			// % protected region % [Add any extra GraphQL references here] off begin
			// % protected region % [Add any extra GraphQL references here] end
		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class WorkflowEntityInputType : InputObjectGraphType<WorkflowEntity>
	{
		public WorkflowEntityInputType()
		{
			Name = "WorkflowEntityInput";
			Description = "The input object for adding a new WorkflowEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<StringGraphType>("Name").Description = @"The name of the workflow";

			// Add entity references
			Field<IdGraphType>("CurrentVersionId");

			// Add references to foreign models to allow nested creation
			Field<ListGraphType<WorkflowVersionEntityInputType>>("Versionss");
			Field<WorkflowVersionEntityInputType>("CurrentVersion");

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}

}