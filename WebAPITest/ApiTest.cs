using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catalog.Reposotories;
using Catalog.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Catalog.Controllers;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Hosting;
using FluentAssertions;
using System;
using Microsoft.Extensions.Logging;
// Arrange

// Act

// Assert
namespace WebAPITest
{
    [TestClass]
    public class ApiTest
    {
        private readonly GameController _sut;
        private readonly Mock<IGameRepository> _gameRepoMock = new Mock<IGameRepository>();
        private readonly Mock<ILogger<GameController>> _loggerMock = new Mock<ILogger<GameController>>();
        private readonly Random rand = new();

        public ApiTest()
        {
            _sut = new GameController(_gameRepoMock.Object, _loggerMock.Object);
        }

        [TestMethod]
        public async Task GetGame_WithUnexistingItem_ShouldReturnNotFound()
        {
            // Arrange
            _gameRepoMock.Setup(x => x.GetGame(It.IsAny<Guid>()))
                .ReturnsAsync((Game)null);

            // Act
            var result = await _sut.GetGame(It.IsAny<Guid>());

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }
        [TestMethod]
        public async Task GetGame_WithExistingItem_ShouldReturnExpectedItem()
        {
            // Arrange
            Game expectedGame = CreateRandomGame();

            _gameRepoMock.Setup(x => x.GetGame(It.IsAny<Guid>())).ReturnsAsync(expectedGame);

            // Act
            var result = await _sut.GetGame(Guid.NewGuid());

            // Assert
            result.Value.Should().BeEquivalentTo(expectedGame);

        }
        [TestMethod]
        public async Task GetGames_WithExistingItem_ShouldReturnAllItems()
        {
            var expectedItems = new[] { CreateRandomGame(), CreateRandomGame(), CreateRandomGame(), CreateRandomGame() };
            throw new NotImplementedException();
            var test = 1;
        }
        [TestMethod]
        public async Task GetItems_WithMatchingItems_ShouldReturnMatchingItems()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public async Task CreateGame_WithgameToCreate_ShouldReturnCreatedGame()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public async Task Updategame_WithExistingGame_ShouldReturnNoContent()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public async Task DeleteGames_WithExistingGame_ShouldReturnNoContent()
        {
            var asd = 1;
            throw new NotImplementedException();
        }


        private Game CreateRandomGame()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
                Grade = rand.Next(1, 10),
                Image = Guid.NewGuid().ToString(),
            };
        }
    }
}
