using Global.Dto;
using MassTransit;
using Profile_API.Application.Service;
using Profile_API.Domain.Models;

namespace Profile_API.Consumer
{
    public class CreateSpecializationConsumer : IConsumer<CreateSpecialization>
    {
        private readonly ISpecializationService _specializationService;

        public CreateSpecializationConsumer(ISpecializationService specializationService)
        {
            _specializationService = specializationService;
        }
        public async Task Consume(ConsumeContext<CreateSpecialization> context)
        {
            var speccontext = context.Message;
            var spec = new Specialization
            {
                Id = speccontext.Specialization.Id,
                SpecializationName = speccontext.Specialization.Name,
                IsActive = speccontext.Specialization.IsActive
            };
            await _specializationService.CreateSpecializationAsync(spec);

        }
    }
}
