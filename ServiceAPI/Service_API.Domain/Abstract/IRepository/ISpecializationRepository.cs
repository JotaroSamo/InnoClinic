using CSharpFunctionalExtensions;
using Service_API.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_API.Domain.Abstract.IRepository
{
    public interface ISpecializationRepository
    {
     
        Task<Result<Specialization>> Create(Specialization specialization);
        Task<Result> Delete(Guid id);
        Task<Result<List<Specialization>>> GetAll();
        Task<Result<Specialization>> GetById(Guid id);
        Task<Result<Specialization>> Update(Specialization specialization);
       
    }
}
