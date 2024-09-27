using CSharpFunctionalExtensions;
using Service_API.Domain.Abstract.IRepository;
using Service_API.Domain.Abstract.IService;
using Service_API.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_API.Application.Services
{
    public class SpecializationService : ISpecializationService
    {
        private readonly ISpecializationRepository _specializationRepository;

        public SpecializationService(ISpecializationRepository specializationRepository)
        {
            _specializationRepository = specializationRepository;
        }

        public async Task<Result<List<Specialization>>> GetAllSpecialization()
        {
            var result = await _specializationRepository.GetAll();
            return result;
        }

        public async Task<Result<Specialization>> GetByIdSpecialization(Guid id)
        {
            var result = await _specializationRepository.GetById(id);
            return result;
        }

        public async Task<Result<Specialization>> CreateSpecialization(Specialization specialization)
        {
            var result = await _specializationRepository.Create(specialization);
            return result;
        }

        public async Task<Result> DeleteSpecialization(Guid id)
        {
            var result = await _specializationRepository.Delete(id);
            return result;
        }

        public async Task<Result<Specialization>> UpdateSpecialization(Specialization specialization)
        {
            var result = await _specializationRepository.Update(specialization);
            return result;
        }
    }

}
