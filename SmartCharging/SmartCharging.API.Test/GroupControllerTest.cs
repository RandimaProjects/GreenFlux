using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Moq;
using NUnit.Framework;
using SmartCharging.Application.Contracts.Services;
using SmartCharging.Controllers;
using SmartCharging.Domain.Models;
using SmartCharging.Dto;


namespace SmartCharging.API.Test
{
    public class GroupControllerTest
    {
        private readonly GroupController _groupController;
        private readonly Mock<IGroupService> _groupServiceMock;
        private readonly Mock<IChargeStationService> _chargeStationServiceMock;
        private readonly Mock<IConnectorService> _connectorServiceMock;
        private readonly Mock<IMapper> _mapperMock;

        public GroupControllerTest()
        {
            _groupServiceMock = new Mock<IGroupService>();
            _chargeStationServiceMock = new Mock<IChargeStationService>();
            _connectorServiceMock = new Mock<IConnectorService>();
            _mapperMock = new Mock<IMapper>();
            _groupController = new GroupController(_groupServiceMock.Object, _mapperMock.Object, _chargeStationServiceMock.Object, _connectorServiceMock.Object);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetAll_ShouldReturnOk_WhenGroupExists()
        {
            var groupList = GetGroupList();
            _groupServiceMock.Setup(c => c.GetAll()).ReturnsAsync(groupList);

            var result = _groupController.GetAll().Result;

            Assert.IsInstanceOf<OkObjectResult>(result);

        }

        [Test]
        public void GetAll_ShouldReturnOk_WhenGroupDoesNotExists()
        {
            var groupList = new List<Group>();
            _groupServiceMock.Setup(c => c.GetAll()).ReturnsAsync(groupList);

            var result = _groupController.GetAll().Result;

            Assert.IsInstanceOf<OkObjectResult>(result);

        }

        [Test]
        public void Add_ShouldReturnOk_WhenGroupIsAdded()
        {
            var group = CreateGroup();
            var groupDto = MapToDto(group);
            _groupServiceMock.Setup(c => c.Add(group)).ReturnsAsync(group);
            

            var result = _groupController.Add(groupDto).Result;

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void Update_ShouldReturnBadRequest_WhenGroupCapacityInvalid()
        {
            var group = CreateGroup();
            group.ChargeStations = new List<ChargeStation>()
                { new ChargeStation { Id = 1, Name = "TestCharge001", GroupId = group.Id } };
            var groupEditDto = new GroupEditDto { Id = group.Id, Name = group.Name, Capacity = group.Capacity };
            
            
            _connectorServiceMock.Setup(c => c.GetTotalMaxCurrent(group.Id)).Returns(150);
            _groupServiceMock.Setup(c => c.GetById(group.Id)).ReturnsAsync(group);


            var result = _groupController.Update(groupEditDto).Result;

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void Update_ShouldReturnOk_WhenGroupCapacityValid()
        {
            var group = CreateGroup();
            group.ChargeStations = new List<ChargeStation>()
                { new ChargeStation { Id = 1, Name = "TestCharge001", GroupId = group.Id } };
            var groupEditDto = new GroupEditDto { Id = group.Id, Name = group.Name, Capacity = group.Capacity };


            _connectorServiceMock.Setup(c => c.GetTotalMaxCurrent(group.Id)).Returns(80);
            _groupServiceMock.Setup(c => c.GetById(group.Id)).ReturnsAsync(group);


            var result = _groupController.Update(groupEditDto).Result;

            Assert.IsInstanceOf<OkResult>(result);
        }



        private List<Group> GetGroupList()
        {
            return new List<Group>()
            {
                new Group()
                {
                    Id = 1,
                    Name = "Western",
                    Capacity = 200
                },
                new Group()
                {
                    Id = 2,
                    Name = "Central",
                    Capacity = 100
                }
            };
        }

        private Group CreateGroup()
        {
            return new Group { Name = "TestGroup", Capacity = 100 };
        }

        private GroupAddDto MapToDto(Group group)
        {
            return new GroupAddDto { Capacity = group.Capacity, Name = group.Name };
        }
    }
}