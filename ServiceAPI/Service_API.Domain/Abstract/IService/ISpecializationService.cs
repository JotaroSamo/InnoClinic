using CSharpFunctionalExtensions;
using Service_API.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_API.Domain.Abstract.IService
{
    public interface ISpecializationService
    {
        Task<Result<Specialization>> CreateSpecialization(Specialization specialization);
        Task<Result> DeleteSpecialization(Guid id);
        Task<Result<List<Specialization>>> GetAllSpecialization();
        Task<Result<Specialization>> GetByIdSpecialization(Guid id);
        Task<Result<Specialization>> UpdateSpecialization(Specialization specialization);
    }
}
