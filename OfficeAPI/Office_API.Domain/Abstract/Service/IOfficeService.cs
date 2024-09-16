using CSharpFunctionalExtensions;
using Office_API.Domain.Enums;
using Office_API.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office_API.Domain.Abstract.Service
{
    public interface IOfficeService
    {
        Task<Result<Office>> AddOffice( string city, string street,
            string houseNumber, string officeNumber,
            string registryPhoneNumber, Status isActive, Guid photoId = default, string photoUrl = default);
        Task<Result<Office>> ChangeStatusOffice(Guid id, Status isActive);
        Task<Result<bool>> DeleteteOffice(Guid id);
        Task<Result<List<Office>>> GetAllOffices();
        Task<Result<Office>> GetByIdOffice(Guid id);
        Task<Result<Office>> UpdateOffice(Guid id, string city, string street,
            string houseNumber, string officeNumber,
            string registryPhoneNumber, Status isActive, Guid photoId = default, string photoUrl = default);
    }
}
