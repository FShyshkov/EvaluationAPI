using System;
using System.Collections.Generic;
using System.Text;
using EvaluationAPI.BLL.Contracts;
using EvaluationAPI.BLL.DTO;
using EvaluationAPI.DAL.Entities;

namespace EvaluationAPI.BLL.Common
{
    public class DTOMapper : IDTOMapper
    {
        public QuestionDTO MapQuestion(Question question)
        {
            return new QuestionDTO()
            {
                QuestionId = question.QuestionId,
                Name = question.Name,
                QuestionText = question.QuestionText,
                Answer = question.Answer,
                TestId = question.TestId
            };
        }

        public Question MapDTOQuestion(QuestionDTO question)
        {
            return new Question()
            {
                QuestionId = question.QuestionId,
                Name = question.Name,
                QuestionText = question.QuestionText,
                Answer = question.Answer,
                TestId = question.TestId
            };
        }

        public ResultDTO MapResult(Result result)
        {
            return new ResultDTO()
            {
               ResultId = result.ResultId,
               UserName = result.UserName,
               TestId = result.TestId,
               TestResult = result.TestResult
            };
        }

        public Result MapDTOResult(ResultDTO result)
        {
            return new Result()
            {
                ResultId = result.ResultId,
                UserName = result.UserName,
                TestId = result.TestId,
                TestResult = result.TestResult
            };
        }

        public TestDTO MapTest(Test test)
        {
            TestDTO tempTest = new TestDTO()
            {
                TestId = test.TestId,
                TestName = test.TestName
            };
            if (test.Questions != null)
            {
                foreach (var q in test.Questions)
                {
                    tempTest.DTOQuestions.Add(MapQuestion(q));                    
                }
            }
            if (test.Results != null)
            {
                foreach (var r in test.Results)
                {
                    tempTest.DTOResults.Add(MapResult(r));
                }
            }
            return tempTest;
        }

        public Test MapDTOTest(TestDTO test)
        {
            Test tempTest = new Test()
            {
                TestId = test.TestId,
                TestName = test.TestName
            };
            if (test.DTOQuestions != null)
            {
                foreach (var q in test.DTOQuestions)
                {
                    tempTest.Questions.Add(MapDTOQuestion(q));
                }
            }
            if (test.DTOResults != null)
            {
                foreach (var r in test.DTOResults)
                {
                    tempTest.Results.Add(MapDTOResult(r));
                }
            }
            return tempTest;
        }
    }
}
