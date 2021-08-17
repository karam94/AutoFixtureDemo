using AutoFixture.Xunit2;
using Moq;
using Shouldly;
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
            result.ShouldBe(expected);
        }

        [Theory, AutoMoqData]
        public async void GivenSimpsonService_WhenGettingById_ThenLogsCorrectInfo(
            int characterId,
            [Frozen] Mock<ILogger> mockLogger,
            SimpsonService sut)
        {
            // Act 
            await sut.GetCharacterByIdAsync(characterId);

            // Assert
            mockLogger.Verify(l =>
                l.LogInformation($"Fetching character with id {characterId}"), Times.Once());
        }

        [Theory, AutoMoqData]
        public async void GivenSimpsonService_WhenEmailingById_ThenEmailsCharacter(
            int characterId,
            string subject,
            string message,
            Character expected,
            [Frozen] Mock<ICharacterRepository> mockCharacterRepository,
            [Frozen] Mock<IEmailService> mockEmailService,
            SimpsonService sut)
        {
            // Arrange
            mockCharacterRepository.Setup(cr => cr.GetByIdAsync(characterId)).ReturnsAsync(expected);

            // Act
            await sut.EmailCharacterByIdAsync(characterId, subject, message);

            // Assert
            mockEmailService.Verify(m => m.SendEmailAsync(expected.Email, subject, message), Times.Once);
        }

        [Theory, AutoMoqData]
        public async void GivenSimpsonService_WhenEmailingById_ThenLogsCorrectInfo(
            int characterId,
            string subject,
            string message,
            Character expected,
            [Frozen] Mock<ICharacterRepository> mockCharacterRepository,
            [Frozen] Mock<ILogger> mockLogger,
            SimpsonService sut)
        {
            // Arrange
            mockCharacterRepository
                .Setup(cr => cr.GetByIdAsync(characterId))
                .ReturnsAsync(expected);

            // Act
            await sut.EmailCharacterByIdAsync(characterId, subject, message);

            // Assert
            mockLogger.Verify(l =>
                l.LogInformation($"Fetching character with id {characterId}"), Times.Once());
        }
    }
}