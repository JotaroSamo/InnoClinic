using Appointment_API.Contract.Request.Create;
using Appointment_API.Contract.Request.Update;
using Appointment_API.DataAccess.IService;
using Appointment_API.Domain.Model;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Appointment_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IValidator<Appointment> _appointmentValidator;
        private readonly ILogger<AppointmentController> _logger; // Добавляем ILogger

        public AppointmentController(IAppointmentService appointmentService,
                                     IValidator<Appointment> appointmentValidator,
                                     ILogger<AppointmentController> logger) // Внедряем ILogger
        {
            _appointmentService = appointmentService;
            _appointmentValidator = appointmentValidator;
            _logger = logger; // Инициализируем ILogger
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all appointments."); // Логируем начало операции

            var result = await _appointmentService.GetAllAppointments();
            if (result.IsFailure)
            {
                _logger.LogError("Failed to retrieve appointments: {Error}", result.Error); // Логируем ошибку
                return BadRequest(result.Error);
            }

            _logger.LogInformation("Retrieved {Count} appointments.", result.Value.Count); // Логируем успешное завершение
            return Ok(result.Value);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("Getting appointment by ID: {Id}", id); // Логируем начало операции

            var result = await _appointmentService.GetByIdAppointment(id);
            if (result.IsFailure)
            {
                _logger.LogWarning("Appointment not found: {Id}. Error: {Error}", id, result.Error); // Логируем предупреждение
                return NotFound(result.Error);
            }

            _logger.LogInformation("Retrieved appointment: {Appointment}", result.Value); // Логируем успешное завершение
            return Ok(result.Value);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateAppointmentRequest request)
        {
            var appointment = new Appointment
            {
                Date = request.Date,
                Time = request.Time,
                IsApproved = request.IsApproved,
                DoctorId = request.DoctorId,
                PatientId = request.PatientId
            };

            var validationResult = await _appointmentValidator.ValidateAsync(appointment);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation failed for creating appointment: {Errors}", validationResult.Errors); // Логируем ошибку валидации
                return BadRequest(validationResult.Errors);
            }

            var result = await _appointmentService.CreateAppointment(appointment);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to create appointment: {Error}", result.Error); // Логируем ошибку
                return BadRequest(result.Error);
            }

            _logger.LogInformation("Created appointment: {Appointment}", result.Value); // Логируем успешное завершение
            return Ok(result.Value);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateAppointmentRequest request)
        {
            var appointment = new Appointment
            {
                Id = request.Id,
                Date = request.Date,
                Time = request.Time,
                IsApproved = request.IsApproved,
                DoctorId = request.DoctorId,
                PatientId = request.PatientId
            };

            var validationResult = await _appointmentValidator.ValidateAsync(appointment);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation failed for updating appointment: {Errors}", validationResult.Errors); // Логируем ошибку валидации
                return BadRequest(validationResult.Errors);
            }

            var result = await _appointmentService.UpdateAppointment(appointment);
            if (result.IsFailure)
            {
                _logger.LogWarning("Appointment not found for update: {Id}. Error: {Error}", request.Id, result.Error); // Логируем предупреждение
                return NotFound(result.Error);
            }

            _logger.LogInformation("Updated appointment: {Appointment}", result.Value); // Логируем успешное завершение
            return Ok(result.Value);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Deleting appointment with ID: {Id}", id); // Логируем начало операции

            var result = await _appointmentService.Delete(id);
            if (result.IsFailure)
            {
                _logger.LogWarning("Appointment not found for deletion: {Id}. Error: {Error}", id, result.Error); // Логируем предупреждение
                return NotFound(result.Error);
            }

            _logger.LogInformation("Deleted appointment with ID: {Id}", id); // Логируем успешное завершение
            return NoContent();
        }
    }

}
