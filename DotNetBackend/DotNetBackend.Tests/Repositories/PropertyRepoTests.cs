using DotNetBackend.Models;
using DotNetBackend.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetBackend.Tests.Repositories
{
    [TestFixture]
    public class PropertyRepoTests
    {
        private PropertyRepo _propertyRepo;
        private DbContextOptions<AppDbContext> _dbContextOptions;

        [SetUp]
        public void Setup()
        {
            // Use an in-memory database for testing
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new AppDbContext(_dbContextOptions))
            {
                // Ensure the database is created
                context.Database.EnsureCreated();
            }

            _propertyRepo = new PropertyRepo(new AppDbContext(_dbContextOptions));
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllProperties()
        {
            // Arrange
            var properties = new List<Property>
        {
            new Property { Pid = 2, Location = "Location 2" }
        };

            using (var context = new AppDbContext(_dbContextOptions))
            {
                context.Property.AddRange(properties);
                await context.SaveChangesAsync();
            }

            // Act
            var result = await _propertyRepo.GetAllAsync();

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(1, result.Count, "Expected 2 properties.");
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnProperty_WhenExists()
        {
            // Arrange
            var property = new Property { Pid = 3, Location = "Location 3" }; // Changed to unique Pid

            using (var context = new AppDbContext(_dbContextOptions))
            {
                context.Property.Add(property);
                await context.SaveChangesAsync();
            }

            // Act
            var result = await _propertyRepo.GetByIdAsync(3); // Match with unique Pid

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual("Location 3", result.Location, "Expected location to be 'Location 3'.");
        }
    

    [Test]
        public async Task CreateAsync_ShouldAddProperty()
        {
            // Arrange
            var property = new Property { Location = "New Location" };

            // Act
            var result = await _propertyRepo.CreateAsync(property);

            // Assert
            using (var context = new AppDbContext(_dbContextOptions))
            {
                var createdProperty = await context.Property.FindAsync(property.Pid);
                ClassicAssert.IsNotNull(createdProperty);
                ClassicAssert.AreEqual("New Location", createdProperty.Location);
            }
        }

        // More tests can be added for Update and Delete methods
    }
}
