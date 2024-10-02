using Global.Dto;
using MassTransit;
using Profile_API.Application.Service;

namespace Profile_API.Consumer
{
    public class DeleteSpecializationConsumer : IConsumer<DeleteSpecialization>
    {
        private readonly ISpecializationService _specializationService;

        public DeleteSpecializationConsumer(ISpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        public async Task Consume(ConsumeContext<DeleteSpecialization> context)
        {
            var speccontext = context.Message;
            Guid id = speccontext.Id;
            await _specializationService.DeleteSpecializationAsync(id);
        }
    }
}
