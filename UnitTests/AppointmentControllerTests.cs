using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Appointment_API.Contract.Request.Create;
using Appointment_API.Contract.Request.Update;
using Appointment_API.Controllers;
using Appointment_API.DataAccess.IService;
using Appointment_API.Domain.Abstract.IService;
using Appointment_API.Domain.Model;
using CSharpFunctionalExtensions;
using FluentValidation;
using Global.Dto;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Xunit;

public class AppointmentControllerTests
{
    private readonly AppointmentController _controller;
    private readonly Mock<IAppointmentService> _appointmentServiceMock;
    private readonly Mock<IValidator<Appointment>> _appointmentValidatorMock;
    private readonly Mock<ILogger<AppointmentController>> _loggerMock;
    private readonly Mock<IEmailService> _emailServiceMock;

    public AppointmentControllerTests()
    {
        _appointmentServiceMock = new Mock<IAppointmentService>();
        _appointmentValidatorMock = new Mock<IValidator<Appointment>>();
        _loggerMock = new Mock<ILogger<AppointmentController>>();
        _emailServiceMock = new Mock<IEmailService>();

        _controller = new AppointmentController(
            _appointmentServiceMock.Object,
            _appointmentValidatorMock.Object,
            _loggerMock.Object,
            _emailServiceMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOk_WhenAppointmentsExist()
    {
        // Arrange
        var appointments = new List<Appointment>
        {
            new Appointment { Id = Guid.NewGuid(), Date = DateOnly.FromDateTime(DateTime.Now), Time = TimeOnly.FromDateTime(DateTime.Now) }
        };
        _appointmentServiceMock.Setup(service => service.GetAllAppointments())
            .ReturnsAsync(Result.Success(appointments));

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<Appointment>>(okResult.Value);
        Assert.Equal(appointments.Count, returnValue.Count);
    }

    [Fact]
    public async Task GetById_ReturnsOk_WhenAppointmentExists()
    {
        // Arrange
        var appointmentId = Guid.NewGuid();
        var appointment = new Appointment { Id = appointmentId, Date = DateOnly.FromDateTime(DateTime.Now), Time = TimeOnly.FromDateTime(DateTime.Now) };

        _appointmentServiceMock.Setup(service => service.GetByIdAppointment(appointmentId))
            .ReturnsAsync(Result.Success(appointment));

        // Act
        var result = await _controller.GetById(appointmentId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Appointment>(okResult.Value);
        Assert.Equal(appointmentId, returnValue.Id);
    }

    [Fact]
    public async Task Create_ReturnsOk_WhenAppointmentIsCreated()
    {
        // Arrange
        var request = new CreateAppointmentRequest
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
            Time = TimeOnly.FromDateTime(DateTime.Now),
            IsApproved = false,
            DoctorId = Guid.NewGuid(), // Замените на существующий ID
            PatientId = Guid.NewGuid()  // Замените на существующий ID
        };

        var appointment = new Appointment
        {
            Id = Guid.NewGuid(),
            Date = request.Date,
            Time = request.Time,
            IsApproved = request.IsApproved,
            DoctorId = request.DoctorId,
            PatientId = request.PatientId
        };

        _appointmentValidatorMock.Setup(v => v.ValidateAsync(It.IsAny<Appointment>(), default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _appointmentServiceMock.Setup(service => service.CreateAppointment(appointment))
            .ReturnsAsync(Result.Success(appointment));

        // Act
        var result = await _controller.Create(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Appointment>(okResult.Value);
        Assert.Equal(appointment.Id, returnValue.Id);
    }

    [Fact]
    public async Task Update_ReturnsOk_WhenAppointmentIsUpdated()
    {
        // Arrange
        var appointmentId = Guid.NewGuid();
        var request = new UpdateAppointmentRequest
        {
            Id = appointmentId,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Time = TimeOnly.FromDateTime(DateTime.Now),
            IsApproved = true,
            DoctorId = Guid.NewGuid(), 
            PatientId = Guid.NewGuid() 
        };

        var appointment = new Appointment
        {
            Id = appointmentId,
            Date = request.Date,
            Time = request.Time,
            IsApproved = request.IsApproved,
            DoctorId = request.DoctorId,
            PatientId = request.PatientId
        };

        _appointmentValidatorMock.Setup(v => v.ValidateAsync(It.IsAny<Appointment>(), default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _appointmentServiceMock.Setup(service => service.UpdateAppointment(appointment))
            .ReturnsAsync(Result.Success(appointment));

        // Act
        var result = await _controller.Update(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Appointment>(okResult.Value);
        Assert.Equal(appointmentId, returnValue.Id);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenAppointmentIsDeleted()
    {
        // Arrange
        var appointmentId = Guid.NewGuid();
        _appointmentServiceMock.Setup(service => service.Delete(appointmentId))
            .ReturnsAsync(Result.Success());

        // Act
        var result = await _controller.Delete(appointmentId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
