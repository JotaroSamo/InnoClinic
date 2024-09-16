using CSharpFunctionalExtensions;
using Office_API.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Office_API.Domain.Model
{
    public class Office
    {
        public Guid Id { get; set; }

        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string HouseNumber { get; set; } = string.Empty;
        public string OfficeNumber { get; set; } = string.Empty;
        public string Address { get => $"City: {City}, Street: {Street}, HouseNumber: {HouseNumber}, OfficeNumber: {OfficeNumber}, Phone: {RegistryPhoneNumber}"; }

        public Guid? PhotoId { get; set; }
        public string? PhotoUrl { get; set; } = string.Empty;
        public string RegistryPhoneNumber { get; set; } = string.Empty;
        public Status IsActive { get; set; }
        public Office()
        {
            
        }
        private Office(Guid id, string city, string street, 
            string houseNumber, string officeNumber,
            string registryPhoneNumber, Status isActive, Guid photoId = default, string photoUrl = default)
        {
            Id = id;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            OfficeNumber = officeNumber;
            RegistryPhoneNumber = registryPhoneNumber;
            IsActive = isActive;
            PhotoId = photoId;
            PhotoUrl = photoUrl;
        }

        public static Result<Office> Create(
    Guid id,
    string city,
    string street,
    string houseNumber,
    string officeNumber,
    string registryPhoneNumber,
    Status isActive,
    Guid photoId = default,
    string photoUrl = default)
        {
            // Проверка на валидность ID
            if (id == Guid.Empty)
            {
                return Result.Failure<Office>("ID cannot be empty.");
            }

            // Проверка на валидность города
            if (string.IsNullOrWhiteSpace(city))
            {
                return Result.Failure<Office>("City cannot be empty.");
            }

            // Проверка на валидность улицы
            if (string.IsNullOrWhiteSpace(street))
            {
                return Result.Failure<Office>("Street cannot be empty.");
            }

            // Проверка на валидность номера дома
            if (string.IsNullOrWhiteSpace(houseNumber))
            {
                return Result.Failure<Office>("House number cannot be empty.");
            }

            // Проверка на валидность номера офиса
            if (string.IsNullOrWhiteSpace(officeNumber))
            {
                return Result.Failure<Office>("Office number cannot be empty.");
            }

            // Проверка на валидность регистрационного телефонного номера
            if (string.IsNullOrWhiteSpace(registryPhoneNumber))
            {
                return Result.Failure<Office>("Registry phone number cannot be empty.");
            }
            // Создание нового объекта Office
            var office = new Office(id, city, street, houseNumber, officeNumber, registryPhoneNumber,isActive ,photoId, photoUrl);

       
            return Result.Success<Office>(office);
        }

    }
}
