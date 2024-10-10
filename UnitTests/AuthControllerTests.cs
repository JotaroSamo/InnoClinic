using Xunit;
using Moq;
using Auth_API.Domain.Abstract.Service;
using Microsoft.Extensions.Options;
using Auth_API.Infrastructure;
using Microsoft.Extensions.Logging;
using Auth_API.Controllers;
using MassTransit;
using Auth_API.Contract;
using CSharpFunctionalExtensions;
using Auth_API.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Global.Dto;


namespace UnitTests
{
    public class AuthControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IOptions<JwtOptions>> _mockJwtOptions;
        private readonly Mock<ILogger<AuthController>> _mockLogger;
        private readonly Mock<IBus> _mockBus;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _mockJwtOptions = new Mock<IOptions<JwtOptions>>();
            _mockLogger = new Mock<ILogger<AuthController>>();
            _mockBus = new Mock<IBus>();

            // Задаем значение JwtOptions, если оно используется в тестируемых методах
            _mockJwtOptions.Setup(j => j.Value).Returns(new JwtOptions());

            _controller = new AuthController(_mockUserService.Object, _mockJwtOptions.Object, _mockLogger.Object, _mockBus.Object);
        }

        [Fact]
        public async Task Register_ReturnsOkResult_WhenRegistrationIsSuccessful()
        {
            // Arrange
            var userRequest = new UserRegisterRequest
            {
                Email = "test@example.com",
                Password = "password"
            };

            var userResult = Result.Success(new User { Id = Guid.NewGuid(), Email = userRequest.Email, HashPassword = "hashedPassword" });

            _mockUserService.Setup(us => us.RegisterAsync(userRequest.Email, userRequest.Password))
                            .ReturnsAsync(userResult);

            // Act
            var result = await _controller.Register(userRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(userResult.Value, okResult.Value);

            // Проверяем, что вызов _bus.Publish() был выполнен
            _mockBus.Verify(b => b.Publish(It.Is<AccountDto>(dto => dto.Email == userRequest.Email), default), Times.Once);
        }

        [Fact]
        public async Task Register_ReturnsBadRequest_WhenRegistrationFails()
        {
            // Arrange
            var userRequest = new UserRegisterRequest
            {
                Email = "test@example.com",
                Password = "password"
            };

            var errorMessage = "Registration error";
            var userResult = Result.Failure<User>(errorMessage);

            _mockUserService.Setup(us => us.RegisterAsync(userRequest.Email, userRequest.Password))
                            .ReturnsAsync(userResult);

            // Act
            var result = await _controller.Register(userRequest);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(errorMessage, badRequestResult.Value);

            // Проверяем, что _bus.Publish() не был вызван при ошибке
            _mockBus.Verify(b => b.Publish(It.IsAny<AccountDto>(), default), Times.Never);
        }
    }
}
