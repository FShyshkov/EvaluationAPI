using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using EvaluationAPI.BLL.Contracts;
using EvaluationAPI.BLL.DTO;
using EvaluationAPI.DAL.Entities;
using EvaluationAPI.BLL.Exceptions;

namespace EvaluationAPI.BLL.Common
{
    public class DTOMapper : IDTOMapper
    {
        public QuestionDTO MapQuestion(Question question)
        {
            var tempPAn = question.QuestionText.Split("#-#", StringSplitOptions.RemoveEmptyEntries);
            if (tempPAn.Length < 2)
            {
                throw new QuestionMappingException("Should be more than one possible answer");
            }
            if (tempPAn.Length != question.Answer.Length)
            {
                throw new QuestionMappingException("Correct answer is corrupted");
            }           
            var tempCAnString = new StringBuilder(question.Answer);
            List<int> tempCAns = new List<int>();
            for (int i = 0; i < tempCAnString.Length; i++)
            {
                if(tempCAnString[i] == '1')
                {
                    tempCAns.Add(i + 1);
                }
            }
            return new QuestionDTO()
            {
                QuestionId = question.QuestionId,
                QuestionText = question.Name,
                PossibleAnswers = tempPAn,
                Answer = tempCAns,
                TestId = question.TestId
            };
        }

        public Question MapDTOQuestion(QuestionDTO question)
        {
            int tempLength = question.PossibleAnswers.Length;
            if (tempLength < 2)
            {
                throw new QuestionMappingException("Should be more than one possible answer");
            }
            int[] tempAnArray = new int[tempLength];
            foreach (int i in question.Answer)
            {
                if (i < 1 || tempLength < (i-1))
                {
                    throw new QuestionMappingException("Correct answer id is corrupted");
                }
                tempAnArray[i - 1] = 1; 
            }
            var tempAn = String.Concat(tempAnArray);
            if (tempLength != tempAn.Length)
            {
                throw new QuestionMappingException("Correct answer is corrupted");
            }
            if (question.PossibleAnswers.Contains("#-#"))
            {
                throw new QuestionMappingException("Possible answers contain illegal character combination #-#");
            }
            return new Question()
            {
                QuestionId = question.QuestionId,
                Name = question.QuestionText,
                QuestionText = String.Join("#-#", question.PossibleAnswers),
                Answer = tempAn,
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
