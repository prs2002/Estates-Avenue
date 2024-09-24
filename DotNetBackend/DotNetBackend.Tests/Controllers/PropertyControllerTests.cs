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
    public class PropertyControllerTests
    {
        private PropertyController _controller;
        private Mock<IPropertyService> _mockService;

        [SetUp]
        public void SetUp()
        {
            _mockService = new Mock<IPropertyService>();
            _controller = new PropertyController(_mockService.Object);
        }

        [Test]
        public async Task GetAllProperties_ShouldReturnOkResult_WithProperties()
        {
            // Arrange
            var properties = new List<Property>
            {
                new Property { Pid = 1, Location = "Location 1" },
                new Property { Pid = 2, Location = "Location 2" }
            };
            _mockService.Setup(service => service.GetAllPropertiesAsync()).ReturnsAsync(properties);

            // Act
            var result = await _controller.GetAllProperties();

            // Assert
            var okResult = result as OkObjectResult;
            ClassicAssert.IsNotNull(okResult);
            ClassicAssert.AreEqual(200, okResult.StatusCode);
            ClassicAssert.AreEqual(properties, okResult.Value);
        }

        [Test]
        public async Task GetPropertyById_ShouldReturnNotFound_WhenPropertyDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetPropertyByIdAsync(1)).ReturnsAsync((Property)null);

            // Act
            var result = await _controller.GetPropertyById(1);

            // Assert
            ClassicAssert.IsInstanceOf<NotFoundResult>(result);
        }

        // More tests can be added for Create, Update, and Delete methods
    }
}
