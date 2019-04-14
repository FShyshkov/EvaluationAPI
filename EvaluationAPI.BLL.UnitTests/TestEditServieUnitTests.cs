using EvaluationAPI.BLL.Contracts;
using EvaluationAPI.BLL.DTO;
using EvaluationAPI.BLL.Services;
using EvaluationAPI.DAL.Contracts;
using EvaluationAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using Xunit;

namespace EvaluationAPI.BLL.UnitTests
{
    public class TestEditServieUnitTests
    {
        
        [Fact]
        public async void AddTestAsync_GivenValidInput_ShouldProduceValidResponse()
        {
            // arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockDTOMapper = new Mock<IDTOMapper>();
            var mockTransaction = new Mock<IDbContextTransaction>();
            var testEditService = new TestEditService(mockUnitOfWork.Object, mockDTOMapper.Object);
            mockUnitOfWork.Setup(uow => uow.StartTransaction()).ReturnsAsync(mockTransaction.Object);
            mockUnitOfWork.Setup(uow => uow.Tests.Add(It.IsAny<Test>())).ReturnsAsync(new Test());
            mockDTOMapper.Setup(map => map.MapTest(It.IsAny<Test>())).Returns(new TestDTO() { TestId = -1 });


            // act
            var actual = await testEditService.AddTestAsync("");

            // assert
            Assert.False(actual.ErrorOccured);
            Assert.True(actual.Model.TestId == -1);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Fact]
        public async void UpdateTestAsync_GivenValidInput_ShouldProduceValidResponse()
        {
            // arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockDTOMapper = new Mock<IDTOMapper>();
            var mockTransaction = new Mock<IDbContextTransaction>();
            var testEditService = new TestEditService(mockUnitOfWork.Object, mockDTOMapper.Object);
            mockUnitOfWork.Setup(uow => uow.StartTransaction()).ReturnsAsync(mockTransaction.Object);
            mockUnitOfWork.Setup(uow => uow.Tests.Update(It.IsAny<Test>()));
            mockDTOMapper.Setup(map => map.MapTest(It.IsAny<Test>())).Returns(new TestDTO() { TestId = -1 });


            // act
            var actual = await testEditService.UpdateTestAsync(1, "");

            // assert
            Assert.False(actual.ErrorOccured);
            Assert.True(actual.Model.TestId == -1);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
            mockUnitOfWork.Verify(x => x.Tests.Update(It.IsAny<Test>()), Times.Once);
        }

        [Fact]
        public async void AddQuestionAsync_GivenValidInput_ShouldProduceValidResponse()
        {
            // arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockDTOMapper = new Mock<IDTOMapper>();
            var mockTransaction = new Mock<IDbContextTransaction>();
            var testEditService = new TestEditService(mockUnitOfWork.Object, mockDTOMapper.Object);
            mockUnitOfWork.Setup(uow => uow.StartTransaction()).ReturnsAsync(mockTransaction.Object);
            mockUnitOfWork.Setup(uow => uow.Questions.Add(It.IsAny<Question>())).ReturnsAsync(new Question());
            mockDTOMapper.Setup(map => map.MapDTOQuestion(It.IsAny<QuestionDTO>())).Returns(new Question() { });
            mockDTOMapper.Setup(map => map.MapQuestion(It.IsAny<Question>())).Returns(new QuestionDTO() { QuestionId = -1 });


            // act
            var actual = await testEditService.AddQuestionAsync("", new string[] { "" }, new int[] { 0 }, 1);

            // assert
            Assert.False(actual.ErrorOccured);
            Assert.True(actual.Model.QuestionId == -1);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Fact]
        public async void UpdateQuestionAsync_GivenValidInput_ShouldProduceValidResponse()
        {
            // arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockDTOMapper = new Mock<IDTOMapper>();
            var mockTransaction = new Mock<IDbContextTransaction>();
            var testEditService = new TestEditService(mockUnitOfWork.Object, mockDTOMapper.Object);
            mockUnitOfWork.Setup(uow => uow.StartTransaction()).ReturnsAsync(mockTransaction.Object);
            mockUnitOfWork.Setup(uow => uow.Questions.Update(It.IsAny<Question>()));
            mockDTOMapper.Setup(map => map.MapDTOQuestion(It.IsAny<QuestionDTO>())).Returns(new Question() { });
            mockDTOMapper.Setup(map => map.MapQuestion(It.IsAny<Question>())).Returns(new QuestionDTO() { QuestionId = -1 });

            // act
            var actual = await testEditService.UpdateQuestionAsync(1,"", new string[] { "" }, new int[] { 0 }, 1);

            // assert
            Assert.False(actual.ErrorOccured);
            Assert.True(actual.Model.QuestionId == -1);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
            mockUnitOfWork.Verify(x => x.Questions.Update(It.IsAny<Question>()), Times.Once);
        }
    }
}
