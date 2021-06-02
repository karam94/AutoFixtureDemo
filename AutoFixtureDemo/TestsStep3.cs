using AutoFixture.Xunit2;
using Moq;
using Xunit;

namespace AutoFixtureDemo
{
    public class TestsStep3
    {
        [Theory, AutoMoqData]
        public async void GivenSimpsonService_WhenGettingById_ThenReturnsCorrectCharacter(
             int characterId,
             Character expected,
             [Frozen] Mock<ICharacterRepository> mockCharacterRepository,
             SimpsonService sut)
        {
            // Arrange
            mockCharacterRepository.Setup(cr => cr.GetByIdAsync(characterId)).ReturnsAsync(expected);

            // Act
            var result = await sut.GetCharacterByIdAsync(characterId);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}