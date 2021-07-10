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
	public class MembersEntityType : ObjectGraphType<MembersEntity>
	{
		public MembersEntityType()
		{

			// Add model fields to type
			Field(o => o.Id, type: typeof(NonNullGraphType<IdGraphType>));
			Field(o => o.Created, type: typeof(NonNullGraphType<DateTimeGraphType>));
			Field(o => o.Modified, type: typeof(NonNullGraphType<DateTimeGraphType>));
			Field(o => o.Email, type: typeof(StringGraphType));
			Field(o => o.FullName, type: typeof(StringGraphType));
			Field(o => o.NationalID, type: typeof(StringGraphType));
			Field(o => o.Residence, type: typeof(StringGraphType));
			Field(o => o.DateOfBirth, type: typeof(DateTimeGraphType)).Description(@"For age Calculation");
			Field(o => o.Age, type: typeof(IntGraphType)).Description(@"Current Age");
			Field(o => o.Status, type: typeof(EnumerationGraphType<Status>));
			Field(o => o.MembershipStatus, type: typeof(EnumerationGraphType<Membershipstatus>));
			Field(o => o.CategoryChoice, type: typeof(EnumerationGraphType<CategoryGroups>));
			Field(o => o.AccountabilityGrp, type: typeof(IntGraphType));
			Field(o => o.PictureId, type: typeof(IdGraphType)).Description(@"Profile Picture");
			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

			// Add entity references
			Field(o => o.AccountabilityGroupId, type: typeof(IdGraphType));
			Field(o => o.HomeFellowshipId, type: typeof(IdGraphType));

			// GraphQL reference to entity AccountabilityGroupsEntity via reference AccountabilityGroup
			Field<AccountabilityGroupsEntityType, AccountabilityGroupsEntity>()
				.Name("AccountabilityGroup")
				.ResolveAsync(async context =>
				{
					if (!context.Source.AccountabilityGroupId.HasValue)
					{
						return null;
					}

					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();

					var loader = accessor.Context.GetOrAddBatchLoader<Guid?, AccountabilityGroupsEntity>(
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetAccountabilityGroupForMembersEntity",
						async keys =>
						{
							var results = await QueryHelpers.BuildQueryResolver<AccountabilityGroupsEntity>(
								context,
								x => keys.Contains(x.Id));
							return results.ToDictionary(x => new Guid?(x.Id), x => x);
						});

					return loader.LoadAsync(context.Source.AccountabilityGroupId);
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
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetHomeFellowshipForMembersEntity",
						async keys =>
						{
							var results = await QueryHelpers.BuildQueryResolver<HomeFellowshipEntity>(
								context,
								x => keys.Contains(x.Id));
							return results.ToDictionary(x => new Guid?(x.Id), x => x);
						});

					return loader.LoadAsync(context.Source.HomeFellowshipId);
				});

			// GraphQL reference to entity CategoryLeadersEntity via reference CategoryGroupLeader
			Field<CategoryLeadersEntityType, CategoryLeadersEntity>()
				.Name("CategoryGroupLeader")
				.ResolveAsync(async context =>
				{
					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();

					var loader = accessor.Context.GetOrAddBatchLoader<Guid, CategoryLeadersEntity>(
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetCategoryGroupLeaderForMembersEntity",
						async keys =>
						{
							var results = await QueryHelpers.BuildQueryResolver<CategoryLeadersEntity>(
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
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetProtocolForMembersEntity",
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
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetUshersForMembersEntity",
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
	public class MembersEntityInputType : InputObjectGraphType<MembersEntity>
	{
		public MembersEntityInputType()
		{
			Name = "MembersEntityInput";
			Description = "The input object for adding a new MembersEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<StringGraphType>("FullName");
			Field<StringGraphType>("NationalID");
			Field<StringGraphType>("Residence");
			Field<DateTimeGraphType>("DateOfBirth").Description = @"For age Calculation";
			Field<IntGraphType>("Age").Description = @"Current Age";
			Field<EnumerationGraphType<Status>>("Status");
			Field<EnumerationGraphType<Membershipstatus>>("MembershipStatus");
			Field<EnumerationGraphType<CategoryGroups>>("CategoryChoice");
			Field<IntGraphType>("AccountabilityGrp");
			Field(o => o.PictureId, type: typeof(IdGraphType)).Description(@"Profile Picture");

			// Add entity references
			Field<IdGraphType>("AccountabilityGroupId");
			Field<IdGraphType>("HomeFellowshipId");

			// Add references to foreign models to allow nested creation
			Field<AccountabilityGroupsEntityInputType>("AccountabilityGroup");
			Field<HomeFellowshipEntityInputType>("HomeFellowship");
			Field<CategoryLeadersEntityInputType>("CategoryGroupLeader");
			Field<ProtocolEntityInputType>("Protocol");
			Field<UsherEntityInputType>("Ushers");

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}

	/// <summary>
	/// The GraphQL input type for creating a user entity
	/// </summary>
	public class MembersEntityCreateInputType : InputObjectGraphType<MembersEntity>
	{
		public MembersEntityCreateInputType()
		{
			Name = "MembersEntityCreateInput";
			Description = "The input object for creating a new MembersEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");

			// Add fields specific to a user entity
			Field<StringGraphType>("Email");
			Field<StringGraphType>("Password");

			Field<StringGraphType>("FullName");
			Field<StringGraphType>("NationalID");
			Field<StringGraphType>("Residence");
			Field<DateTimeGraphType>("DateOfBirth").Description = @"For age Calculation";
			Field<IntGraphType>("Age").Description = @"Current Age";
			Field<EnumerationGraphType<Status>>("Status");
			Field<EnumerationGraphType<Membershipstatus>>("MembershipStatus");
			Field<EnumerationGraphType<CategoryGroups>>("CategoryChoice");
			Field<IntGraphType>("AccountabilityGrp");
			Field(o => o.PictureId, type: typeof(IdGraphType)).Description(@"Profile Picture");

			// Add entity references
			Field<IdGraphType>("AccountabilityGroupId");
			Field<IdGraphType>("HomeFellowshipId");


			// Add references to foreign models to allow nested creation
			Field<AccountabilityGroupsEntityInputType>("AccountabilityGroup");
			Field<HomeFellowshipEntityInputType>("HomeFellowship");
			Field<CategoryLeadersEntityInputType>("CategoryGroupLeader");
			Field<ProtocolEntityInputType>("Protocol");
			Field<UsherEntityInputType>("Ushers");

			// % protected region % [Add any extra GraphQL create input fields here] off begin
			// % protected region % [Add any extra GraphQL create input fields here] end
		}
	}
}