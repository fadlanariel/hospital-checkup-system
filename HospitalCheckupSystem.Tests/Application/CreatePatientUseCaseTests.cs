using Xunit;
using Moq;
using FluentAssertions;
using HospitalCheckupSystem.Application.DTOs;
using HospitalCheckupSystem.Domain.Entities;
using HospitalCheckupSystem.Domain.Interfaces;
using HospitalCheckupSystem.Application.UseCases;

namespace HospitalCheckupSystem.Tests.Application;

public class CreatePatientUseCaseTests
{
    [Fact]
    public async Task Execute_ShouldCreatePatient_WithGeneratedMrn()
    {
        var repoMock = new Mock<IPatientRepository>();
        repoMock.Setup(r => r.GetNextSequenceAsync()).ReturnsAsync(1);

        Patient? savedPatient = null;

        repoMock.Setup(r => r.AddAsync(It.IsAny<Patient>()))
            .Callback<Patient>(p => savedPatient = p)
            .Returns(Task.CompletedTask);

        var mrnGen = new FakeMrnGenerator();

        var useCase = new CreatePatientUseCase(
            repoMock.Object,
            mrnGen);

        var request = new CreatePatientRequest
        {
            Name = "John Doe",
            Dob = new DateTime(1990, 1, 1),
            Gender = "M",
            Phone = "123",
            Address = "Street"
        };

        var result = await useCase.ExecuteAsync(request);

        result.Mrn.Should().StartWith("MRN-");
        savedPatient.Should().NotBeNull();
        savedPatient.Name.Should().Be("John Doe");
    }
}

class FakeMrnGenerator : IMrnGenerator
{
    public string Generate(int sequence)
        => $"MRN-TEST-{sequence:D6}";
}