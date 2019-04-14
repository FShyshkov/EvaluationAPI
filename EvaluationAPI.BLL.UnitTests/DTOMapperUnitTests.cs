using EvaluationAPI.BLL.Common;
using EvaluationAPI.BLL.DTO;
using EvaluationAPI.BLL.Exceptions;
using EvaluationAPI.DAL.Entities;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace EvaluationAPI.BLL.UnitTests
{
    public class DTOMapperUnitTests
    {
        [Fact]
        public void MapQuestion_GivenInvalidQuestionText_ShouldThrowQuestionMappingException()
        {
            // arrange
            var mapper = new DTOMapper();
            string expectedErrorMessage = "Should be more than one possible answer";

            // act
            Action actual = () => mapper.MapQuestion(new Question() { QuestionText = "1" } );

            // assert
            var actualError = Assert.Throws<QuestionMappingException>(actual);
            Assert.Equal(expectedErrorMessage, actualError.Message);
        }

        [Fact]
        public void MapQuestion_GivenInvalidAnswer_ShouldThrowQuestionMappingException()
        {
            // arrange
            var mapper = new DTOMapper();
            string expectedErrorMessage = "Correct answer is corrupted";
            // act
            Action actual = () => mapper.MapQuestion(new Question() { QuestionText = "0#-#1", Answer ="" });

            // assert
            var actualError = Assert.Throws<QuestionMappingException>(actual);
            Assert.Equal(expectedErrorMessage, actualError.Message);
        }

        [Fact]
        public void MapDTOQuestion_GivenInvalidPossibleAnswersLength_ShouldThrowQuestionMappingException()
        {
            // arrange
            var mapper = new DTOMapper();
            string expectedErrorMessage = "Should be more than one possible answer";
            // act
            Action actual = () => mapper.MapDTOQuestion(new QuestionDTO() { PossibleAnswers = new string[] { "" } });

            // assert
            var actualError = Assert.Throws<QuestionMappingException>(actual);
            Assert.Equal(expectedErrorMessage, actualError.Message);
        }

        [Fact]
        public void MapDTOQuestion_GivenAnswerValueGreaterThanNumberOfPossibleQuestions_ShouldThrowQuestionMappingException()
        {
            // arrange
            var mapper = new DTOMapper();
            List<int> stubList = new List<int>();
            stubList.Add(5);
            string expectedErrorMessage = "Correct answer id is corrupted";
            // act
            Action actual = () => mapper.MapDTOQuestion(new QuestionDTO() { PossibleAnswers = new string[] { "", "" }, Answer = stubList });

            // assert
            var actualError = Assert.Throws<QuestionMappingException>(actual);
            Assert.Equal(expectedErrorMessage, actualError.Message);
        }

        [Fact]
        public void MapDTOQuestion_GivenPossibleAnswersContainIllegalCombination_ShouldThrowQuestionMappingException()
        {
            // arrange
            var mapper = new DTOMapper();
            string expectedErrorMessage = "Possible answers contain illegal character combination #-#";
            // act
            Action actual = () => mapper.MapDTOQuestion(new QuestionDTO() { PossibleAnswers = new string[] { "", "#-#" }, Answer = new List<int>() { 1 } });

            // assert
            var actualError = Assert.Throws<QuestionMappingException>(actual);
            Assert.Equal(expectedErrorMessage, actualError.Message);
        }
    }
}
