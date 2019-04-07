﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using EvaluationAPI.BLL.Contracts;
using EvaluationAPI.BLL.DTO;
using EvaluationAPI.BLL.Responses;
using EvaluationAPI.DAL.Contracts;
using EvaluationAPI.DAL.Entities;
using EvaluationAPI.DAL.Extensions;

namespace EvaluationAPI.BLL.Services
{
    public class EvaluationService : IEvaluationService
    {
        private readonly IUnitOfWork _evalUOW;
        private bool disposedValue = false;
        private readonly IDTOMapper _mapper;


        public EvaluationService(IUnitOfWork uow, IDTOMapper mapper)
        {
            _evalUOW = uow ?? throw new ArgumentNullException(nameof(uow));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ISingleResponse<ResultDTO>> AddResultForTestAsync(long testId, string userName, ResultDTO result)
        {
            var response = new SingleResponse<ResultDTO>();
            var tempResult = _mapper.MapDTOResult(result);

            using (var transaction = await _evalUOW.StartTransaction())
            {
                try
                {
                    await _evalUOW.Results.Add(tempResult);
                    await _evalUOW.SaveAsync();
                    response.Model = result;
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    response.SetError(nameof(AddResultForTestAsync), ex);
                }
            }
            return response;
        }

        public async Task<IPagedResponse<ResultDTO>> GetResultsByTestAsync(long testId, int pageSize, int pageNumber)
        {
            var response = new PagedResponse<ResultDTO>();

            try
            {
                // Get query
                IQueryable<Result> query = _evalUOW.Results.GetAll().Where(x => x.TestId == testId);

                // Set information for paging
                response.PageSize = pageSize;
                response.PageNumber = pageNumber;
                response.ItemsCount = await query.CountAsync();

                // Retrieve items, set model for response
                var resultList = await query.Paging(pageSize, pageNumber).ToListAsync();
                var resultListDTO = new List<ResultDTO>();
                foreach (var res in resultList)
                {
                    resultListDTO.Add(_mapper.MapResult(res));
                }

                response.Model = resultListDTO;
            }
            catch (Exception ex)
            {
                response.SetError(nameof(GetResultsByTestAsync), ex);
            }

            return response;
        }

        public async Task<IPagedResponse<ResultDTO>> GetResultsByUserAsync(string userName, int pageSize, int pageNumber)
        {
            var response = new PagedResponse<ResultDTO>();

            try
            {
                // Get query
                IQueryable<Result> query = _evalUOW.Results.GetAll().Where(x => x.UserName == userName);

                // Set information for paging
                response.PageSize = pageSize;
                response.PageNumber = pageNumber;
                response.ItemsCount = await query.CountAsync();

                // Retrieve items, set model for response
                var resultList = await query.Paging(pageSize, pageNumber).ToListAsync();
                var resultListDTO = new List<ResultDTO>();
                foreach (var res in resultList)
                {
                    resultListDTO.Add(_mapper.MapResult(res));
                }

                response.Model = resultListDTO;
            }
            catch (Exception ex)
            {
                response.SetError(nameof(GetResultsByTestAsync), ex);
            }

            return response;
        }

        public async Task<ISingleResponse<TestDTO>> GetTestAsync(long id)
        {
            var response = new SingleResponse<TestDTO>();

            try
            {
                var test = await _evalUOW.Tests.Get(x => x.TestId == id, null, "Questions,Results");
                var testDTO = _mapper.MapTest(test.FirstOrDefault());

                response.Model = testDTO;
            }
            catch (Exception ex)
            {
                response.SetError(nameof(GetResultsByTestAsync), ex);
            }

            return response;
        }

        public async Task<IPagedResponse<TestDTO>> GetTestsSummaryAsync(int pageSize = 10, int pageNumber = 1)
        {
            var response = new PagedResponse<TestDTO>();

            try
            {
                // Get query
                IQueryable<Test> query = _evalUOW.Tests.GetAll();

                // Set information for paging
                response.PageSize = pageSize;
                response.PageNumber = pageNumber;
                response.ItemsCount = await query.CountAsync();

                // Retrieve items, set model for response
                var testList = await query.Paging(pageSize, pageNumber).ToListAsync();
                var testListDTO = new List<TestDTO>();
                foreach (var tes in testList)
                {
                    testListDTO.Add(_mapper.MapTest(tes));
                }

                response.Model = testListDTO;
            }
            catch (Exception ex)
            {
                response.SetError(nameof(GetResultsByTestAsync), ex);
            }

            return response;
        }               

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _evalUOW.Dispose();
                }
                
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}