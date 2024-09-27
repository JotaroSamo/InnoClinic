using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Office_API.Domain.Abstract.Repositories;
using Office_API.Domain.Enums;
using Office_API.Domain.Model;
using Office_API.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office_API.DataAccess.Repositories
{
    public class OfficeRepositories : IOfficeRepositories
    {
        private readonly OfficeDbContext _officeDbContext;
        private readonly IMapper _mapper;

        public OfficeRepositories(OfficeDbContext officeDbContext, IMapper mapper)
        {
            _officeDbContext = officeDbContext;
            _mapper = mapper;
        }

        public async Task<Office> Add(Office office)
        {
            var officeEntity = _mapper.Map<OfficeEntity>(office);
            await _officeDbContext.Offices.AddAsync(officeEntity);
            await _officeDbContext.SaveChangesAsync();
            return office;
        }
        public async Task<bool> Delete(Guid id)
        {
            var officeEntity = await _officeDbContext.Offices.FindAsync(id);
            if (officeEntity == null)
            {
                return false;
            }
            await _officeDbContext.Offices.Where(i => i.Id == id).ExecuteDeleteAsync();
            return true;
        }
        public async Task<Office> GetById(Guid id)
        {
            var officeEntity = await _officeDbContext.Offices.FindAsync(id);
            return _mapper.Map<Office>(officeEntity);
        }
        public async Task<List<Office>> GetAll()
        {
            var offficeEntities = await _officeDbContext.Offices.AsNoTracking().ToListAsync();
            return _mapper.Map<List<Office>>(offficeEntities);
        }
        public async Task<Office> ChangeStatus(Guid id, Status isActive)
        {
            var officeEntity = await _officeDbContext.Offices.FindAsync(id);
            if (officeEntity == null)
            {
                return null;
            }
            officeEntity.IsActive = isActive;
            await _officeDbContext.SaveChangesAsync();
            return _mapper.Map<Office>(officeEntity);
        }
        public async Task<Office> Update(Office office)
        {
            var officeEntity = await _officeDbContext.Offices.FirstOrDefaultAsync(i=>i.Id==office.Id);
            if (officeEntity == null)
            {
                return null;
            }

            officeEntity.City = office.City;
            officeEntity.Street = office.Street;
            officeEntity.HouseNumber = office.HouseNumber;
            officeEntity.OfficeNumber = office.OfficeNumber;
            officeEntity.RegistryPhoneNumber = office.RegistryPhoneNumber;
            officeEntity.PhotoId = office.PhotoId;
            officeEntity.PhotoUrl = office.PhotoUrl;
            officeEntity.IsActive = office.IsActive;

            await _officeDbContext.SaveChangesAsync();

            return _mapper.Map<Office>(officeEntity);
        }

    }
}
