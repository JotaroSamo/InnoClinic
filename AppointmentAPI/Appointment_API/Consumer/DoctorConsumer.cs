using Appointment_API.DataAccess.IService;
using Appointment_API.Domain.Model;
using Global.Dto;
using MassTransit;


namespace Appointment_API.Consumer
{
    public class DoctorConsumer: IConsumer<CreateDoctor>, IConsumer<UpdateDoctor>, IConsumer<DeleteDoctor>
    {
        private readonly IDoctorAppointmentService _doctorService;

        public DoctorConsumer(IDoctorAppointmentService doctorService)
        {
            _doctorService = doctorService;
        }

        public async Task Consume(ConsumeContext<CreateDoctor> context)
        {
            var message = context.Message;

            var doctor = new DoctorAppointment
            {
                Id = message.Doctor.Id,
                Doctro_Name = message.Doctor.Doctro_Name,
                Specialization_Name = message.Doctor.Specialization_Name
            };

            // Создание аккаунта
            await _doctorService.CreateDoctor(doctor);
        }

        public async Task Consume(ConsumeContext<UpdateDoctor> context)
        {
            var message = context.Message;

            var doctor = new DoctorAppointment
            {
                Id = message.Doctor.Id,
                Doctro_Name = message.Doctor.Doctro_Name,
                Specialization_Name = message.Doctor.Specialization_Name
            };

      
            await _doctorService.UpdateDoctor(doctor);
        }

        public async Task Consume(ConsumeContext<DeleteDoctor> context)
        {
            var message = context.Message;

            await _doctorService.DeleteDoctor(message.Id);
        }
    }
}
