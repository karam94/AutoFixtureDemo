// using AutoFixture;
// using AutoFixture.AutoMoq;
// using AutoFixture.Xunit2;
// using Moq;
// using Shouldly;
// using Xunit;
//
// namespace AutoFixtureDemo
// {
//     public class TestsOnSteroids
//     {
//         [Theory, AutoData]
//         public void GivenSimpsonService_WhenGettingById_ReturnsExpectedCharacter(
//             int characterId,
//             string expected)
//         {
//             // Arrange
//             var mockLogger = new Mock<ILogger>();
//
//             var mockCharacterRepository = new Mock<ICharacterRepository>();
//             mockCharacterRepository.Setup(cr => cr.GetById(characterId)).Returns(expected);
//
//             var mockEmailService = new Mock<IEmailService>();
//
//             var sut = new SimpsonService(
//                 mockLogger.Object, mockCharacterRepository.Object, mockEmailService.Object);
//
//             // Act
//             var result = sut.GetCharacterById(characterId);
//
//             // Assert
//             Assert.Equal(expected, result);
//         }
//
//         [Theory, AutoData]
//         public void GivenSimpsonService_WhenGettingById_ReturnsExpectedCharacter_2(
//             int characterId,
//             string expected)
//         {
//             // Arrange
//             var fixture = new Fixture();
//             fixture.Customize(new AutoMoqCustomization {ConfigureMembers = true});
//             fixture.Freeze<Mock<ICharacterRepository>>()
//                 .Setup(cr => cr.GetById(characterId))
//                 .Returns(expected);
//             var sut = fixture.Create<SimpsonService>();
//         
//             // Act
//             var result = sut.GetCharacterById(characterId);
//         
//             // Assert
//             Assert.Equal(expected, result);
//         }
//         
//         [Theory, AutoMoqData]
//         public void GivenSimpsonService_WhenGettingById_ReturnsExpectedCharacter_3(
//             int characterId,
//             string expected,
//             [Frozen] Mock<ICharacterRepository> mockCharacterRepository,
//             SimpsonService sut)
//         {
//             // Arrange
//             mockCharacterRepository.Setup(cr => cr.GetById(characterId)).Returns(expected);
//         
//             // Act
//             var result = sut.GetCharacterById(characterId);
//         
//             // Assert
//             result.ShouldBe(expected);
//         }
//     }
// }