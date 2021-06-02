using Moq;
using Xunit;

namespace AutoFixtureDemo
{
    public class Tests
    {
        [Fact]
        public async void GivenSimpsonService_WhenGettingById_ThenReturnsCorrectCharacter()
        {
            // Arrange
            var characterId = 1;
            var homer = new Character {Name = "Homer", Email = "homer@compuglobal.com"};

            var mockLogger = new Mock<ILogger>();

            var mockCharacterRepository = new Mock<ICharacterRepository>();
            mockCharacterRepository.Setup(cr => cr.GetByIdAsync(characterId)).ReturnsAsync(homer);

            var mockEmailService = new Mock<IEmailService>();

            var sut = new SimpsonService(
                mockLogger.Object, mockCharacterRepository.Object, mockEmailService.Object);

            // Act
            var result = await sut.GetCharacterByIdAsync(characterId);

            // Assert
            Assert.Equal(homer, result);
        }

        [Fact]
        public async void GivenSimpsonService_WhenGettingById_ThenLogsCorrectInfo()
        {
            // Arrange
            var characterId = 1;

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

        [Fact]
        public async void GivenSimpsonService_WhenEmailingById_ThenEmailsCharacter()
        {
            // Arrange
            var characterId = 1;
            var subject = "Hello Springfield";
            var message = "I hate Mr. Burns!";
            var homer = new Character {Name = "Homer", Email = "homer@compuglobal.com"};

            var mockLogger = new Mock<ILogger>();

            var mockCharacterRepository = new Mock<ICharacterRepository>();
            mockCharacterRepository.Setup(cr => cr.GetByIdAsync(characterId)).ReturnsAsync(homer);

            var mockEmailService = new Mock<IEmailService>();

            var sut = new SimpsonService(
                mockLogger.Object, mockCharacterRepository.Object, mockEmailService.Object);

            // Act
            await sut.EmailCharacterByIdAsync(characterId, subject, message);

            // Assert
            mockEmailService.Verify(m => m.SendEmailAsync(homer.Email, subject, message), Times.Once);
        }

        [Fact]
        public async void GivenSimpsonService_WhenEmailingById_ThenLogsCorrectInfo()
        {
            // Arrange
            var characterId = 1;
            var subject = "Hello Springfield";
            var message = "I hate Mr. Burns!";
            var homer = new Character {Name = "Homer", Email = "homer@compuglobal.com"};

            var mockLogger = new Mock<ILogger>();

            var mockCharacterRepository = new Mock<ICharacterRepository>();
            mockCharacterRepository.Setup(cr => cr.GetByIdAsync(characterId)).ReturnsAsync(homer);

            var mockEmailService = new Mock<IEmailService>();

            var sut = new SimpsonService(
                mockLogger.Object, mockCharacterRepository.Object, mockEmailService.Object);

            // Act & Assert
            await sut.EmailCharacterByIdAsync(characterId, subject, message);
            mockLogger.Verify(l =>
                l.LogInformation($"Fetching character with id {characterId}"), Times.Once());
        }
    }
}