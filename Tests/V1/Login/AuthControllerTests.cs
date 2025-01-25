using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Api.Controllers;
using Core.V1.Login.Models;
using Core.V1.Login.Interfaces.Services;

namespace Tests
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly AuthController _authController;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _authController = new AuthController(_authServiceMock.Object);
        }

        [Fact]
        public void Login_ShouldReturnOk_WhenCredentialsAreValid()
        {
            // Arrange
            var loginModel = new Login
            {
                Username = "admin",
                Password = "password"
            };
            var expectedToken = "fake-jwt-token";

            _authServiceMock
                .Setup(service => service.Authenticate(loginModel.Username, loginModel.Password))
                .Returns(expectedToken);

            // Act
            var result = _authController.Login(loginModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResponse = okResult.Value;

            Assert.NotNull(actualResponse);

            // Accessing the Token property directly
            var tokenProperty = actualResponse.GetType().GetProperty("Token");
            Assert.NotNull(tokenProperty);
            var tokenValue = tokenProperty.GetValue(actualResponse);
            Assert.Equal(expectedToken, tokenValue);
        }

        [Fact]
        public void Login_ShouldReturnUnauthorized_WhenCredentialsAreInvalid()
        {
            // Arrange
            var loginModel = new Login
            {
                Username = "invalid",
                Password = "wrongpassword"
            };

            _authServiceMock
                .Setup(service => service.Authenticate(loginModel.Username, loginModel.Password))
                .Returns((string)null);

            // Act
            var result = _authController.Login(loginModel);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}
