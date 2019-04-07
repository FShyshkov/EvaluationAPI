using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationAPI.BLL.DTO
{
    public class TestDTO
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public ICollection<QuestionDTO> DTOQuestions { get; set; }
        public ICollection<ResultDTO> DTOResults { get; set; }

        public TestDTO()
        {
            DTOQuestions = new List<QuestionDTO>();
            DTOResults = new List<ResultDTO>();
        }
    }
}
