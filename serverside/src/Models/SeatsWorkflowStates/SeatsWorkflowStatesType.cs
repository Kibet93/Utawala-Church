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
using Utawalaaltar.Services;
using GraphQL.Types;
using GraphQL.DataLoader;
using GraphQL.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

namespace Utawalaaltar.Models 
{
	/// <summary>
	/// The GraphQL type for returning data in GraphQL queries
	/// </summary>
	public class SeatsWorkflowStatesType : ObjectGraphType<SeatsWorkflowStates>
	{
		public SeatsWorkflowStatesType()
		{

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));

			Field(o => o.SeatsId, type: typeof(IdGraphType));
			Field(o => o.WorkflowStatesId, type: typeof(IdGraphType));

			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

			// GraphQL reference to entity SeatsEntity via reference SeatsEntity
			Field<SeatsEntityType, SeatsEntity>()
				.Name("Seats")
				.ResolveAsync(async context =>
				{
					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();

					var loader = accessor.Context.GetOrAddBatchLoader<Guid, SeatsEntity>(
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetSeatsForSeatsWorkflowStates",
						async keys =>
						{
							var results = await QueryHelpers.BuildQueryResolver<SeatsEntity>(
								context,
								x => keys.Contains(x.Id));
							return results.ToDictionary(x => x.Id, x => x);
						});

					return loader.LoadAsync(context.Source.SeatsId);
				});

			// GraphQL reference to entity WorkflowStateEntity via reference WorkflowStateEntity
			Field<WorkflowStateEntityType, WorkflowStateEntity>()
				.Name("WorkflowStates")
				.ResolveAsync(async context =>
				{
					var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
					var accessor = graphQlContext.ServiceProvider.GetRequiredService<IDataLoaderContextAccessor>();

					var loader = accessor.Context.GetOrAddBatchLoader<Guid, WorkflowStateEntity>(
						string.Join("-", context.ResponsePath.Where(x => x is string)) + "GetWorkflowStatesForSeatsWorkflowStates",
						async keys =>
						{
							var results = await QueryHelpers.BuildQueryResolver<WorkflowStateEntity>(
								context,
								x => keys.Contains(x.Id));
							return results.ToDictionary(x => x.Id, x => x);
						});

					return loader.LoadAsync(context.Source.WorkflowStatesId);
				});

			// % protected region % [Add any extra GraphQL references here] off begin
			// % protected region % [Add any extra GraphQL references here] end
		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class SeatsWorkflowStatesInputType : InputObjectGraphType<SeatsWorkflowStates>
	{
		public SeatsWorkflowStatesInputType()
		{
			Name = "SeatsWorkflowStatesInput";
			Description = "The input object for adding a new SeatsWorkflowStates";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<IdGraphType>("SeatsId");
			Field<IdGraphType>("WorkflowStatesId");

			// Add references to foreign objects
			Field<SeatsEntityInputType>("Seats");
			Field<WorkflowStateEntityInputType>("WorkflowStates");

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}
}