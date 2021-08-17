using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Moq;
using Xunit;

namespace AutoFixtureDemo
{
    public class TestsStep2
    {
        [Theory, AutoData]
        public async void GivenSimpsonService_WhenGettingById_ThenReturnsCorrectCharacter(
            int characterId,
            Character expected)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization {ConfigureMembers = true});
            fixture.Freeze<Mock<ICharacterRepository>>()
                .Setup(cr => cr.GetByIdAsync(characterId))
                .ReturnsAsync(expected);
            var sut = fixture.Create<SimpsonService>();

            // Act
            var result = await sut.GetCharacterByIdAsync(characterId);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory, AutoData]
        public async void GivenSimpsonService_WhenGettingById_ThenLogsCorrectInfo(
            int characterId)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization {ConfigureMembers = true});
            var mockLogger = fixture.Freeze<Mock<ILogger>>();
            var sut = fixture.Create<SimpsonService>();

            // Act & Assert
            await sut.GetCharacterByIdAsync(characterId);
            mockLogger.Verify(l =>
                l.LogInformation($"Fetching character with id {characterId}"), Times.Once());
        }

        [Theory, AutoData]
        public async void GivenSimpsonService_WhenEmailingById_ThenEmailsCharacter(
            int characterId,
            string subject,
            string message,
            Character expected)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization {ConfigureMembers = true});
            fixture.Freeze<Mock<ICharacterRepository>>()
                .Setup(cr => cr.GetByIdAsync(characterId))
                .ReturnsAsync(expected);
            var mockEmailService = fixture.Freeze<Mock<IEmailService>>();
            var sut = fixture.Create<SimpsonService>();

            // Act
            await sut.EmailCharacterByIdAsync(characterId, subject, message);

            // Assert
            mockEmailService.Verify(m => m.SendEmailAsync(expected.Email, subject, message), Times.Once);
        }

        [Theory, AutoData]
        public async void GivenSimpsonService_WhenEmailingById_ThenLogsCorrectInfo(
            int characterId,
            string subject,
            string message,
            Character expected)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization {ConfigureMembers = true});
            fixture.Freeze<Mock<ICharacterRepository>>()
                .Setup(cr => cr.GetByIdAsync(characterId))
                .ReturnsAsync(expected);
            var mockLogger = fixture.Freeze<Mock<ILogger>>();
            var sut = fixture.Create<SimpsonService>();

            // Act & Assert
            await sut.EmailCharacterByIdAsync(characterId, subject, message);
            mockLogger.Verify(l =>
                l.LogInformation($"Fetching character with id {characterId}"), Times.Once());
        }
    }
}