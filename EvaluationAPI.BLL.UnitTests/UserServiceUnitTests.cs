using EvaluationAPI.BLL.Contracts;
using EvaluationAPI.BLL.DTO;
using EvaluationAPI.DAL.Contracts;
using EvaluationAPI.DAL.Identity.IdentityEntity;
using EvaluationAPI.BLL.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using EvaluationAPI.BLL.Responses;
using EvaluationAPI.BLL.Requests;
using Newtonsoft.Json;
using EvaluationAPI.Presenters;
using EvaluationAPI.DAL.Identity.Responses;
using System.Net;
using System.Linq;

namespace EvaluationAPI.BLL.UnitTests
{
    public class UserServiceUnitTests
    {


        [Fact]
        public async void HandleLogin_GivenValidCredentials_ShouldSucceed()
        {
            // arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(repo => repo.Users.FindByName(It.IsAny<string>())).ReturnsAsync(new EvaluationUser() { FirstName = "", LastName = "", Id = "", UserName = "" });

            mockUnitOfWork.Setup(repo => repo.Users.CheckPassword(It.IsAny<EvaluationUser>(), It.IsAny<string>())).ReturnsAsync(true);

            var mockJwtFactory = new Mock<IJwtFactory>();
            mockJwtFactory.Setup(factory => factory.GenerateEncodedToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<string>>())).ReturnsAsync(new AccessToken("", 0));


            var userService = new UserService(mockUnitOfWork.Object, mockJwtFactory.Object);

            var mockOutputPort = new Mock<IOutputPort<LoginResponse>>();
            mockOutputPort.Setup(outputPort => outputPort.Handle(It.IsAny<LoginResponse>()));

            // act
            var response = await userService.Handle(new LoginRequest("userName", "password", "127.0.0.1"), mockOutputPort.Object);

            // assert
            Assert.True(response);
        }


        [Fact]
        public void HandleLogin_GivenSuccessfulUseCaseResponse_SetsAccessToken()
        {
            // arrange
            const string token = "777888AAABBB";
            var presenter = new LoginPresenter();

            // act
            presenter.Handle(new LoginResponse(new AccessToken(token, 0),  true, ""));

            // assert
            dynamic data = JsonConvert.DeserializeObject(presenter.ContentResult.Content);
            Assert.Equal(token, data.accessToken.token.Value);
        }

        [Fact]
        public void HandleLogin_GivenFailedUseCaseResponse_SetsErrors()
        {
            // arrange
            var presenter = new LoginPresenter();

            // act
            presenter.Handle(new LoginResponse(new[] { new Error("", "Invalid username/password") }));

            // assert
            var data = JsonConvert.DeserializeObject<IEnumerable<Error>>(presenter.ContentResult.Content);
            Assert.Equal((int)HttpStatusCode.Unauthorized, presenter.ContentResult.StatusCode);
            Assert.Equal("Invalid username/password", data.First().Description);
        }

        [Fact]
        public void HandleRegister_GivenSuccessfulUseCaseResponse_SetsOKHttpStatusCode()
        {
            // arrange
            var presenter = new RegisterUserPresenter();

            // act
            presenter.Handle(new RegisterUserResponse("", true));

            // assert
            Assert.Equal((int)HttpStatusCode.OK, presenter.ContentResult.StatusCode);
        }

        [Fact]
        public void HandleRegister_GivenSuccessfulUseCaseResponse_SetsId()
        {
            // arrange
            var presenter = new RegisterUserPresenter();

            // act
            presenter.Handle(new RegisterUserResponse("1234", true));

            // assert
            dynamic data = JsonConvert.DeserializeObject(presenter.ContentResult.Content);
            Assert.True(data.success.Value);
            Assert.Equal("1234", data.id.Value);
        }

        [Fact]
        public void HandleRegister_GivenFailedUseCaseResponse_SetsErrors()
        {
            // arrange
            var presenter = new RegisterUserPresenter();

            // act
            presenter.Handle(new RegisterUserResponse(new[] { "missing first name" }));

            // assert
            dynamic data = JsonConvert.DeserializeObject(presenter.ContentResult.Content);
            Assert.False(data.success.Value);
            Assert.Equal("missing first name", data.errors.First.Value);
        }
    }
}
