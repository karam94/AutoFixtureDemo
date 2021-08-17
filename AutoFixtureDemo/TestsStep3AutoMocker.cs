using AutoFixture.Xunit2;
using Moq;
using Moq.AutoMock;
using Shouldly;
using Xunit;

namespace AutoFixtureDemo
{
    public class TestsStep3AutoMocker
    {
        private readonly AutoMocker _mocker;
        private readonly SimpsonService _sut;

        public TestsStep3AutoMocker()
        {
            _mocker = new AutoMocker();
            _sut = _mocker.CreateInstance<SimpsonService>();
        }

        [Theory, AutoData]
        public async void GivenSimpsonService_WhenGettingById_ThenReturnsCorrectCharacter(
            int characterId,
            Character expected)
        {
            // Arrange
            var mockCharacterRepository = _mocker.GetMock<ICharacterRepository>();
            mockCharacterRepository.Setup(cr => cr.GetByIdAsync(characterId)).ReturnsAsync(expected);

            // Act
            var result = await _sut.GetCharacterByIdAsync(characterId);

            // Assert
            result.ShouldBe(expected);
        }

        [Theory, AutoData]
        public async void GivenSimpsonService_WhenGettingById_ThenLogsCorrectInfo(
            int characterId)
        {
            // Arrange
            var mockLogger = _mocker.GetMock<ILogger>();

            // Act 
            await _sut.GetCharacterByIdAsync(characterId);

            // Assert
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
            var mockCharacterRepository = _mocker.GetMock<ICharacterRepository>();
            var mockEmailService = _mocker.GetMock<IEmailService>();

            mockCharacterRepository.Setup(cr => cr.GetByIdAsync(characterId)).ReturnsAsync(expected);

            // Act
            await _sut.EmailCharacterByIdAsync(characterId, subject, message);

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
            var mockCharacterRepository = _mocker.GetMock<ICharacterRepository>();
            var mockLogger = _mocker.GetMock<ILogger>();

            mockCharacterRepository
                .Setup(cr => cr.GetByIdAsync(characterId))
                .ReturnsAsync(expected);

            // Act
            await _sut.EmailCharacterByIdAsync(characterId, subject, message);

            // Assert
            mockLogger.Verify(l =>
                l.LogInformation($"Fetching character with id {characterId}"), Times.Once());
        }
    }
}