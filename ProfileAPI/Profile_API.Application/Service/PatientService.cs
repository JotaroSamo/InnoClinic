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
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<Result<Patient>> CreatePatientAsync(Patient patient)
        {
            var creationResult = await _patientRepository.Create(patient);
            if (creationResult.IsFailure)
                return Result.Failure<Patient>("Failed to create patient");

            return Result.Success(creationResult.Value);
        }

        public async Task<Result> DeletePatientAsync(Guid id)
        {
            var deleteResult = await _patientRepository.Delete(id);
            if (deleteResult.IsFailure)
                return Result.Failure("Failed to delete patient");

            return Result.Success();
        }

        public async Task<Result<List<Patient>>> GetAllPatientsAsync()
        {
            var patientsResult = await _patientRepository.GetAll();
            return Result.Success(patientsResult.Value);
        }

        public async Task<Result<Patient>> GetPatientByIdAsync(Guid id)
        {
            var patientResult = await _patientRepository.GetById(id);
            if (patientResult.IsFailure)
                return Result.Failure<Patient>("Patient not found");

            return Result.Success(patientResult.Value);
        }

        public async Task<Result<Patient>> UpdatePatientAsync(Guid id, Patient patient)
        {
            var updateResult = await _patientRepository.Update(id, patient);
            if (updateResult.IsFailure)
                return Result.Failure<Patient>("Failed to update patient");

            return Result.Success(updateResult.Value);
        }
    }

}
