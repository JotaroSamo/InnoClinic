using CSharpFunctionalExtensions;
using Profile_API.DataAccess.Repositories;
using Profile_API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile_API.Application.Service
{
    public class SpecializationService : ISpecializationService
    {
        private readonly ISpecializationRepository _specializationRepository;

        public SpecializationService(ISpecializationRepository specializationRepository)
        {
            _specializationRepository = specializationRepository;
        }

        public async Task<Result<Specialization>> CreateSpecializationAsync(Specialization specialization)
        {
            var creationResult = await _specializationRepository.Create(specialization);
            if (creationResult.IsFailure)
                return Result.Failure<Specialization>("Failed to create specialization");

            return Result.Success(creationResult.Value);
        }

        public async Task<Result> DeleteSpecializationAsync(Guid id)
        {
            var deleteResult = await _specializationRepository.Delete(id);
            if (deleteResult.IsFailure)
                return Result.Failure("Failed to delete specialization");

            return Result.Success();
        }

        public async Task<Result<List<Specialization>>> GetAllSpecializationsAsync()
        {
            var specializationsResult = await _specializationRepository.GetAll();
            return Result.Success(specializationsResult.Value);
        }

        public async Task<Result<Specialization>> GetSpecializationByIdAsync(Guid id)
        {
            var specializationResult = await _specializationRepository.GetById(id);
            if (specializationResult.IsFailure)
                return Result.Failure<Specialization>("Specialization not found");

            return Result.Success(specializationResult.Value);
        }

        public async Task<Result<Specialization>> UpdateSpecializationAsync(Guid id, Specialization specialization)
        {
            var updateResult = await _specializationRepository.Update(id, specialization);
            if (updateResult.IsFailure)
                return Result.Failure<Specialization>("Failed to update specialization");

            return Result.Success(updateResult.Value);
        }
    }
}
