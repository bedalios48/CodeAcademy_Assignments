using AutoMapper;
using GenealogyTree.Controllers;
using GenealogyTree.Domain.Interfaces;
using GenealogyTree.Domain.Interfaces.IRepositories;
using GenealogyTree.Domain.Models;
using GenealogyTree.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;

namespace GenealogyTree.Tests
{
    [TestClass()]
    public class UserControllerTests
    {
        private Mock<IUserRepository> _mockRepo;
        private Mock<IJwtService> _mockJwt;
        private Mock<IPasswordService> _mockPassword;
        private Mock<IMapper> _mockMapper;
        private UserController sut;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepo = new Mock<IUserRepository>();
            _mockJwt = new Mock<IJwtService>();
            _mockPassword = new Mock<IPasswordService>();
            _mockMapper = new Mock<IMapper>();
            sut = new UserController(_mockRepo.Object, _mockJwt.Object,
                _mockPassword.Object, _mockMapper.Object);
        }

        [TestMethod]
        public async Task Login_UnauthorizedForBadLogin()
        {
            var fakeRequest = new LoginRequest();
            User fakeUser = null;
            _mockRepo.Setup(r => r.TryLoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(fakeUser);
            var actual = await sut.Login(fakeRequest);
            Assert.IsInstanceOfType(actual, typeof(UnauthorizedObjectResult));
        }

        [TestMethod]
        public async Task Login_ReturnsToken()
        {
            var fakeRequest = new LoginRequest();
            var fakeUser = new User();
            var expected = "test token";
            _mockRepo.Setup(r => r.TryLoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(fakeUser);
            _mockJwt.Setup(j => j.GetJwtToken(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(expected);
            var actual = await sut.Login(fakeRequest) as OkObjectResult;
            Assert.AreEqual(expected, (actual.Value as LoginResponse).Token);
        }

        [TestMethod]
        public async Task Login_ReturnsUserName()
        {
            var expected = "test username";
            var fakeRequest = new LoginRequest
            {
                UserName = expected
            };
            var fakeUser = new User();
            _mockRepo.Setup(r => r.TryLoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(fakeUser);
            var actual = await sut.Login(fakeRequest) as OkObjectResult;
            Assert.AreEqual(expected, (actual.Value as LoginResponse).UserName);
        }

        [TestMethod]
        public async Task Register_BadRequestForExistingUser()
        {
            var fakeRequest = new RegisterRequest();
            _mockRepo.Setup(r => r.ExistAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(true);
            var actual = await sut.Register(fakeRequest);
            Assert.IsInstanceOfType(actual, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task Register_CreatesPasswordHash()
        {
            var fakeRequest = new RegisterRequest();
            var fakeSalt = new byte[0];
            var expected = new byte[1];
            _mockRepo.Setup(r => r.ExistAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(false);
            _mockPassword.Setup(p => p
            .CreatePasswordHash(It.IsAny<string>(), out expected, out fakeSalt));
            var actual = await sut.Register(fakeRequest);
            _mockMapper.Verify(m => m
            .Map<User>(It.Is<(RegisterRequest r, byte[] hash, byte[] salt)>(m => m.hash == expected)),
            Times.Once);
        }

        [TestMethod]
        public async Task Register_CreatesPasswordSalt()
        {
            var fakeRequest = new RegisterRequest();
            var fakeHash = new byte[0];
            var expected = new byte[1];
            _mockRepo.Setup(r => r.ExistAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(false);
            _mockPassword.Setup(p => p
            .CreatePasswordHash(It.IsAny<string>(), out fakeHash, out expected));
            var actual = await sut.Register(fakeRequest);
            _mockMapper.Verify(m => m
            .Map<User>(It.Is<(RegisterRequest r, byte[] hash, byte[] salt)>(m => m.salt == expected)),
            Times.Once);
        }

        [TestMethod]
        public async Task Register_CreatesUser()
        {
            var fakeRequest = new RegisterRequest();
            _mockRepo.Setup(r => r.ExistAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(false);
            var actual = await sut.Register(fakeRequest);
            _mockRepo.Verify(r => r.CreateAsync(It.IsAny<User>()), Times.Once);
        }

        [TestMethod]
        public async Task Register_ReturnsCreated()
        {
            var fakeRequest = new RegisterRequest();
            _mockRepo.Setup(r => r.ExistAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(false);
            var actual = await sut.Register(fakeRequest);
            Assert.IsInstanceOfType(actual, typeof(CreatedResult));
        }
    }
}
