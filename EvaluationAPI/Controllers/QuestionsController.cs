using System.Threading.Tasks;
using EvaluationAPI.BLL.Contracts;
using EvaluationAPI.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;


namespace EvaluationAPI.Controllers
{
    [Authorize(Policy = "TestEditor")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IEvaluationService _evaluationService;
        private readonly ITestEditService _testEditService;
       
        public QuestionsController(IEvaluationService evaluationService, ITestEditService testEditService)
        {
            _evaluationService = evaluationService;
            _testEditService = testEditService;
        }
        //DELETE api/v1.0/questions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionAsync(int id)
        {
            var response = await _testEditService.RemoveQuestionAsync(id);
            return response.ToHttpResponse();
        }
         //PUT api/v1.0/questions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestionAsync(int id, [FromBody] EvaluationAPI.Models.Requests.UpdateQuestionRequest request)
        {
            var response = await _testEditService.UpdateQuestionAsync(id, request.QuestionText, request.PossibleAnswers, request.correctAnswers, request.TestId);
            return response.ToHttpResponse();
        }
    }
}