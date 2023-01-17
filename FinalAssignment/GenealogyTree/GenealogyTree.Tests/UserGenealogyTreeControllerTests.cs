using AutoMapper;
using GenealogyTree.Controllers;
using GenealogyTree.Domain.Interfaces;
using GenealogyTree.Domain.Interfaces.IRepositories;
using GenealogyTree.Domain.Models;
using Moq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using GenealogyTree.DTO;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace GenealogyTree.Tests
{
    [TestClass]
    public class UserGenealogyTreeControllerTests
    {
        private Mock<IRelativeService> _mockRelative;
        private Mock<IMapper> _mockMapper;
        private Mock<IUnitOfWork> _mockRepo;
        private UserGenealogyTreeController sut;

        [TestInitialize]
        public void Initialize()
        {
            _mockRelative = new Mock<IRelativeService>();
            _mockMapper = new Mock<IMapper>();
            _mockRepo = new Mock<IUnitOfWork>();
            var mockLogger = new Mock<ILogger<UserGenealogyTreeController>>();
            var mockHttp = new Mock<IHttpContextAccessor>();

            sut = new UserGenealogyTreeController(_mockMapper.Object, _mockRelative.Object,
                mockLogger.Object, mockHttp.Object, _mockRepo.Object);
        }

        [TestMethod]
        public async Task GetCloseRelatives_ReturnsBadRequestForNonExistantPerson()
        {
            var fake = 0;
            _mockRepo.Setup(p => p.Person.ExistAsync(It.IsAny<Expression<Func<Person, bool>>>()))
                .ReturnsAsync(false);
            var actual = await sut.GetCloseRelatives(fake);
            Assert.IsInstanceOfType(actual, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task GetCloseRelatives_ReturnsNoContentForNoRelatives()
        {
            var fake = 0;
            var fakeMainRelative = new MainRelative(new Person());
            _mockRepo.Setup(p => p.Person.ExistAsync(It.IsAny<Expression<Func<Person, bool>>>()))
                .ReturnsAsync(true);
            _mockRelative.Setup(r => r.GetMainRelativeAsync(It.IsAny<int>()))
                .ReturnsAsync(fakeMainRelative);
            var actual = await sut.GetCloseRelatives(fake);
            Assert.IsInstanceOfType(actual, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task GetCloseRelatives_ReturnsOk()
        {
            var fake = 0;
            var fakeMainRelative = new MainRelative(new Person());
            fakeMainRelative.Relatives.Add(new Relative(new Person(), ""));
            _mockRepo.Setup(p => p.Person.ExistAsync(It.IsAny<Expression<Func<Person, bool>>>()))
                .ReturnsAsync(true);
            _mockRelative.Setup(r => r.GetMainRelativeAsync(It.IsAny<int>()))
                .ReturnsAsync(fakeMainRelative);
            var actual = await sut.GetCloseRelatives(fake);
            Assert.IsInstanceOfType(actual, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetCloseRelatives_ReturnsRelativeResponses()
        {
            var fake = 0;
            var fakeMainRelative = new MainRelative(new Person());
            fakeMainRelative.Relatives.Add(new Relative(new Person(), ""));
            var expected = "test name surname";
            var fakeResponse = new RelativeResponse
            {
                NameSurname = expected
            };
            _mockRepo.Setup(p => p.Person.ExistAsync(It.IsAny<Expression<Func<Person, bool>>>()))
                .ReturnsAsync(true);
            _mockRelative.Setup(r => r.GetMainRelativeAsync(It.IsAny<int>()))
                .ReturnsAsync(fakeMainRelative);
            _mockMapper.Setup(m => m.Map<RelativeResponse>(It.IsAny<Relative>()))
                .Returns(fakeResponse);
            var actual = await sut.GetCloseRelatives(fake) as OkObjectResult;
            Assert.AreEqual(expected, (actual.Value as IEnumerable<RelativeResponse>).ToList()[0].NameSurname);
        }
    }
}
