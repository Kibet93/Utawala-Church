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
	public class HomeFellowshipEntityType : ObjectGraphType<HomeFellowshipEntity>
	{
		public HomeFellowshipEntityType()
		{

			// Add model fields to type
			Field(o => o.Id, type: typeof(NonNullGraphType<IdGraphType>));
			Field(o => o.Created, type: typeof(NonNullGraphType<DateTimeGraphType>));
			Field(o => o.Modified, type: typeof(NonNullGraphType<DateTimeGraphType>));
			Field(o => o.FellowshipID, type: typeof(IntGraphType));
			Field(o => o.FellowshipName, type: typeof(StringGraphType));
			Field(o => o.FellowshipPastor, type: typeof(StringGraphType));
			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

			// Add entity references

			// GraphQL reference to entity MembersEntity via reference Membersfellowship
			Field<ListGraphType<NonNullGraphType<MembersEntityType>>, IEnumerable<MembersEntity>>()
				.Name("Membersfellowships")
				.AddCommonArguments()
				.ResolveAsync(async context =>
				{
					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();

					var loader = accessor.Context.GetOrAddCollectionBatchLoader<Guid?, MembersEntity>(
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetMembersfellowshipsForHomeFellowshipEntity",
						async keys =>
						{
							var args = new CommonArguments(context);
							var query = QueryHelpers.CreateResolveFunction<MembersEntity>(context, new ReadOptions {DisableAudit = true});
							var results = await query
								.Where(x => x.HomeFellowshipId.HasValue && keys.Contains(x.HomeFellowshipId))
								.Select(x => x.HomeFellowshipId.Value)
								.Distinct()
								.SelectMany(x => query
									.Where(y => y.HomeFellowshipId == x)
									.AddIdCondition(args.Id)
									.AddIdsCondition(args.Ids)
									.AddWhereFilter(args.Where)
									.AddConditionalWhereFilter(args.Conditions)
									.AddConditionalHasFilter(args.Has, ((UtawalaaltarGraphQlContext) context.UserContext).ServiceProvider)
									.AddOrderBys(args.OrderBy)
									.AddSkip(args.Skip)
									.AddTake(args.Take))
								.ToListAsync(context.CancellationToken);
							return results.ToLookup(x => x.HomeFellowshipId, x => x);
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
	public class HomeFellowshipEntityInputType : InputObjectGraphType<HomeFellowshipEntity>
	{
		public HomeFellowshipEntityInputType()
		{
			Name = "HomeFellowshipEntityInput";
			Description = "The input object for adding a new HomeFellowshipEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<IntGraphType>("FellowshipID");
			Field<StringGraphType>("FellowshipName");
			Field<StringGraphType>("FellowshipPastor");

			// Add entity references

			// Add references to foreign models to allow nested creation
			Field<ListGraphType<MembersEntityInputType>>("Membersfellowships");

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}

}