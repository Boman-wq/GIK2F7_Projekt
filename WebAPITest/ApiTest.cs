using Autofac.Extras.Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjektWebApi;
using ProjektWebApi.Repositories;
using ProjektWebApi.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProjektWebApi.Controllers;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Hosting;
using FluentAssertions;
using System;
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
        private readonly Mock<IWebHostEnvironment> _webMock = new Mock<IWebHostEnvironment>();
        private readonly Random rand = new();

        public ApiTest()
        {
            _sut = new GameController(_gameRepoMock.Object, _webMock.Object);
        }

        [TestMethod]
        public async Task GetGame_WithUnexistingItem_ShouldReturnNotFound()
        {
            // Arrange
            _gameRepoMock.Setup(x => x.Get(It.IsAny<int>()))
                .ReturnsAsync((Game)null);

            // Act
            var result = await _sut.GetGame(rand.Next());

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();

        }

        [TestMethod]
        public async Task GetAllGames_ShouldReturnAllGames()
        {
            var gameList = new List<Game>();
            gameList.Add(new Game { Id = 1, Name = "game1", Description = "Short1", Grade = 1, Image = "N/A1"});
            gameList.Add(new Game { Id = 2, Name = "game2", Description = "Short2", Grade = 2, Image = "N/A2" });

            _gameRepoMock.Setup(x => x.GetAll()).ReturnsAsync(gameList);

            var games = await _sut.Get();

            Assert.AreEqual(gameList, games);
        }

        [TestMethod]
        public async Task GetGameById_ShouldReturnGame()
        {
            // Arrange
            var gameId = 1;
            var gameName = "Game";
            var gameDto = new Game
            {
                Id = gameId,
                Name = gameName,
                Description = "Short",
                Grade = 2,
                Image = "N/A"

            };
            _gameRepoMock.Setup(x => x.Get(gameId)).ReturnsAsync(gameDto);

            // Act
            var game = await _sut.GetGame(gameId);

            // Assert
            //Assert.AreEqual(gameId, game.Id);
            //Assert.AreEqual(gameName, game.Name);
        }
        [TestMethod]
        public async Task GetById_ShouldReturnNothing_WhenGameDoesNotExist()
        {
            Random random = new Random();

            _gameRepoMock.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(() => null);

            var game = await _sut.GetGame(random.Next());

            Assert.IsNull(game);
        }
    }
}
