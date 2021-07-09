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
using System.Linq;
using FluentAssertions;
using Utawalaaltar.Controllers.Entities;
using Utawalaaltar.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServersideTests.Helpers;
using ServersideTests.Helpers.EntityFactory;
using Xunit;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

// % protected region % [Add any additional imports here] off begin
// % protected region % [Add any additional imports here] end

namespace ServersideTests.Tests.Integration.BotWritten
{
	[Trait("Category", "BotWritten")]
	[Trait("Category", "Unit")]
	public class CrudTests : IDisposable
	{
		private readonly IHost _host;
		private readonly UtawalaaltarDBContext _database;
		private readonly IServiceScope _scope;
		private readonly IServiceProvider _serviceProvider;
		// % protected region % [Add any additional members here] off begin
		// % protected region % [Add any additional members here] end

		public CrudTests()
		{
			// % protected region % [Configure constructor here] off begin
			_host = ServerBuilder.CreateServer();
			_scope = _host.Services.CreateScope();
			_serviceProvider = _scope.ServiceProvider;
			_database = _serviceProvider.GetRequiredService<UtawalaaltarDBContext>();
			// % protected region % [Configure constructor here] end
		}
		public void Dispose()
		{
			// % protected region % [Configure dispose here] off begin
			_host?.Dispose();
			_database?.Dispose();
			_scope?.Dispose();
			// % protected region % [Configure dispose here] end
		}


		// % protected region % [Customise Accountability Group Entity crud tests here] off begin
		[Fact]
		public async void AccountabilityGroupEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<AccountabilityGroupEntityController>();
			var entities = new EntityFactory<AccountabilityGroupEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise Accountability Group Entity crud tests here] end

		// % protected region % [Customise Category Group Leader Entity crud tests here] off begin
		[Fact]
		public async void CategoryGroupLeaderEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<CategoryGroupLeaderEntityController>();
			var entities = new EntityFactory<CategoryGroupLeaderEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise Category Group Leader Entity crud tests here] end

		// % protected region % [Customise Group Category Entity crud tests here] off begin
		[Fact]
		public async void GroupCategoryEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<GroupCategoryEntityController>();
			var entities = new EntityFactory<GroupCategoryEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise Group Category Entity crud tests here] end

		// % protected region % [Customise MEMBER Entity crud tests here] off begin
		[Fact]
		public async void MemberEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<MemberEntityController>();
			var entities = new EntityFactory<MemberEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise MEMBER Entity crud tests here] end

		// % protected region % [Customise No Of Service Entity crud tests here] off begin
		[Fact]
		public async void NoOfServiceEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<NoOfServiceEntityController>();
			var entities = new EntityFactory<NoOfServiceEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise No Of Service Entity crud tests here] end

		// % protected region % [Customise Admin Entity crud tests here] off begin
		[Fact]
		public async void AdminEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<AdminEntityController>();
			var entities = new EntityFactory<AdminEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise Admin Entity crud tests here] end

		// % protected region % [Customise Attendance Entity crud tests here] off begin
		[Fact]
		public async void AttendanceEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<AttendanceEntityController>();
			var entities = new EntityFactory<AttendanceEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise Attendance Entity crud tests here] end

		// % protected region % [Customise Home Fellowship Entity crud tests here] off begin
		[Fact]
		public async void HomeFellowshipEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<HomeFellowshipEntityController>();
			var entities = new EntityFactory<HomeFellowshipEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise Home Fellowship Entity crud tests here] end

		// % protected region % [Customise Protocol Entity crud tests here] off begin
		[Fact]
		public async void ProtocolEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<ProtocolEntityController>();
			var entities = new EntityFactory<ProtocolEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise Protocol Entity crud tests here] end

		// % protected region % [Customise Seats Entity crud tests here] off begin
		[Fact]
		public async void SeatsEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<SeatsEntityController>();
			var entities = new EntityFactory<SeatsEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise Seats Entity crud tests here] end

		// % protected region % [Customise Services Entity crud tests here] off begin
		[Fact]
		public async void ServicesEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<ServicesEntityController>();
			var entities = new EntityFactory<ServicesEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise Services Entity crud tests here] end

		// % protected region % [Customise Usher Entity crud tests here] off begin
		[Fact]
		public async void UsherEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<UsherEntityController>();
			var entities = new EntityFactory<UsherEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise Usher Entity crud tests here] end

		// % protected region % [Customise Workflow Entity crud tests here] off begin
		[Fact]
		public async void WorkflowEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<WorkflowEntityController>();
			var entities = new EntityFactory<WorkflowEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise Workflow Entity crud tests here] end

		// % protected region % [Customise Workflow State Entity crud tests here] off begin
		[Fact]
		public async void WorkflowStateEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<WorkflowStateEntityController>();
			var entities = new EntityFactory<WorkflowStateEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise Workflow State Entity crud tests here] end

		// % protected region % [Customise Workflow Transition Entity crud tests here] off begin
		[Fact]
		public async void WorkflowTransitionEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<WorkflowTransitionEntityController>();
			var entities = new EntityFactory<WorkflowTransitionEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise Workflow Transition Entity crud tests here] end

		// % protected region % [Customise Workflow Version Entity crud tests here] off begin
		[Fact]
		public async void WorkflowVersionEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<WorkflowVersionEntityController>();
			var entities = new EntityFactory<WorkflowVersionEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise Workflow Version Entity crud tests here] end

		// % protected region % [Customise MEMBER Submission Entity crud tests here] off begin
		[Fact]
		public async void MemberSubmissionEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<MemberSubmissionEntityController>();
			var entities = new EntityFactory<MemberSubmissionEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise MEMBER Submission Entity crud tests here] end

		// % protected region % [Customise MEMBER Entity Form Tile Entity crud tests here] off begin
		[Fact]
		public async void MemberEntityFormTileEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<MemberEntityFormTileEntityController>();
			var entities = new EntityFactory<MemberEntityFormTileEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise MEMBER Entity Form Tile Entity crud tests here] end

	// % protected region % [Add any additional tests here] off begin
	// % protected region % [Add any additional tests here] end
	}
}