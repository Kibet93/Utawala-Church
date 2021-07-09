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
	public class CategoryGroupLeaderEntityType : ObjectGraphType<CategoryGroupLeaderEntity>
	{
		public CategoryGroupLeaderEntityType()
		{

			// Add model fields to type
			Field(o => o.Id, type: typeof(NonNullGraphType<IdGraphType>));
			Field(o => o.Created, type: typeof(NonNullGraphType<DateTimeGraphType>));
			Field(o => o.Modified, type: typeof(NonNullGraphType<DateTimeGraphType>));
			Field(o => o.Email, type: typeof(StringGraphType));
			Field(o => o.MemberID, type: typeof(IntGraphType));
			Field(o => o.CategoryID, type: typeof(IntGraphType));
			Field(o => o.GroupName, type: typeof(StringGraphType));
			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

			// Add entity references
			Field(o => o.GroupCategoryId, type: typeof(IdGraphType));
			Field(o => o.MemberId, type: typeof(IdGraphType));

			// GraphQL reference to entity GroupCategoryEntity via reference GroupCategory
			Field<GroupCategoryEntityType, GroupCategoryEntity>()
				.Name("GroupCategory")
				.ResolveAsync(async context =>
				{
					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();

					var loader = accessor.Context.GetOrAddBatchLoader<Guid, GroupCategoryEntity>(
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetGroupCategoryForCategoryGroupLeaderEntity",
						async keys =>
						{
							var results = await QueryHelpers.BuildQueryResolver<GroupCategoryEntity>(
								context,
								x => keys.Contains(x.Id));
							return results.ToDictionary(x => x.Id, x => x);
						});

					return loader.LoadAsync(context.Source.GroupCategoryId);
				});

			// GraphQL reference to entity MemberEntity via reference Member
			Field<MemberEntityType, MemberEntity>()
				.Name("Member")
				.ResolveAsync(async context =>
				{
					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();

					var loader = accessor.Context.GetOrAddBatchLoader<Guid, MemberEntity>(
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetMemberForCategoryGroupLeaderEntity",
						async keys =>
						{
							var results = await QueryHelpers.BuildQueryResolver<MemberEntity>(
								context,
								x => keys.Contains(x.Id));
							return results.ToDictionary(x => x.Id, x => x);
						});

					return loader.LoadAsync(context.Source.MemberId);
				});

			// % protected region % [Add any extra GraphQL references here] off begin
			// % protected region % [Add any extra GraphQL references here] end
		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class CategoryGroupLeaderEntityInputType : InputObjectGraphType<CategoryGroupLeaderEntity>
	{
		public CategoryGroupLeaderEntityInputType()
		{
			Name = "CategoryGroupLeaderEntityInput";
			Description = "The input object for adding a new CategoryGroupLeaderEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<IntGraphType>("MemberID");
			Field<IntGraphType>("CategoryID");
			Field<StringGraphType>("GroupName");

			// Add entity references
			Field<IdGraphType>("GroupCategoryId");
			Field<IdGraphType>("MemberId");

			// Add references to foreign models to allow nested creation
			Field<GroupCategoryEntityInputType>("GroupCategory");
			Field<MemberEntityInputType>("Member");

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}

	/// <summary>
	/// The GraphQL input type for creating a user entity
	/// </summary>
	public class CategoryGroupLeaderEntityCreateInputType : InputObjectGraphType<CategoryGroupLeaderEntity>
	{
		public CategoryGroupLeaderEntityCreateInputType()
		{
			Name = "CategoryGroupLeaderEntityCreateInput";
			Description = "The input object for creating a new CategoryGroupLeaderEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");

			// Add fields specific to a user entity
			Field<StringGraphType>("Email");
			Field<StringGraphType>("Password");

			Field<IntGraphType>("MemberID");
			Field<IntGraphType>("CategoryID");
			Field<StringGraphType>("GroupName");

			// Add entity references
			Field<IdGraphType>("GroupCategoryId");
			Field<IdGraphType>("MemberId");


			// Add references to foreign models to allow nested creation
			Field<GroupCategoryEntityInputType>("GroupCategory");
			Field<MemberEntityInputType>("Member");

			// % protected region % [Add any extra GraphQL create input fields here] off begin
			// % protected region % [Add any extra GraphQL create input fields here] end
		}
	}
}