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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utawalaaltar.Controllers;
using Utawalaaltar.Helpers;
using Utawalaaltar.Models;
using Utawalaaltar.Services;
using Utawalaaltar.Services.Files;
using Utawalaaltar.Services.Interfaces;
using GraphQL;
using GraphQL.NewtonsoftJson;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using ServersideTests.Helpers;
using ServersideTests.Helpers.EntityFactory;
using ServersideTests.Helpers.FileProviders;
using Xunit;

namespace ServersideTests.Tests.Integration.BotWritten
{
	[Trait("Category", "BotWritten")]
	[Trait("Category", "Unit")]
	public class GraphqlFileTests : IDisposable
	{
		private const string FileContents = @"<svg width=""200"" height=""200"" xmlns=""http://www.w3.org/2000/svg"">
	<circle cx=""10"" cy=""10"" r=""2"" fill=""red""/>
	<text x=""20"" y=""35"" class=""small"">{TEXT}</text>
</svg>";

		private readonly IHost _host;
		private readonly UtawalaaltarDBContext _dbContext;
		private readonly IUploadStorageProvider _storageProvider;
		private readonly ICrudService _crudService;
		private readonly IGraphQlService _graphqlService;
		private readonly IIdentityService _identityService;
		private readonly FileController _fileController;

		public GraphqlFileTests()
		{
			_host = ServerBuilder.CreateServer(new ServerBuilderOptions
			{
				ConfigureServices = (collection, options) =>
				{
					collection.AddScoped<IUploadStorageProvider, InMemoryFileProvider>();
				}
			});
			var serviceProvider = _host.Services.CreateScope().ServiceProvider;
			var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();

			_dbContext = serviceProvider.GetRequiredService<UtawalaaltarDBContext>();
			_storageProvider = serviceProvider.GetRequiredService<IUploadStorageProvider>();
			_crudService = serviceProvider.GetRequiredService<ICrudService>();
			_graphqlService = serviceProvider.GetRequiredService<IGraphQlService>();
			_identityService = serviceProvider.GetRequiredService<IIdentityService>();
			_fileController = serviceProvider.GetRequiredService<FileController>();

			_fileController.ControllerContext.HttpContext = httpContextAccessor.HttpContext;
		}

		public void Dispose()
		{
			_host.Dispose();
			_dbContext.Dispose();
			_storageProvider.Dispose();
		}

		/// <summary>
		/// Test for the Picture attribute on the MEMBERS entity
		/// that will will ensure that files can be fetched using the graphql API.
		/// </summary>
		[Fact]
		public async void GetMembersPictureTest()
		{
			// Arrange
			var (dbEntity, fileEntity) = InitialiseEntity<MembersEntity>(false, "MembersEntity");
			var fileToSave = FileContents.Replace("{TEXT}", dbEntity.Id.ToString());
			var fileBytes = Encoding.UTF8.GetBytes(fileToSave);

			dbEntity.Picture = fileEntity;
			_dbContext.Add(dbEntity);
			await _dbContext.SaveChangesAsync();

			await _storageProvider.PutAsync(new StoragePutOptions
			{
				Container = "MembersEntity",
				FileName = fileEntity.FileId,
				Content = new MemoryStream(fileBytes),
			});

			// Act
			var fileId = await FetchFileAsync("MembersEntity", "pictureId");
			var fileResult = await _fileController.Get(fileId, default) as FileStreamResult;

			// Assert
			Assert.NotNull(fileResult?.FileStream);

			using var reader = new StreamReader(fileResult.FileStream);
			var fileContents = reader.ReadToEnd();

			Assert.Equal(fileToSave, fileContents);
		}

		/// <summary>
		/// Test for the Picture attribute on the MembersEntity entity
		/// that will will ensure that files can be saved using the CrudService
		/// </summary>
		[Fact]
		public async void CreateMembersEntityPictureTest()
		{
			// Arrange
			var (dbEntity, _) = InitialiseEntity<MembersEntity>(true);
			var fileToSave = FileContents.Replace("{TEXT}", dbEntity.Id.ToString());
			var fileBytes = Encoding.UTF8.GetBytes(fileToSave);

			dbEntity.PictureId = Guid.NewGuid();

			// Act
			await _crudService.Create(dbEntity, new UpdateOptions
			{
				Files = new FormFileCollection
				{
					new FormFile(
						new MemoryStream(fileBytes),
						0,
						fileBytes.LongLength,
						dbEntity.PictureId.ToString(),
						"file.svg")
					{
						Headers = new HeaderDictionary
						{
							{HeaderNames.ContentType, "image/svg"}
						},
					}
				}
			});

			// Assert
			var entity = _dbContext.MembersEntity.First();
			var file = _dbContext.Files.First(f => f.Id == entity.PictureId);
			var fileStream = await _storageProvider.GetAsync(new StorageGetOptions
			{
				Container = "MembersEntity",
				FileName = file.FileId
			});
			var reader = new StreamReader(fileStream);

			Assert.Equal(fileToSave, reader.ReadToEnd());
		}

		/// <summary>
		/// Initialises a new entity with attributes, references and a random owner and a file for this entity.
		/// </summary>
		/// <param name="disableIds">Should id generation be disabled for this entity</param>
		/// <param name="containerName">The name of the container the file should be stored in</param>
		/// <typeparam name="T">The type of the entity to create</typeparam>
		/// <returns>An entity and file pairing</returns>
		private static (T, UploadFile) InitialiseEntity<T>(bool disableIds = false, string containerName = "")
			where T : class, IAbstractModel, new()
		{
			var ownerId = Guid.NewGuid();
			var dbEntity = new EntityFactory<T>()
				.UseAttributes()
				.UseReferences()
				.UseOwner(ownerId)
				.DisableIdGeneration(disableIds)
				.Generate()
				.First();
			var fileEntity = new EntityFactory<UploadFile>()
				.UseAttributes()
				.UseOwner(ownerId)
				.FreezeAttribute<UploadFile>("Container", containerName)
				.DisableIdGeneration(disableIds)
				.Generate()
				.First();

			return (dbEntity, fileEntity);
		}

		/// <summary>
		/// Fetches the file id from graphql for the first of this entity
		/// </summary>
		/// <param name="entityName">The name of the entity in pascal case</param>
		/// <param name="attributeName">The name of the attribute in camel case</param>
		/// <returns>The file id</returns>
		private async Task<Guid> FetchFileAsync(string entityName, string attributeName)
		{
			var entityNameCamelCase = entityName.LowerCaseFirst();

			await _identityService.RetrieveUserAsync();

			var executionResult = await _graphqlService.Execute(
				$@"query {entityNameCamelCase} {{
					{entityNameCamelCase}s {{
						id
						created
						modified
						{attributeName}
						__typename
					}}
					count{entityName}s {{
						number
						__typename
					}}
				}}
				",
				entityNameCamelCase,
				new JObject().ToInputs(),
				new FormFileCollection(),
				_identityService.User,
				default);

			var results = ((List<object>) ((Dictionary<string, object>) executionResult.Data)[$"{entityNameCamelCase}s"])
				.Cast<Dictionary<string, object>>()
				.Select(x => x[attributeName])
				.Cast<string>();

			return Guid.Parse(results.First());
		}
	}
}
