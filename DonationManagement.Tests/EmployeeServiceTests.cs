using DonationManagement.Api.DTOs;
using DonationManagement.Api.Services.Implementations;
using DonationManagement.Api.Services.Interfaces;
using DonationManagement.Core.Entities;
using DonationManagement.Core.Repositories.Interfaces;
using Moq;
using FluentAssertions;
using BCrypt.Net;

namespace DonationManagement.Tests
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepoMock;
        private readonly Mock<IDonorRepository> _donorRepoMock;
        private readonly Mock<ICategoryRepository> _categoryRepoMock;
        private readonly Mock<ICaseRepository> _caseRepoMock;
        private readonly Mock<IDistributionRepository> _distributionRepoMock;
        private readonly Mock<IJwtTokenService> _jwtTokenServiceMock;
        private readonly EmployeeService _service;

        public EmployeeServiceTests()
        {
            _employeeRepoMock = new Mock<IEmployeeRepository>();
            _donorRepoMock = new Mock<IDonorRepository>();
            _categoryRepoMock = new Mock<ICategoryRepository>();
            _caseRepoMock = new Mock<ICaseRepository>();
            _distributionRepoMock = new Mock<IDistributionRepository>();
            _jwtTokenServiceMock = new Mock<IJwtTokenService>();

            _service = new EmployeeService(
                _employeeRepoMock.Object,
                _donorRepoMock.Object,
                _categoryRepoMock.Object,
                _caseRepoMock.Object,
                _distributionRepoMock.Object,
                _jwtTokenServiceMock.Object);
        }

        [Fact]
        public async Task LoginAsync_WithValidCredentials_ReturnsAuthResponseWithToken()
        {
            // Arrange
            var password = "password123";
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            var employee = new Employee 
            { 
                Id = 1, 
                Username = "testuser", 
                Password = hashedPassword, 
                Name = "Test User", 
                Role = "Admin" 
            };
            
            _employeeRepoMock.Setup(r => r.FindAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Employee, bool>>>()))
                .ReturnsAsync(new List<Employee> { employee });

            _jwtTokenServiceMock.Setup(s => s.GenerateToken(employee.Username, employee.Role))
                .Returns("fake-jwt-token");

            var request = new LoginRequest("testuser", password);

            // Act
            var result = await _service.LoginAsync(request);

            // Assert
            result.Should().NotBeNull();
            result!.Username.Should().Be(employee.Username);
            result.Token.Should().Be("fake-jwt-token");
        }

        [Fact]
        public async Task LoginAsync_WithInvalidPassword_ReturnsNull()
        {
            // Arrange
            var password = "password123";
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            var employee = new Employee { Username = "testuser", Password = hashedPassword };
            
            _employeeRepoMock.Setup(r => r.FindAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Employee, bool>>>()))
                .ReturnsAsync(new List<Employee> { employee });

            var request = new LoginRequest("testuser", "wrongpassword");

            // Act
            var result = await _service.LoginAsync(request);

            // Assert
            result.Should().BeNull();
        }
    }
}
