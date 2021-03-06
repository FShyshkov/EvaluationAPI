﻿using System.Threading.Tasks;
using EvaluationAPI.BLL.Contracts;
using EvaluationAPI.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
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
        private readonly ITestEditService _testEditService;

        public TestsController(IEvaluationService evaluationService, ITestEditService testEditService)
        {
            _evaluationService = evaluationService;
            _testEditService = testEditService;
        }

        //GET api/v1.0/tests/
        [HttpGet]
        public async Task<IActionResult> GetTestsSummaryAsync(int? pageSize = 10, int? pageNumber = 1)
        {
            // Get response from business logic
            var response = await _evaluationService.GetTestsSummaryAsync((int)pageSize, (int)pageNumber);

            // Return as http response
            return response.ToHttpResponse();
        }
        //GET api/v1.0/tests/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTestAsync(int? id)
        {
            // Get response from business logic
            var response = await _evaluationService.GetTestAsync((int)id);

            // Return as http response
            return response.ToHttpResponse();
        }

        //POST api/v1.0/tests/{id}/Results
        [HttpPost("{id}/Result")]
        public async Task<IActionResult> AddResultForTestAsync(int id, [FromBody] Models.Requests.ResultRequest result )
        {
            var response = await _evaluationService.AddResultForTestAsync(id, result.UserName, result.UserResult);
            return response.ToHttpResponse();
        }

        //POST api/v1.0/tests/{id}/Results/{name}
        [HttpPost("{id}/Results/{name}")]
        public async Task<IActionResult> GetResultsForUserByTestAsync(string name, int id, int? pageSize, int? pageNumber)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(name, @"^[a-zA-Z0-9]+$"))
            {
                return BadRequest("username should be alphanumeric");
            }
            var response = await _evaluationService.GetResultsForUserByTestAsync(name, id, (int)pageSize, (int)pageNumber);
            return response.ToHttpResponse();
        }
        //POST  api/v1.0/tests/
        [Authorize(Policy = "TestEditor")]
        [HttpPost]
        public async Task<IActionResult> AddTestAsync([FromBody] EvaluationAPI.Models.Requests.TestRequest test)
        {
            var response = await _testEditService.AddTestAsync(test.TestName);
            return response.ToHttpResponse();
        }
        //PUT  api/v1.0/tests/{id}
        [Authorize(Policy = "TestEditor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTestAsync(int id, [FromBody] EvaluationAPI.Models.Requests.TestRequest test)
        {
            var response = await _testEditService.UpdateTestAsync(id, test.TestName);
            return response.ToHttpResponse();
        }

        //POST  api/v1.0/tests/{id}/Questions
        [Authorize(Policy = "TestEditor")]
        [HttpPost("{id}/Questions")]
        public async Task<IActionResult> AddQuestionAsync(int id, [FromBody] EvaluationAPI.Models.Requests.AddQuestionRequest request)
        {
            var response = await _testEditService.AddQuestionAsync(request.QuestionText, request.PossibleAnswers, request.correctAnswers, id);
            return response.ToHttpResponse();
        }
        //PUT  api/v1.0/tests/{id}/Questions
        [Authorize(Policy = "TestEditor")]
        [HttpPut("{id}/Questions")]
        public async Task<IActionResult> UpdateQuestionInTestAsync(int id, [FromBody] EvaluationAPI.Models.Requests.UpdateQuestionRequest request)
        {
            var response = await _testEditService.UpdateQuestionAsync(request.QuestionId, request.QuestionText, request.PossibleAnswers, request.correctAnswers, id);
            return response.ToHttpResponse();
        }
        //PUT  api/v1.0/tests/{id}/Questions/{id2}
        [Authorize(Policy = "TestEditor")]
        [HttpPut("{id}/Questions/{id2}")]
        public async Task<IActionResult> UpdateQuestionInTestWithIdAsync(int id, int id2, [FromBody] EvaluationAPI.Models.Requests.UpdateQuestionRequest request)
        {
            var response = await _testEditService.UpdateQuestionAsync(id2, request.QuestionText, request.PossibleAnswers, request.correctAnswers, id);
            return response.ToHttpResponse();
        }
    }
}