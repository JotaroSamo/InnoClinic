using CSharpFunctionalExtensions;
using Profile_API.DataAccess.Repositories;
using Profile_API.Domain.Abstract.IService;
using Profile_API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile_API.Application.Service
{
    public class ReceptionistService : IReceptionistService
    {
        private readonly IReceptionistRepository _receptionistRepository;

        public ReceptionistService(IReceptionistRepository receptionistRepository)
        {
            _receptionistRepository = receptionistRepository;
        }

        public async Task<Result<Receptionist>> CreateReceptionistAsync(Receptionist receptionist)
        {
            var creationResult = await _receptionistRepository.Create(receptionist);
            if (creationResult.IsFailure)
                return Result.Failure<Receptionist>("Failed to create receptionist");

            return Result.Success(creationResult.Value);
        }

        public async Task<Result> DeleteReceptionistAsync(Guid id)
        {
            var deleteResult = await _receptionistRepository.Delete(id);
            if (deleteResult.IsFailure)
                return Result.Failure("Failed to delete receptionist");

            return Result.Success();
        }

        public async Task<Result<List<Receptionist>>> GetAllReceptionistsAsync()
        {
            var receptionistsResult = await _receptionistRepository.GetAll();
            return Result.Success(receptionistsResult.Value);
        }

        public async Task<Result<Receptionist>> GetReceptionistByIdAsync(Guid id)
        {
            var receptionistResult = await _receptionistRepository.GetById(id);
            if (receptionistResult.IsFailure)
                return Result.Failure<Receptionist>("Receptionist not found");

            return Result.Success(receptionistResult.Value);
        }

        public async Task<Result<Receptionist>> UpdateReceptionistAsync(Guid id, Receptionist receptionist)
        {
            var updateResult = await _receptionistRepository.Update(id, receptionist);
            if (updateResult.IsFailure)
                return Result.Failure<Receptionist>("Failed to update receptionist");

            return Result.Success(updateResult.Value);
        }
    }

}
