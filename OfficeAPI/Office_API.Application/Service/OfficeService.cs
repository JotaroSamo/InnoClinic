using CSharpFunctionalExtensions;
using Office_API.Domain.Abstract.Repositories;
using Office_API.Domain.Abstract.Service;
using Office_API.Domain.Enums;
using Office_API.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office_API.Application.Service
{
    public class OfficeService : IOfficeService
    {
        private readonly IOfficeRepositories _officeRepositories;

        public OfficeService(IOfficeRepositories officeRepositories)
        {
            _officeRepositories = officeRepositories;
        }

        public async Task<Result<Office>> AddOffice( string city, string street,
            string houseNumber, string officeNumber,
            string registryPhoneNumber, Status isActive, Guid photoId = default, string photoUrl = default)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return Result.Failure<Office>("City cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(street))
            {
                return Result.Failure<Office>("Street cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(houseNumber))
            {
                return Result.Failure<Office>("HouseNumber cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(officeNumber))
            {
                return Result.Failure<Office>("OfficeNumber cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(registryPhoneNumber))
            {
                return Result.Failure<Office>("RegistryPhoneNumber cannot be empty.");
            }
            var office = Office.Create(Guid.NewGuid(), 
               city, street,houseNumber ,officeNumber,
                registryPhoneNumber, isActive,
                photoUrl: photoUrl);
            if (office.IsFailure)
            {
                return Result.Failure<Office>(office.Error);
            }

            await _officeRepositories.Add(office.Value);

            return Result.Success(office.Value);
        }

        public async Task<Result<Office>> ChangeStatusOffice(Guid id, Status isActive)
        {
            var office = await _officeRepositories.ChangeStatus(id, isActive);
            if (office == null)
            {
                return Result.Failure<Office>("Office is null.");
            }
            return Result.Success(office);
        }

        public async Task<Result<bool>> DeleteteOffice(Guid id)
        {
            var office = await _officeRepositories.Delete(id);
            if (!office)
            {
                return Result.Failure<bool>("Office not found.");
            }

            return Result.Success(true);
        }

        public async Task<Result<List<Office>>> GetAllOffices()
        {
            var office = await _officeRepositories.GetAll();
            if (office is  null)
            {
                return Result.Failure<List<Office>>("Office is null");
            }

            return Result.Success(office);
        }

        public async Task<Result<Office>> GetByIdOffice(Guid id)
        {
            var office = await _officeRepositories.GetById(id);
            if (office is null)
            {
                return Result.Failure<Office>("Office is null.");
            }

            return Result.Success(office);
        }

        public async Task<Result<Office>> UpdateOffice(Guid id ,string city, string street,
            string houseNumber, string officeNumber,
            string registryPhoneNumber, Status isActive, Guid photoId = default, string photoUrl = default)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return Result.Failure<Office>("City cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(street))
            {
                return Result.Failure<Office>("Street cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(houseNumber))
            {
                return Result.Failure<Office>("HouseNumber cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(officeNumber))
            {
                return Result.Failure<Office>("OfficeNumber cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(registryPhoneNumber))
            {
                return Result.Failure<Office>("RegistryPhoneNumber cannot be empty.");
            }
            var office = Office.Create(id,
              city, street, houseNumber, officeNumber,
               registryPhoneNumber, isActive,
               photoUrl: photoUrl);
            if (office.IsFailure)
            {
                return Result.Failure<Office>(office.Error);
            }
            var upOffice= await _officeRepositories.Update(office.Value);
            return Result.Success(upOffice);

        }
    }

}
