using DotNetBackend.Controllers;
using DotNetBackend.Models;
using DotNetBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetBackend.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        private Mock<IUserService> _mockUserService;
        private UserController _userController;

        [SetUp]
        public void Setup()
        {
            _mockUserService = new Mock<IUserService>();
            _userController = new UserController(_mockUserService.Object);
        }

        [Test]
        public async Task GetAllUsers_ShouldReturnOkResult_WithUserList()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = "1", Name = "John Doe", Email = "doe@example.com", UserType = "customer",Location = "Location1", Number = "1234567890", Password = "hashed_password1" },
                new User { Id = "2", Name = "Jane Smith", Email = "smith@example.com", UserType = "customer",Location = "Location2", Number = "0987654321", Password = "hashed_password2" }
            };
            _mockUserService.Setup(s => s.GetAllAsync()).ReturnsAsync(users);

            // Act
            var result = await _userController.GetAllUsers();

            // Assert
            var okResult = result as OkObjectResult;
            ClassicAssert.IsNotNull(okResult);
            ClassicAssert.AreEqual(200, okResult.StatusCode);
            ClassicAssert.AreEqual(users, okResult.Value);
        }

        // Additional tests for CreateUser, GetUserById, DeleteUser, UpdateUser, etc.
    }
}