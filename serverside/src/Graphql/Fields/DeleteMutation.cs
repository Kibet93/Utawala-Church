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
using System.Threading.Tasks;
using Utawalaaltar.Graphql.Helpers;
using Utawalaaltar.Graphql.Types;
using Utawalaaltar.Helpers;
using Utawalaaltar.Models;
using Utawalaaltar.Services;
using GraphQL;
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

namespace Utawalaaltar.Graphql.Fields
{
	public class DeleteMutation
	{
		/// <summary>
		/// Creates a mutation that will delete things from the database
		/// </summary>
		/// <param name="name">The name of the model to delete</param>
		/// <typeparam name="TModel">The type of the model to delete</typeparam>
		/// <returns>A function that takes a graphql context and returns a list the deleted ids</returns>
		public static Func<IResolveFieldContext<object>, Task<object>> CreateDeleteMutation<TModel>(string name)
			where TModel : class, IOwnerAbstractModel, new()
		{
			// % protected region % [Override CreateDeleteMutation here] off begin
			return async context =>
			{
				var graphQlContext = (UtawalaaltarGraphQlContext) context.UserContext;
				var crudService = graphQlContext.CrudService;
				var ids = context.GetArgument<List<Guid>>($"{name}Ids".ToCamelCase());

				try
				{
					if (ids == null)
					{
						throw new AggregateException(new Exception("No ids provided to delete, aborting!"));
					}

					var deletedIds = await crudService.Delete<TModel>(ids);
					return IdObject.FromList(deletedIds);
				}
				catch (AggregateException exception)
				{
					context.Errors.AddRange(
						exception.InnerExceptions.Select(error => new ExecutionError(error.Message)));
					return new List<TModel>();
				}
			};
			// % protected region % [Override CreateDeleteMutation here] end
		}

		/// <summary>
		/// Creates a mutation that will delete things from the database by a where condition
		/// </summary>
		/// <param name="name">The name of the model to delete</param>
		/// <typeparam name="TModel">The type of the model to delete</typeparam>
		/// <returns>A function that takes a graphql context and returns whether the delete is successful</returns>
		public static Func<IResolveFieldContext<object>, Task<object>> CreateConditionalDeleteMutation<TModel>(string name)
			where TModel : class, IOwnerAbstractModel, new()
		{
			// % protected region % [Override CreateConditionalDeleteMutation here] off begin
			return async context =>
			{
				var graphQlContext = (UtawalaaltarGraphQlContext)context.UserContext;
				var crudService = graphQlContext.CrudService;
				var args = new CommonArguments(context);

				var models = graphQlContext.DbContext.Set<TModel>()
					.AddConditionalWhereFilter(args.Conditions)
					.AddWhereFilter(args.Where)
					.AddIdCondition(args.Id)
					.AddIdsCondition(args.Ids);

				try
				{
					return await crudService.ConditionalDelete(models);
				}
				catch (AggregateException exception)
				{
					context.Errors.AddRange(
						exception.InnerExceptions.Select(error => new ExecutionError(error.Message)));
					return false;
				}
			};
			// % protected region % [Override CreateConditionalDeleteMutation here] end
		}
	}
}