using AutoFixture.Xunit2;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace AutoFixtureDemo
{
    public class TestsStep2AutoMocker
    {
        [Theory, AutoData]
        public async void GivenSimpsonService_WhenGettingById_ThenReturnsCorrectCharacter(
            int characterId,
            Character expected)
        {
            // Arrange
            var mocker = new AutoMocker();
            mocker.GetMock<ICharacterRepository>()
                .Setup(cr => cr.GetByIdAsync(characterId))
                .ReturnsAsync(expected);
            var sut = mocker.CreateInstance<SimpsonService>();
            
            // Act
            var result = await sut.GetCharacterByIdAsync(characterId);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}