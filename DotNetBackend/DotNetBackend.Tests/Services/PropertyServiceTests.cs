using DotNetBackend.Models;
using DotNetBackend.Repositories;
using DotNetBackend.Services;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetBackend.Tests.Services
{
    [TestFixture]
    public class PropertyServiceTests
    {
        private PropertyService _propertyService;
        private Mock<IPropertyRepo> _mockRepo;

        [SetUp]
        public void SetUp()
        {
            _mockRepo = new Mock<IPropertyRepo>();
            _propertyService = new PropertyService(_mockRepo.Object);
        }

        [Test]
        public async Task GetAllPropertiesAsync_ShouldReturnListOfProperties()
        {
            // Arrange
            var properties = new List<Property>
            {
                new Property { Pid = 1, Location = "Location 1" },
                new Property { Pid = 2, Location = "Location 2" }
            };
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(properties);

            // Act
            var result = await _propertyService.GetAllPropertiesAsync();

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(2, result.Count);
        }

        [Test]
        public async Task GetPropertyByIdAsync_ShouldReturnProperty_WhenExists()
        {
            // Arrange
            var property = new Property { Pid = 1, Location = "Location 1" };
            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(property);

            // Act
            var result = await _propertyService.GetPropertyByIdAsync(1);

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual("Location 1", result.Location);
        }

        [Test]
        public async Task CreatePropertyAsync_ShouldReturnCreatedProperty()
        {
            // Arrange
            var property = new Property { Location = "New Location" };
            _mockRepo.Setup(repo => repo.CreateAsync(property)).ReturnsAsync(property);

            // Act
            var result = await _propertyService.CreatePropertyAsync(property);

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual("New Location", result.Location);
        }

        // More tests can be added for Update and Delete methods
    }
}
