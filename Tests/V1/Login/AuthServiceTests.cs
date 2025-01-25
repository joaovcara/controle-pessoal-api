using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using Core.V1.Login.Services;
using Core.V1.Login.Interfaces.Services;

namespace Tests
{
    public class AuthServiceTests
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly IAuthService _authService;

        public AuthServiceTests()
        {
            _configurationMock = new Mock<IConfiguration>();

            // Mock para JwtSettings
            var jwtSection = new Mock<IConfigurationSection>();
            jwtSection.Setup(x => x["SecretKey"]).Returns("supersecretkey@12345678901234567890"); // 32 caracteres
            jwtSection.Setup(x => x["Issuer"]).Returns("TestIssuer");
            jwtSection.Setup(x => x["Audience"]).Returns("TestAudience");
            jwtSection.Setup(x => x["ExpirationInMinutes"]).Returns("60");

            _configurationMock.Setup(x => x.GetSection("JwtSettings")).Returns(jwtSection.Object);


            _authService = new AuthService(_configurationMock.Object);
        }

        [Fact]
        public void Authenticate_ShouldReturnToken_WhenCredentialsAreValid()
        {
            // Arrange
            var username = "admin";
            var password = "password";

            // Act
            var token = _authService.Authenticate(username, password);

            // Assert
            Assert.NotNull(token);
        }

        [Fact]
        public void Authenticate_ShouldReturnNull_WhenCredentialsAreInvalid()
        {
            // Arrange
            var username = "invalidUser";
            var password = "wrongpassword";

            // Act
            var token = _authService.Authenticate(username, password);

            // Assert
            Assert.Null(token);
        }
    }
}
