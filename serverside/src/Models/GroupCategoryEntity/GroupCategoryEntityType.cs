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
	public class GroupCategoryEntityType : ObjectGraphType<GroupCategoryEntity>
	{
		public GroupCategoryEntityType()
		{
			Description = @"Men, Women, Young Men, Ladies";

			// Add model fields to type
			Field(o => o.Id, type: typeof(NonNullGraphType<IdGraphType>));
			Field(o => o.Created, type: typeof(NonNullGraphType<DateTimeGraphType>));
			Field(o => o.Modified, type: typeof(NonNullGraphType<DateTimeGraphType>));
			Field(o => o.Email, type: typeof(StringGraphType));
			Field(o => o.Name, type: typeof(StringGraphType));
			Field(o => o.LeaderID, type: typeof(IntGraphType));
			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

			// Add entity references

			// GraphQL reference to entity CategoryGroupLeaderEntity via reference CategoryGroupLeaders
			Field<ListGraphType<NonNullGraphType<CategoryGroupLeaderEntityType>>, IEnumerable<CategoryGroupLeaderEntity>>()
				.Name("CategoryGroupLeaderss")
				.AddCommonArguments()
				.ResolveAsync(async context =>
				{
					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();

					var loader = accessor.Context.GetOrAddCollectionBatchLoader<Guid, CategoryGroupLeaderEntity>(
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetCategoryGroupLeaderssForGroupCategoryEntity",
						async keys =>
						{
							var args = new CommonArguments(context);
							var query = QueryHelpers.CreateResolveFunction<CategoryGroupLeaderEntity>(context, new ReadOptions {DisableAudit = true});
							var results = await query
								.Where(x => keys.Contains(x.GroupCategoryId))
								.Select(x => x.GroupCategoryId)
								.Distinct()
								.SelectMany(x => query
									.Where(y => y.GroupCategoryId == x)
									.AddIdCondition(args.Id)
									.AddIdsCondition(args.Ids)
									.AddWhereFilter(args.Where)
									.AddConditionalWhereFilter(args.Conditions)
									.AddConditionalHasFilter(args.Has, ((UtawalaaltarGraphQlContext) context.UserContext).ServiceProvider)
									.AddOrderBys(args.OrderBy)
									.AddSkip(args.Skip)
									.AddTake(args.Take))
								.ToListAsync(context.CancellationToken);
							return results.ToLookup(x => x.GroupCategoryId, x => x);
						});

					return loader.LoadAsync(context.Source.Id);
				});

			// GraphQL reference to entity MemberEntity via reference Memberscategories
			Field<ListGraphType<NonNullGraphType<MemberEntityType>>, IEnumerable<MemberEntity>>()
				.Name("Memberscategoriess")
				.AddCommonArguments()
				.ResolveAsync(async context =>
				{
					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();

					var loader = accessor.Context.GetOrAddCollectionBatchLoader<Guid?, MemberEntity>(
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetMemberscategoriessForGroupCategoryEntity",
						async keys =>
						{
							var args = new CommonArguments(context);
							var query = QueryHelpers.CreateResolveFunction<MemberEntity>(context, new ReadOptions {DisableAudit = true});
							var results = await query
								.Where(x => x.GroupCategoryId.HasValue && keys.Contains(x.GroupCategoryId))
								.Select(x => x.GroupCategoryId.Value)
								.Distinct()
								.SelectMany(x => query
									.Where(y => y.GroupCategoryId == x)
									.AddIdCondition(args.Id)
									.AddIdsCondition(args.Ids)
									.AddWhereFilter(args.Where)
									.AddConditionalWhereFilter(args.Conditions)
									.AddConditionalHasFilter(args.Has, ((UtawalaaltarGraphQlContext) context.UserContext).ServiceProvider)
									.AddOrderBys(args.OrderBy)
									.AddSkip(args.Skip)
									.AddTake(args.Take))
								.ToListAsync(context.CancellationToken);
							return results.ToLookup(x => x.GroupCategoryId, x => x);
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
	public class GroupCategoryEntityInputType : InputObjectGraphType<GroupCategoryEntity>
	{
		public GroupCategoryEntityInputType()
		{
			Name = "GroupCategoryEntityInput";
			Description = "The input object for adding a new GroupCategoryEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<StringGraphType>("Name");
			Field<IntGraphType>("LeaderID");

			// Add entity references

			// Add references to foreign models to allow nested creation
			Field<ListGraphType<CategoryGroupLeaderEntityInputType>>("CategoryGroupLeaderss");
			Field<ListGraphType<MemberEntityInputType>>("Memberscategoriess");

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}

	/// <summary>
	/// The GraphQL input type for creating a user entity
	/// </summary>
	public class GroupCategoryEntityCreateInputType : InputObjectGraphType<GroupCategoryEntity>
	{
		public GroupCategoryEntityCreateInputType()
		{
			Name = "GroupCategoryEntityCreateInput";
			Description = "The input object for creating a new GroupCategoryEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");

			// Add fields specific to a user entity
			Field<StringGraphType>("Email");
			Field<StringGraphType>("Password");

			Field<StringGraphType>("Name");
			Field<IntGraphType>("LeaderID");

			// Add entity references


			// Add references to foreign models to allow nested creation
			Field<ListGraphType<CategoryGroupLeaderEntityInputType>>("CategoryGroupLeaderss");
			Field<ListGraphType<MemberEntityInputType>>("Memberscategoriess");

			// % protected region % [Add any extra GraphQL create input fields here] off begin
			// % protected region % [Add any extra GraphQL create input fields here] end
		}
	}
}