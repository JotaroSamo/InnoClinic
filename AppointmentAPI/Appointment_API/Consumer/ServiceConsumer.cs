using Appointment_API.DataAccess.IService;
using Appointment_API.Domain.Model;
using Global.Dto;
using MassTransit;

namespace Appointment_API.Consumer
{
    public class ServiceConsumer : IConsumer<CreateService>, IConsumer<UpdateService>, IConsumer<DeleteService>
    {
        private readonly IServiceAppointmentService _serviceAppointmentService;

        public ServiceConsumer(IServiceAppointmentService serviceAppointmentService)
        {
            _serviceAppointmentService = serviceAppointmentService;
        }

        public async Task Consume(ConsumeContext<CreateService> context)
        {
            var message = context.Message;

            var service = new ServiceAppointment
            {
                Id = message.Service.Id,
                Service_Name = message.Service.Service_Name,
                Service_Price = message.Service.Service_Price
            };

            // Создание новой услуги
            await _serviceAppointmentService.CreateService(service);
        }

        public async Task Consume(ConsumeContext<UpdateService> context)
        {
            var message = context.Message;

            var service = new ServiceAppointment
            {
                Id = message.Service.Id,
                Service_Name = message.Service.Service_Name,
                Service_Price = message.Service.Service_Price
            };

            // Обновление информации об услуге
            await _serviceAppointmentService.UpdateService(service);
        }

        public async Task Consume(ConsumeContext<DeleteService> context)
        {
            var message = context.Message;

            // Удаление услуги
            await _serviceAppointmentService.DeleteService(message.Id);
        }
    }
}
