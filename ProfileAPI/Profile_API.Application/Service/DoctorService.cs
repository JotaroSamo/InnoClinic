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
    public class DoctorService : IDoctorAppointmentService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<Result<Doctor>> CreateDoctorAsync(Doctor doctor)
        {
            var creationResult = await _doctorRepository.Create(doctor);
            if (creationResult.IsFailure)
                return Result.Failure<Doctor>("Failed to create doctor");

            return Result.Success(creationResult.Value);
        }

        public async Task<Result> DeleteDoctorAsync(Guid id)
        {
            var deleteResult = await _doctorRepository.Delete(id);
            if (deleteResult.IsFailure)
                return Result.Failure("Failed to delete doctor");

            return Result.Success();
        }

        public async Task<Result<List<Doctor>>> GetAllDoctorsAsync()
        {
            var doctorsResult = await _doctorRepository.GetAll();
            return Result.Success(doctorsResult.Value);
        }

        public async Task<Result<Doctor>> GetDoctorByIdAsync(Guid id)
        {
            var doctorResult = await _doctorRepository.GetById(id);
            if (doctorResult.IsFailure)
                return Result.Failure<Doctor>("Doctor not found");

            return Result.Success(doctorResult.Value);
        }

        public async Task<Result<Doctor>> GetDoctorByNameAsync(string firstName, string lastName, string middleName)
        {
            var doctorResult = await _doctorRepository.GetByName(firstName, lastName, middleName);
            if (doctorResult.IsFailure)
                return Result.Failure<Doctor>("Doctor not found by name");

            return Result.Success(doctorResult.Value);
        }

        public async Task<Result<List<Doctor>>> GetDoctorListBySpecializationAsync(Guid specId)
        {
            var doctorsResult = await _doctorRepository.GetBySpecialization(specId);
            return Result.Success(doctorsResult.Value);
        }

        public async Task<Result<Doctor>> UpdateDoctorAsync(Guid id, Doctor doctor)
        {
            var updateResult = await _doctorRepository.Update(id, doctor);
            if (updateResult.IsFailure)
                return Result.Failure<Doctor>("Failed to update doctor");

            return Result.Success(updateResult.Value);
        }
    }

}
