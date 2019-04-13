using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationAPI.Models.Requests
{
    public class AddQuestionRequest
    {
        public string QuestionText;
        public string[] PossibleAnswers;
        public int[] correctAnswers;
    }
}
