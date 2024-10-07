using Appointment_API.DataAccess.IService;
using Appointment_API.Domain.Model;
using Global.Dto;
using MassTransit;

namespace Appointment_API.Consumer
{
    public class PatientConsumer : IConsumer<CreatePatient>, IConsumer<UpdatePatient>, IConsumer<DeletePatient>
    {
        private readonly IPatientAppointmentService _patientService;

        public PatientConsumer(IPatientAppointmentService patientService)
        {
            _patientService = patientService;
        }

        public async Task Consume(ConsumeContext<CreatePatient> context)
        {
            var message = context.Message;

            var patient = new PatientAppointment
            {
                Id = message.Patient.Id,
                Patient_Name = message.Patient.Patient_Name,
                Number_Phone = message.Patient.Number_Phone
            };

            // Создание аккаунта
            await _patientService.CreatePatient(patient);
        }

        public async Task Consume(ConsumeContext<UpdatePatient> context)
        {
            var message = context.Message;

            var patient = new PatientAppointment
            {
                Id = message.Patient.Id,
                Patient_Name = message.Patient.Patient_Name,
                Number_Phone = message.Patient.Number_Phone
            };

            // Обновление информации о пациенте
            await _patientService.UpdatePatient(patient);
        }

        public async Task Consume(ConsumeContext<DeletePatient> context)
        {
            var message = context.Message;

            // Удаление пациента
            await _patientService.DeletePatient(message.Id);
        }
    }
}
