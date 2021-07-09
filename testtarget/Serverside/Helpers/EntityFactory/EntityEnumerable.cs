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
using System.Collections.Generic;
using Utawalaaltar.Models;

namespace ServersideTests.Helpers.EntityFactory
{
	/// <summary>
	/// A list of tracked entities
	/// </summary>
	/// <typeparam name="T">The base entity type that is tracked</typeparam>
	public class EntityEnumerable<T> : List<T>
		where T : IAbstractModel
	{
		/// <summary>
		/// A set of all tracked entities
		/// </summary>
		public HashSet<IAbstractModel> AllEntities { get; } = new HashSet<IAbstractModel>();

		/// <summary>
		/// Construct a new entity collection
		/// </summary>
		public EntityEnumerable()
		{

		}

		/// <summary>
		/// Construct a new entity collection with the given elements
		/// </summary>
		/// <param name="enumerable"></param>
		public EntityEnumerable(IEnumerable<T> enumerable)
		{
			foreach (var entity in enumerable)
			{
				Add(entity);
				AllEntities.Add(entity);
			}
		}
	}
}