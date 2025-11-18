using Moq;
using AIssist.Domain.Entities;
using AutoMapper;
using AIssist.Application.Services;
using AIssist.Application.Services.Interfaces;
using AIssist.Domain.Http.Request.User;
using AIssist.Domain.Http.Response.Users;
using AIssist.Domain.Services.Interfaces;

namespace AIssist.Tests.ApplicationServices
{
    public class UserAppServiceTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<ILogAppService> _logAppServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UserAppService _service;

        public UserAppServiceTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _logAppServiceMock = new Mock<ILogAppService>();
            _mapperMock = new Mock<IMapper>();

            _service = new UserAppService(
                _userServiceMock.Object,
                _mapperMock.Object,
                _logAppServiceMock.Object
            );
        }

        [Fact]
        public async Task Add_Should_ReturnSuccess_When_UserAndLogCreated()
        {
            var request = new UserPostRequest { Name = "John", Password = "123" };
            var user = new Users { Id = 1, Name = "John" };

            _mapperMock.Setup(m => m.Map<Users>(request)).Returns(user);

            _userServiceMock
                .Setup(s => s.Add(It.IsAny<Users>()))
                .ReturnsAsync(true);

            _logAppServiceMock
                .Setup(s => s.Add("Inserção", It.IsAny<string>()))
                .Returns(Task.CompletedTask);


            var result = await _service.Add(request);

            Assert.True(result.Success);
            Assert.Null(result.Message);

            _logAppServiceMock.Verify(s => s.Add("Inserção", It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Add_Should_ReturnError_When_UserFails()
        {
            var request = new UserPostRequest { Name = "John", Password = "123" };

            _mapperMock.Setup(m => m.Map<Users>(request)).Returns(new Users());

            _userServiceMock.Setup(s => s.Add(It.IsAny<Users>())).ReturnsAsync(false);

            var result = await _service.Add(request);

            Assert.False(result.Success);
            Assert.Equal("Falha ao salvar registro.", result.Message);

            _logAppServiceMock.Verify(s => s.Add(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task Inactivate_Should_ReturnSuccess_When_UserAndLogOk()
        {
            long id = 10;

            _userServiceMock.Setup(s => s.Inactivate(id)).ReturnsAsync(true);
            _logAppServiceMock.Setup(s =>
                s.Add("Inativação", It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var result = await _service.Inactivate(id);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task Inactivate_Should_ReturnError_When_UserFails()
        {
            long id = 10;

            _userServiceMock.Setup(s => s.Inactivate(id)).ReturnsAsync(false);

            var result = await _service.Inactivate(id);

            Assert.False(result.Success);
            Assert.Equal("Falha ao salvar registro.", result.Message);
        }

        [Fact]
        public async Task Get_Should_ReturnMappedUsers()
        {
            var users = new List<Users> {
            new Users { Id = 1, Name = "A" },
            new Users { Id = 2, Name = "B" }
        };

            var mapped = new List<UserResponse> {
            new UserResponse { Id = 1, Name = "A" },
            new UserResponse { Id = 2, Name = "B" }
        };

            _userServiceMock.Setup(s => s.Get()).ReturnsAsync(users);
            _mapperMock.Setup(m => m.Map<List<UserResponse>>(users)).Returns(mapped);

            var result = await _service.Get();

            Assert.Equal(2, result.Count);
            Assert.Equal("A", result[0].Name);
        }

        [Fact]
        public async Task GetById_Should_ReturnUser()
        {
            long id = 5;
            var user = new Users { Id = id, Name = "Test" };

            _userServiceMock.Setup(s => s.GetById(id)).ReturnsAsync(user);

            var result = await _service.GetById(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task Update_Should_ReturnSuccess_When_Ok()
        {
            var request = new UserPutRequest { Id = 1, Name = "Updated" };
            var user = new Users { Id = 1, Name = "OldName" };

            _userServiceMock.Setup(s => s.GetById(1)).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map(request, user));

            _userServiceMock.Setup(s => s.Update(user)).ReturnsAsync(true);
            _logAppServiceMock.Setup(s =>
                s.Add("Atualização", It.IsAny<string>()))
                .Returns(Task.CompletedTask);


            var result = await _service.Update(request);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task Update_Should_ReturnError_When_UpdateFails()
        {
            var request = new UserPutRequest { Id = 1, Name = "Updated" };
            var user = new Users { Id = 1 };

            _userServiceMock.Setup(s => s.GetById(1)).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map(request, user));

            _userServiceMock.Setup(s => s.Update(user)).ReturnsAsync(false);

            var result = await _service.Update(request);

            Assert.False(result.Success);
            Assert.Equal("Falha ao atualizar registro.", result.Message);
        }
    }

}

