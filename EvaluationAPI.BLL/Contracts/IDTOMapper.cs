using System;
using System.Collections.Generic;
using System.Text;
using EvaluationAPI.DAL.Entities;
using EvaluationAPI.BLL.DTO;

namespace EvaluationAPI.BLL.Contracts
{
    public interface IDTOMapper
    {
        QuestionDTO MapQuestion(Question question);
        ResultDTO MapResult(Result result);
        TestDTO MapTest(Test test);
    }
}
