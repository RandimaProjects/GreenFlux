using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SmartCharging.Application.Contracts.Services;
using SmartCharging.Application.Services;
using SmartCharging.Domain.Models;
using SmartCharging.Infrastructure.Context;
using SmartCharging.Infrastructure.Repositories;

namespace SmartCharging.Integration.Test
{
    public class GroupIntegrationTest
    {
        private readonly DbContextOptions<SmartChargingDBContext> _options;
       
        public GroupIntegrationTest()
        {
            _options = SmartChargingIntegrationtestHelper.SmartChargingDbContextOptionsEfCoreInMemory();
            SmartChargingIntegrationtestHelper.CleanDataBase(_options);
            var context = new SmartChargingDBContext(_options);
            context.Database.EnsureDeleted();
            SmartChargingIntegrationtestHelper.CreateDataBaseEfCoreInMemory(_options);

           

        }
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GroupAdd_ShouldSaveAndRetrive_WithNoError()
        {
            using (var context = new SmartChargingDBContext(_options))
            {
                var groupRepository = new GroupRepository(context);
                var groupService = new GroupService(groupRepository);

                var group = new Group { Name = "Group3", Capacity = 100 };
                var result = groupService.Add(group).Result;

                var searchGroup = groupService.GetById(result.Id).Result;

                Assert.NotNull(searchGroup);
                Assert.That(group.Name, Is.EqualTo(searchGroup.Name));
                Assert.That(group.Capacity, Is.EqualTo(searchGroup.Capacity));
            }
        }

        [Test]
        public void GroupDelete_ShouldSaveAndRetrive_WithNoError()
        {
            using (var context = new SmartChargingDBContext(_options))
            {
                var groupRepository = new GroupRepository(context);
                var groupService = new GroupService(groupRepository);

                var group = groupService.GetAll().Result.First();
                var result = groupService.Remove(group).Result;

                var searchGroup = groupService.GetById(group.Id).Result;

                Assert.IsNull(searchGroup);
               
            }
        }
        [Test]
        public void GroupUpdate_ShouldSaveAndRetrive_WithNoError()
        {
            using (var context = new SmartChargingDBContext(_options))
            {
                var groupRepository = new GroupRepository(context);
                var groupService = new GroupService(groupRepository);

                var group = groupService.GetAll().Result.First();
                group.Name = "Testgroup";
                group.Capacity = 50;
                var result = groupService.Update(group).Result;

                var searchGroup = groupService.GetById(group.Id).Result;

                Assert.NotNull(searchGroup);
                Assert.That(group.Name, Is.EqualTo(result.Name));
                Assert.That(group.Capacity, Is.EqualTo(result.Capacity));

            }
        }
    }
}