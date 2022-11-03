using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SmartCharging.Domain.Models;
using SmartCharging.Infrastructure.Context;
using SmartCharging.Infrastructure.Repositories;

namespace SmartCharging.Repository.Test
{
    public class GroupRepositoryTest
    {
        private readonly DbContextOptions<SmartChargingDBContext> _options;

        public GroupRepositoryTest()
        {
            _options = SmartChargingTestHelper.SmartChargingDbContextOptionsEfCoreInMemory();
            SmartChargingTestHelper.CleanDataBase(_options);
            var context = new SmartChargingDBContext(_options);
            context.Database.EnsureDeleted();
            SmartChargingTestHelper.CreateDataBaseEfCoreInMemory(_options);
        }

        [SetUp]
        public void Setup()
        {
          
        }


        [Test]
        public void GetAll_ShouldReturnAListOfGroup_WhenGroupsExist()
        {
            using (var context = new SmartChargingDBContext(_options))
            {
                var groupRepository = new GroupRepository(context);
                var groups = groupRepository.GetAll();

                Assert.NotNull(groups);
                Assert.That(groups.Result.Any());
            }
        }

        [Test]
        public void GetAll_ShouldMatchedData_WhenGroupExist()
        {
            using (var context = new SmartChargingDBContext(_options))
            {
                var groupRepository = new GroupRepository(context);

                var searchedGroup = CreateGroupList()[1];
                var group = groupRepository.GetAll().Result[2];
               

                Assert.NotNull(group);
                Assert.AreEqual(group.Name, searchedGroup.Name);
                Assert.AreEqual(group.Capacity, searchedGroup.Capacity);
            }
        }


        private List<Group> CreateGroupList()
        {
            var books = new List<Group>();
            books.Add(new Group { Id = 1, Name = "Group1", Capacity = 100 });
            books.Add(new Group { Id = 2, Name = "Group2", Capacity = 50 });

            return books;
        }
    }
}