using System;
using Xunit;
using Moq;
using api.Services;
using System.Threading.Tasks;
using api.ViewModels.Response;
using api.Interfaces;
using api.Controllers;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using FluentAssertions;
using api.ViewModels;
using System.Collections.Generic;

namespace api.UnitTests
{
    public class CharactersControllerTests
    {
        private readonly Random rand = new();
        private int lastId = 0;

        [Fact]
        public async Task GetCharacterById_WithUnexistingItem_ReturnsNotFound()
        {
            // Arrange            
            var expectedResult = Result.FailureResult("Personaje inexistente.");
            var serviceStub = new Mock<ICharacterService>();
            serviceStub.Setup(service => service.GetCharacterById(It.IsAny<int>()))
                .ReturnsAsync(expectedResult);

            var controller = new charactersController(serviceStub.Object);

            // Act
            IActionResult result = await controller.GetCharacterById(0);
            var objectResult = result as NotFoundObjectResult;
            Result r = objectResult.Value as Result;

            // Assert            
            r.Should().BeEquivalentTo(expectedResult);            
        }

        [Fact]
        public async Task GetCharacterById_WithExistingItem_ReturnsExpectedItem()
        {
            // Arrange            
            var character = this.CreateRandomCharacter();
            var expectedResult = Result<Character>.SuccessResult(character);
            
            var serviceStub = new Mock<ICharacterService>();
            serviceStub.Setup(service => service.GetCharacterById(It.IsAny<int>()))
                .ReturnsAsync(expectedResult);

            var controller = new charactersController(serviceStub.Object);

            // Act
            IActionResult result = await controller.GetCharacterById(1);

            // We cast it to the expected response type
            var objectResult = result as OkObjectResult;
            Result<Character> r = objectResult.Value as Result<Character>;

            // Assert
            r.Should().BeEquivalentTo(
                expectedResult,
                options => options.ComparingByMembers<Character>());
        }

        // Listado de personajes sin filtro
        [Fact]
        public async Task GetCharacterList_WithExistingItems_ReturnsAllItems()
        {
            // Arrange
            var expectedItems = new[] { this.CreateCharacterListView(), this.CreateCharacterListView(), this.CreateCharacterListView(), this.CreateCharacterListView(), this.CreateCharacterListView() };
            var expectedResult = Result<IEnumerable<CharactersListView>>.SuccessResult(expectedItems);

            var serviceStub = new Mock<ICharacterService>();
            serviceStub.Setup(service => service.GetCharacterList(null, null, null))
                .ReturnsAsync(expectedResult);

            var controller = new charactersController(serviceStub.Object);

            // Act
            IActionResult result = await controller.GetCharacterList(null, null, null);

            // We cast it to the expected response type
            var objectResult = result as OkObjectResult;
            Result<IEnumerable<CharactersListView>> r = objectResult.Value as Result<IEnumerable<CharactersListView>>;

            // Assert
            r.Should().BeEquivalentTo(
                expectedResult,
                options => options.ComparingByMembers<CharactersListView>());
        }

        private Character CreateRandomCharacter()
        {            
            return new Character
            {
                CharacterId = ++this.lastId,
                Name = $"testCharacter{this.lastId}",
                Age = this.rand.Next(90) + 1,
                Weight = this.rand.Next(300),
                Image = $"image{this.lastId}.jpg"
            };
        }

        // Retorna instancia de viewModel a partir de un Character random
        private CharactersListView CreateCharacterListView()
        {
            return new CharactersListView(this.CreateRandomCharacter());
        }

        // [Fact]
        // public void UnitOfWork_StateUnderTest_ExpectedBehavior()
        // {

        // }
    }
}
