using Profile_API.Domain.Abstract.IRepository;
using Profile_API.Domain.Abstract.IService;
using Profile_API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile_API.Application.Service
{
    public class ReceptionistSevice : IReceptionistService
    {
        private readonly IReceptionistRepository _receptionistRepository;

        public ReceptionistSevice(IReceptionistRepository receptionistRepository)
        {
            _receptionistRepository = receptionistRepository;
        }
        public async Task<Receptionist> CreateReceptionistAsync(Receptionist receptionist)
        {
            return await _receptionistRepository.CreateReceptionistAsync(receptionist);
        }

        public async Task DeleteReceptionistAsync(Guid id)
        {
            await _receptionistRepository.DeleteReceptionistAsync(id);
        }

        public async Task<List<Receptionist>> GetAllReceptionistsAsync()
        {
           return await _receptionistRepository.GetAllReceptionistsAsync();
        }

        public async Task<Receptionist> GetReceptionistByIdAsync(Guid id)
        {
            return await _receptionistRepository.GetReceptionistByIdAsync(id);
        }

        public async Task<Receptionist> UpdateReceptionistAsync(Guid id, Receptionist receptionist)
        {
            return await _receptionistRepository.UpdateReceptionistAsync(id, receptionist);
        }
    }
}
