using DotNetBackend.Models;
using DotNetBackend.Repositories;
using DotNetBackend.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetBackend.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepo> _mockUserRepo;
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            _mockUserRepo = new Mock<IUserRepo>();
            _userService = new UserService(_mockUserRepo.Object, null); // Assuming IConfiguration is not needed for these tests
        }

        [Test]
        public async Task CreateAsync_ShouldCallRepoCreateUserAsync()
        {
            // Arrange
            var user = new User { Id = "1", Name = "John Doe", Email = "doe@example.com", UserType = "customer", Location = "Location1", Number = "1234567890", Password = "hashed_password1" };

            // Act
            await _userService.CreateAsync(user);

            // Assert
            _mockUserRepo.Verify(repo => repo.CreateUserAsync(It.Is<User>(u => u.Name == "John Doe")), Times.Once);
        }

        // Additional tests for GetAllAsync, GetByIdAsync, UpdateUserAsync, DeleteUserAsync, etc.
    }
}
