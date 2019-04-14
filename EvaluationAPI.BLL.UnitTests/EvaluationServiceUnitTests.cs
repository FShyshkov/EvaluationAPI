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
    public class EvaluationServiceUnitTests
    {
        [Fact]
        public async void AddResultForTestAsync_GivenInvalidResult_ShouldProduceResponseWithErrorMessage()
        {
            // arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockDTOMapper = new Mock<IDTOMapper>();
            var mockTransaction = new Mock<IDbContextTransaction>();       
            var evaluationService = new EvaluationService(mockUnitOfWork.Object, mockDTOMapper.Object);
            mockUnitOfWork.Setup(uow => uow.StartTransaction()).ReturnsAsync(mockTransaction.Object);

            string expected = "Result cant be less than zero or more than 100";

            // act
            var actual = await evaluationService.AddResultForTestAsync(1, "ABCD", -1);

            // assert
            Assert.Equal(expected, actual.ErrorMessage);
        }

        [Fact]
        public async void AddResultForTestAsync_GivenValidInput_ShouldProduceValidResponse()
        {
            // arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockDTOMapper = new Mock<IDTOMapper>();
            var mockTransaction = new Mock<IDbContextTransaction>();
            var evaluationService = new EvaluationService(mockUnitOfWork.Object, mockDTOMapper.Object);
            mockUnitOfWork.Setup(uow => uow.StartTransaction()).ReturnsAsync(mockTransaction.Object);
            mockUnitOfWork.Setup(uow => uow.Results.Add(It.IsAny<Result>())).ReturnsAsync(new Result());
            mockDTOMapper.Setup(map => map.MapResult(It.IsAny<Result>())).Returns(new ResultDTO() { ResultId = 1 });
            

            // act
            var actual = await evaluationService.AddResultForTestAsync(1, "ABCD", 0);

            // assert
            Assert.False(actual.ErrorOccured);
            Assert.True(actual.Model.ResultId == 1);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Fact]
        public async void GetResultsByUserAsync_GivenInvalidUser_ShouldProduceErrorMessage()
        {
            // arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockDTOMapper = new Mock<IDTOMapper>();
            var evaluationService = new EvaluationService(mockUnitOfWork.Object, mockDTOMapper.Object);

            string expected = "username should be alphanumeric";

            // act
            var actual = await evaluationService.GetResultsByUserAsync("@");

            // assert
            Assert.Equal(expected, actual.ErrorMessage);
        }


    }
}
