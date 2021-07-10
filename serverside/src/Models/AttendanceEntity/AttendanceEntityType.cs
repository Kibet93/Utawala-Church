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
	public class AttendanceEntityType : ObjectGraphType<AttendanceEntity>
	{
		public AttendanceEntityType()
		{

			// Add model fields to type
			Field(o => o.Id, type: typeof(NonNullGraphType<IdGraphType>));
			Field(o => o.Created, type: typeof(NonNullGraphType<DateTimeGraphType>));
			Field(o => o.Modified, type: typeof(NonNullGraphType<DateTimeGraphType>));
			Field(o => o.DateOfService, type: typeof(DateTimeGraphType));
			Field(o => o.ServiceID, type: typeof(IntGraphType));
			Field(o => o.SeatNoID, type: typeof(IntGraphType));
			Field(o => o.Temperature, type: typeof(FloatGraphType));
			Field(o => o.AttendedService, type: typeof(BooleanGraphType));
			Field(o => o.ReasonForNotAttending, type: typeof(StringGraphType));
			Field(o => o.Comment, type: typeof(StringGraphType));
			Field(o => o.Name, type: typeof(StringGraphType));
			Field(o => o.PublishedVersionId, type: typeof(IdGraphType));
			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

			// Add entity references
			Field<ListGraphType<AttendanceEntityFormVersionType>, IEnumerable<AttendanceEntityFormVersion>>()
				.Name("FormVersions")
				.AddCommonArguments()
				.ResolveAsync(async context =>
				{
					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();
					var loader = accessor.Context.GetOrAddCollectionBatchLoader<Guid, AttendanceEntityFormVersion>(
						"GetFormVersionsForAttendanceEntity",
						async keys =>
						{
							var args = new CommonArguments(context);
							var query = QueryHelpers.CreateResolveFunction<AttendanceEntityFormVersion>(context, new ReadOptions {DisableAudit = true});
							var results = await query
								.Where(x => keys.Contains(x.FormId))
								.Select(x => x.FormId)
								.Distinct()
								.SelectMany(x => query
									.Where(y => y.FormId == x)
									.AddIdCondition(args.Id)
									.AddIdsCondition(args.Ids)
									.AddWhereFilter(args.Where)
									.AddConditionalWhereFilter(args.Conditions)
									.AddConditionalHasFilter(args.Has, ((UtawalaaltarGraphQlContext) context.UserContext).ServiceProvider)
									.AddOrderBys(args.OrderBy)
									.AddSkip(args.Skip)
									.AddTake(args.Take))
								.ToListAsync(context.CancellationToken);
							return results.ToLookup(x => x.FormId, x => x);
						});
					return loader.LoadAsync(context.Source.Id);
				});
			Field<AttendanceEntityFormVersionType, AttendanceEntityFormVersion>()
				.Name("PublishedVersion")
				.ResolveAsync(async context =>
				{
					if (!context.Source.PublishedVersionId.HasValue)
					{
						return null;
					}
					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();
					var loader = accessor.Context.GetOrAddBatchLoader<Guid?, AttendanceEntityFormVersion>(
						"GetSpacePoliceOfficerForIncidentSubmissionEntity",
						async keys =>
						{
							var results = await QueryHelpers.BuildQueryResolver<AttendanceEntityFormVersion>(
								context,
								x => keys.Contains(x.Id));
							return results.ToDictionary(x => new Guid?(x.Id), x => x);
						});
					return loader.LoadAsync(context.Source.PublishedVersionId);
				});

			// GraphQL reference to entity AttendanceEntityFormTileEntity via reference FormPage
			Field<ListGraphType<NonNullGraphType<AttendanceEntityFormTileEntityType>>, IEnumerable<AttendanceEntityFormTileEntity>>()
				.Name("FormPages")
				.AddCommonArguments()
				.ResolveAsync(async context =>
				{
					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();

					var loader = accessor.Context.GetOrAddCollectionBatchLoader<Guid, AttendanceEntityFormTileEntity>(
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetFormPagesForAttendanceEntity",
						async keys =>
						{
							var args = new CommonArguments(context);
							var query = QueryHelpers.CreateResolveFunction<AttendanceEntityFormTileEntity>(context, new ReadOptions {DisableAudit = true});
							var results = await query
								.Where(x => keys.Contains(x.FormId))
								.Select(x => x.FormId)
								.Distinct()
								.SelectMany(x => query
									.Where(y => y.FormId == x)
									.AddIdCondition(args.Id)
									.AddIdsCondition(args.Ids)
									.AddWhereFilter(args.Where)
									.AddConditionalWhereFilter(args.Conditions)
									.AddConditionalHasFilter(args.Has, ((UtawalaaltarGraphQlContext) context.UserContext).ServiceProvider)
									.AddOrderBys(args.OrderBy)
									.AddSkip(args.Skip)
									.AddTake(args.Take))
								.ToListAsync(context.CancellationToken);
							return results.ToLookup(x => x.FormId, x => x);
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
	public class AttendanceEntityInputType : InputObjectGraphType<AttendanceEntity>
	{
		public AttendanceEntityInputType()
		{
			Name = "AttendanceEntityInput";
			Description = "The input object for adding a new AttendanceEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<DateTimeGraphType>("DateOfService");
			Field<IntGraphType>("ServiceID");
			Field<IntGraphType>("SeatNoID");
			Field<FloatGraphType>("Temperature");
			Field<BooleanGraphType>("AttendedService");
			Field<StringGraphType>("ReasonForNotAttending");
			Field<StringGraphType>("Comment");
			Field<StringGraphType>("Name");
			Field<IdGraphType>("PublishedVersionId").Description = "The current published version for the form";
			Field<ListGraphType<AttendanceEntityFormVersionInputType>>("FormVersions").Description = "The versions for this form";

			// Add entity references

			// Add references to foreign models to allow nested creation
			Field<ListGraphType<AttendanceEntityFormTileEntityInputType>>("FormPages");

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}

}