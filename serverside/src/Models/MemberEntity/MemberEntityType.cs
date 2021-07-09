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
using Utawalaaltar.Enums;
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
	public class MemberEntityType : ObjectGraphType<MemberEntity>
	{
		public MemberEntityType()
		{

			// Add model fields to type
			Field(o => o.Id, type: typeof(NonNullGraphType<IdGraphType>));
			Field(o => o.Created, type: typeof(NonNullGraphType<DateTimeGraphType>));
			Field(o => o.Modified, type: typeof(NonNullGraphType<DateTimeGraphType>));
			Field(o => o.Email, type: typeof(StringGraphType));
			Field(o => o.MemberID, type: typeof(IntGraphType));
			Field(o => o.FullName, type: typeof(StringGraphType));
			Field(o => o.NationalID, type: typeof(StringGraphType));
			Field(o => o.Residence, type: typeof(StringGraphType));
			Field(o => o.DateOfBirth, type: typeof(DateTimeGraphType)).Description(@"For age Calculation");
			Field(o => o.Age, type: typeof(IntGraphType)).Description(@"Current Age");
			Field(o => o.CategoryID, type: typeof(IntGraphType));
			Field(o => o.Status, type: typeof(EnumerationGraphType<Status>));
			Field(o => o.MembershipStatus, type: typeof(EnumerationGraphType<Membershipstatus>));
			Field(o => o.Name, type: typeof(StringGraphType));
			Field(o => o.PublishedVersionId, type: typeof(IdGraphType));
			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

			// Add entity references
			Field<ListGraphType<MemberEntityFormVersionType>, IEnumerable<MemberEntityFormVersion>>()
				.Name("FormVersions")
				.AddCommonArguments()
				.ResolveAsync(async context =>
				{
					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();
					var loader = accessor.Context.GetOrAddCollectionBatchLoader<Guid, MemberEntityFormVersion>(
						"GetFormVersionsForMemberEntity",
						async keys =>
						{
							var args = new CommonArguments(context);
							var query = QueryHelpers.CreateResolveFunction<MemberEntityFormVersion>(context, new ReadOptions {DisableAudit = true});
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
			Field<MemberEntityFormVersionType, MemberEntityFormVersion>()
				.Name("PublishedVersion")
				.ResolveAsync(async context =>
				{
					if (!context.Source.PublishedVersionId.HasValue)
					{
						return null;
					}
					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();
					var loader = accessor.Context.GetOrAddBatchLoader<Guid?, MemberEntityFormVersion>(
						"GetSpacePoliceOfficerForIncidentSubmissionEntity",
						async keys =>
						{
							var results = await QueryHelpers.BuildQueryResolver<MemberEntityFormVersion>(
								context,
								x => keys.Contains(x.Id));
							return results.ToDictionary(x => new Guid?(x.Id), x => x);
						});
					return loader.LoadAsync(context.Source.PublishedVersionId);
				});
			Field(o => o.AccountabilityGroupId, type: typeof(IdGraphType));
			Field(o => o.GroupCategoryId, type: typeof(IdGraphType));
			Field(o => o.HomeFellowshipId, type: typeof(IdGraphType));

			// GraphQL reference to entity AccountabilityGroupEntity via reference AccountabilityGroup
			Field<AccountabilityGroupEntityType, AccountabilityGroupEntity>()
				.Name("AccountabilityGroup")
				.ResolveAsync(async context =>
				{
					if (!context.Source.AccountabilityGroupId.HasValue)
					{
						return null;
					}

					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();

					var loader = accessor.Context.GetOrAddBatchLoader<Guid?, AccountabilityGroupEntity>(
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetAccountabilityGroupForMemberEntity",
						async keys =>
						{
							var results = await QueryHelpers.BuildQueryResolver<AccountabilityGroupEntity>(
								context,
								x => keys.Contains(x.Id));
							return results.ToDictionary(x => new Guid?(x.Id), x => x);
						});

					return loader.LoadAsync(context.Source.AccountabilityGroupId);
				});

			// GraphQL reference to entity GroupCategoryEntity via reference GroupCategory
			Field<GroupCategoryEntityType, GroupCategoryEntity>()
				.Name("GroupCategory")
				.ResolveAsync(async context =>
				{
					if (!context.Source.GroupCategoryId.HasValue)
					{
						return null;
					}

					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();

					var loader = accessor.Context.GetOrAddBatchLoader<Guid?, GroupCategoryEntity>(
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetGroupCategoryForMemberEntity",
						async keys =>
						{
							var results = await QueryHelpers.BuildQueryResolver<GroupCategoryEntity>(
								context,
								x => keys.Contains(x.Id));
							return results.ToDictionary(x => new Guid?(x.Id), x => x);
						});

					return loader.LoadAsync(context.Source.GroupCategoryId);
				});

			// GraphQL reference to entity HomeFellowshipEntity via reference HomeFellowship
			Field<HomeFellowshipEntityType, HomeFellowshipEntity>()
				.Name("HomeFellowship")
				.ResolveAsync(async context =>
				{
					if (!context.Source.HomeFellowshipId.HasValue)
					{
						return null;
					}

					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();

					var loader = accessor.Context.GetOrAddBatchLoader<Guid?, HomeFellowshipEntity>(
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetHomeFellowshipForMemberEntity",
						async keys =>
						{
							var results = await QueryHelpers.BuildQueryResolver<HomeFellowshipEntity>(
								context,
								x => keys.Contains(x.Id));
							return results.ToDictionary(x => new Guid?(x.Id), x => x);
						});

					return loader.LoadAsync(context.Source.HomeFellowshipId);
				});

			// GraphQL reference to entity MemberEntityFormTileEntity via reference FormPage
			Field<ListGraphType<NonNullGraphType<MemberEntityFormTileEntityType>>, IEnumerable<MemberEntityFormTileEntity>>()
				.Name("FormPages")
				.AddCommonArguments()
				.ResolveAsync(async context =>
				{
					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();

					var loader = accessor.Context.GetOrAddCollectionBatchLoader<Guid, MemberEntityFormTileEntity>(
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetFormPagesForMemberEntity",
						async keys =>
						{
							var args = new CommonArguments(context);
							var query = QueryHelpers.CreateResolveFunction<MemberEntityFormTileEntity>(context, new ReadOptions {DisableAudit = true});
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

			// GraphQL reference to entity CategoryGroupLeaderEntity via reference CategoryGroupLeader
			Field<CategoryGroupLeaderEntityType, CategoryGroupLeaderEntity>()
				.Name("CategoryGroupLeader")
				.ResolveAsync(async context =>
				{
					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();

					var loader = accessor.Context.GetOrAddBatchLoader<Guid, CategoryGroupLeaderEntity>(
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetCategoryGroupLeaderForMemberEntity",
						async keys =>
						{
							var results = await QueryHelpers.BuildQueryResolver<CategoryGroupLeaderEntity>(
								context,
								x => keys.Contains(x.MemberId));
							return results.ToDictionary(x => x.MemberId, x => x);
						});

					return loader.LoadAsync(context.Source.Id);
				});

			// GraphQL reference to entity ProtocolEntity via reference Protocol
			Field<ProtocolEntityType, ProtocolEntity>()
				.Name("Protocol")
				.ResolveAsync(async context =>
				{
					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();

					var loader = accessor.Context.GetOrAddBatchLoader<Guid, ProtocolEntity>(
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetProtocolForMemberEntity",
						async keys =>
						{
							var results = await QueryHelpers.BuildQueryResolver<ProtocolEntity>(
								context,
								x => keys.Contains(x.MemberId));
							return results.ToDictionary(x => x.MemberId, x => x);
						});

					return loader.LoadAsync(context.Source.Id);
				});

			// GraphQL reference to entity UsherEntity via reference Ushers
			Field<UsherEntityType, UsherEntity>()
				.Name("Ushers")
				.ResolveAsync(async context =>
				{
					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();

					var loader = accessor.Context.GetOrAddBatchLoader<Guid, UsherEntity>(
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetUshersForMemberEntity",
						async keys =>
						{
							var results = await QueryHelpers.BuildQueryResolver<UsherEntity>(
								context,
								x => keys.Contains(x.MemberId));
							return results.ToDictionary(x => x.MemberId, x => x);
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
	public class MemberEntityInputType : InputObjectGraphType<MemberEntity>
	{
		public MemberEntityInputType()
		{
			Name = "MemberEntityInput";
			Description = "The input object for adding a new MemberEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<IntGraphType>("MemberID");
			Field<StringGraphType>("FullName");
			Field<StringGraphType>("NationalID");
			Field<StringGraphType>("Residence");
			Field<DateTimeGraphType>("DateOfBirth").Description = @"For age Calculation";
			Field<IntGraphType>("Age").Description = @"Current Age";
			Field<IntGraphType>("CategoryID");
			Field<EnumerationGraphType<Status>>("Status");
			Field<EnumerationGraphType<Membershipstatus>>("MembershipStatus");
			Field<StringGraphType>("Name");
			Field<IdGraphType>("PublishedVersionId").Description = "The current published version for the form";
			Field<ListGraphType<MemberEntityFormVersionInputType>>("FormVersions").Description = "The versions for this form";

			// Add entity references
			Field<IdGraphType>("AccountabilityGroupId");
			Field<IdGraphType>("GroupCategoryId");
			Field<IdGraphType>("HomeFellowshipId");

			// Add references to foreign models to allow nested creation
			Field<AccountabilityGroupEntityInputType>("AccountabilityGroup");
			Field<GroupCategoryEntityInputType>("GroupCategory");
			Field<HomeFellowshipEntityInputType>("HomeFellowship");
			Field<ListGraphType<MemberEntityFormTileEntityInputType>>("FormPages");
			Field<CategoryGroupLeaderEntityInputType>("CategoryGroupLeader");
			Field<ProtocolEntityInputType>("Protocol");
			Field<UsherEntityInputType>("Ushers");

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}

	/// <summary>
	/// The GraphQL input type for creating a user entity
	/// </summary>
	public class MemberEntityCreateInputType : InputObjectGraphType<MemberEntity>
	{
		public MemberEntityCreateInputType()
		{
			Name = "MemberEntityCreateInput";
			Description = "The input object for creating a new MemberEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");

			// Add fields specific to a user entity
			Field<StringGraphType>("Email");
			Field<StringGraphType>("Password");

			Field<IntGraphType>("MemberID");
			Field<StringGraphType>("FullName");
			Field<StringGraphType>("NationalID");
			Field<StringGraphType>("Residence");
			Field<DateTimeGraphType>("DateOfBirth").Description = @"For age Calculation";
			Field<IntGraphType>("Age").Description = @"Current Age";
			Field<IntGraphType>("CategoryID");
			Field<EnumerationGraphType<Status>>("Status");
			Field<EnumerationGraphType<Membershipstatus>>("MembershipStatus");

			// Add entity references
			Field<IdGraphType>("AccountabilityGroupId");
			Field<IdGraphType>("GroupCategoryId");
			Field<IdGraphType>("HomeFellowshipId");

			Field<StringGraphType>("Name");
			Field<IdGraphType>("PublishedVersionId").Description = "The current published version for the form";
			Field<ListGraphType<MemberEntityFormVersionInputType>>("FormVersions").Description = "The versions for this form";

			// Add references to foreign models to allow nested creation
			Field<AccountabilityGroupEntityInputType>("AccountabilityGroup");
			Field<GroupCategoryEntityInputType>("GroupCategory");
			Field<HomeFellowshipEntityInputType>("HomeFellowship");
			Field<ListGraphType<MemberEntityFormTileEntityInputType>>("FormPages");
			Field<CategoryGroupLeaderEntityInputType>("CategoryGroupLeader");
			Field<ProtocolEntityInputType>("Protocol");
			Field<UsherEntityInputType>("Ushers");

			// % protected region % [Add any extra GraphQL create input fields here] off begin
			// % protected region % [Add any extra GraphQL create input fields here] end
		}
	}
}