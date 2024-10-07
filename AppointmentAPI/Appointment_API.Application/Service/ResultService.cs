using Appointment_API.DataAccess.IService;
using Appointment_API.DataAccess.Repository;
using Appointment_API.Domain.Model;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_API.Application.Service
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _resultRepository;

        public ResultService(IResultRepository resultRepository)
        {
            _resultRepository = resultRepository;
        }

        public async Task<Result<Results>> CreateResult(Results result)
        {
            var createResult = await _resultRepository.Create(result);
            return createResult;
        }

        public async Task<Result> DeleteResult(Guid id)
        {
            var result = await _resultRepository.Delete(id);
            return result;
        }

        public async Task<Result<List<Results>>> GetAllResults()
        {
            var result = await _resultRepository.GetAll();
            return result;
        }

        public async Task<Result<Results>> GetResultById(Guid id)
        {
            var result = await _resultRepository.GetById(id);
            return result;
        }

        public async Task<Result<Results>> UpdateResult(Results result)
        {
            var updateResult = await _resultRepository.Update(result);
            return updateResult;
        }
    }

}
