using System.Threading.Tasks;
using EvaluationAPI.BLL.Contracts;
using EvaluationAPI.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationAPI.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly IEvaluationService _evaluationService;

        public TestsController(IEvaluationService evaluationService)
        {
            _evaluationService = evaluationService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetTestsSummaryAsync(int? pageSize = 10, int? pageNumber = 1)
        {
            // Get response from business logic
            var response = await _evaluationService.GetTestsSummaryAsync((int)pageSize, (int)pageNumber);

            // Return as http response
            return response.ToHttpResponse();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetTestAsync(int? id)
        {
            // Get response from business logic
            var response = await _evaluationService.GetTestAsync((int)id);

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpPost("{id}/Result")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddResultForTestAsync(int id, [FromBody] Models.Requests.ResultRequest result )
        {
            var response = await _evaluationService.AddResultForTestAsync(id, result.UserName, result.UserResult);
            return response.ToHttpResponse();
        }

        [HttpPost("{id}/Results/{name}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetResultsForUserByTestAsync(string name, int id, int? pageSize, int? pageNumber)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(name, @"^[a-zA-Z0-9]+$"))
            {
                return BadRequest("username should be alphanumeric");
            }
            var response = await _evaluationService.GetResultsForUserByTestAsync(name, id, (int)pageSize, (int)pageNumber);
            return response.ToHttpResponse();
        }

    }
}