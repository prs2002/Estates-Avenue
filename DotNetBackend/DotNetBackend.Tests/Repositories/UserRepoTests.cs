using DotNetBackend.Models;
using DotNetBackend.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetBackend.Tests.Repositories
{
    [TestFixture]
    public class UserRepoTests
    {
        private Mock<IMongoCollection<User>> _mockCollection;
        private UserRepo _userRepo;

        [SetUp]
        public void Setup()
        {
            var mockDatabase = new Mock<IMongoDatabase>();
            var mockClient = new Mock<IMongoClient>();
            _mockCollection = new Mock<IMongoCollection<User>>();
            var settings = Options.Create(new REdbSettings { ConnectionString = "mongodb://localhost:27017", DatabaseName = "TestDB", UserCollection = "Users" });

            mockClient.Setup(c => c.GetDatabase(It.IsAny<string>(), null)).Returns(mockDatabase.Object);
            mockDatabase.Setup(db => db.GetCollection<User>(It.IsAny<string>(), null)).Returns(_mockCollection.Object);

            _userRepo = new UserRepo(settings);
        }

        [Test]
        public async Task GetAllUsersAsync_ShouldReturnAllUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = "1", Name = "John Doe",Email = "doe@example.com", UserType = "customer", Location = "Location1", Number = "1234567890", Password = "hashed_password1" },
                new User { Id = "2", Name = "Jane Smith",Email = "smith@example.com", UserType = "customer", Location = "Location2", Number = "0987654321", Password = "hashed_password2" }
            };

            // Set up the mock to return the users when Find is called
            var findFluentMock = new Mock<IFindFluent<User, User>>();
            findFluentMock.Setup(_ => _.ToListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(users); // Specify the cancellation token

            _mockCollection.Setup(c => c.Find(It.IsAny<FilterDefinition<User>>(), null)).Returns(findFluentMock.Object);

            // Act
            var result = await _userRepo.GetAllUsersAsync();

            // Assert
            ClassicAssert.AreEqual(2, result.Count);
            ClassicAssert.AreEqual("John Doe", result[0].Name);
            ClassicAssert.AreEqual("Jane Smith", result[1].Name);
        }

        // Additional tests for CreateUserAsync, UpdateUserAsync, DeleteUserAsync, etc.
    }
}
