using AutoFixture.Xunit2;
using Moq;
using Xunit;

namespace AutoFixtureDemo
{
    public class TestsStep1
    {
        [Theory, AutoData]
        public async void GivenSimpsonService_WhenGettingById_ThenReturnsCorrectCharacter(
            int characterId,
            Character expected)
        {
            // Arrange
            var mockLogger = new Mock<ILogger>();

            var mockCharacterRepository = new Mock<ICharacterRepository>();
            mockCharacterRepository.Setup(cr => cr.GetByIdAsync(characterId)).ReturnsAsync(expected);

            var mockEmailService = new Mock<IEmailService>();

            var sut = new SimpsonService(
                mockLogger.Object, mockCharacterRepository.Object, mockEmailService.Object);

            // Act
            var result = await sut.GetCharacterByIdAsync(characterId);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory, AutoData]
        public async void GivenSimpsonService_WhenGettingById_ThenLogsCorrectInfo(int characterId)
        {
            // Arrange
            var mockLogger = new Mock<ILogger>();
            var mockCharacterRepository = new Mock<ICharacterRepository>();
            var mockEmailService = new Mock<IEmailService>();

            var sut = new SimpsonService(
                mockLogger.Object, mockCharacterRepository.Object, mockEmailService.Object);

            // Act & Assert
            await sut.GetCharacterByIdAsync(characterId);
            mockLogger.Verify(l =>
                l.LogInformation($"Fetching character with id {characterId}"), Times.Once());
        }

        [Theory, AutoData]
        public async void GivenSimpsonService_WhenEmailingById_ThenEmailsCharacter(
            int characterId, string subject, string message, Character expected)
        {
            // Arrange
            var mockLogger = new Mock<ILogger>();

            var mockCharacterRepository = new Mock<ICharacterRepository>();
            mockCharacterRepository.Setup(cr => cr.GetByIdAsync(characterId)).ReturnsAsync(expected);

            var mockEmailService = new Mock<IEmailService>();

            var sut = new SimpsonService(
                mockLogger.Object, mockCharacterRepository.Object, mockEmailService.Object);

            // Act
            await sut.EmailCharacterByIdAsync(characterId, subject, message);

            // Assert
            mockEmailService.Verify(m => m.SendEmailAsync(expected.Email, subject, message), Times.Once);
        }

        [Theory, AutoData]
        public async void GivenSimpsonService_WhenEmailingById_ThenLogsCorrectInfo(
            int characterId, string subject, string message, Character expected)
        {
            // Arrange
            var mockLogger = new Mock<ILogger>();

            var mockCharacterRepository = new Mock<ICharacterRepository>();
            mockCharacterRepository
                .Setup(cr => cr.GetByIdAsync(characterId))
                .ReturnsAsync(expected);

            var mockEmailService = new Mock<IEmailService>();

            var sut = new SimpsonService(
                mockLogger.Object, mockCharacterRepository.Object, mockEmailService.Object);

            // Act
            await sut.EmailCharacterByIdAsync(characterId, subject, message);
            
            // Assert
            mockLogger.Verify(l =>
                l.LogInformation($"Fetching character with id {characterId}"), Times.Once());
        }
    }
}