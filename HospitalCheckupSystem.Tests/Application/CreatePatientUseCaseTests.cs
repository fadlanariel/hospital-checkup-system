using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using HospitalCheckupSystem.Application.DTOs;
using HospitalCheckupSystem.Application.UseCases;
using HospitalCheckupSystem.Application.Validators;
using HospitalCheckupSystem.Domain.Entities;
using HospitalCheckupSystem.Domain.Interfaces;
using Moq;
using Xunit;

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
        var validator = new CreatePatientRequestValidator();

        var useCase = new CreatePatientUseCase(
            repoMock.Object,
            mrnGen,
            validator);


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

    [Fact]
    public async Task Execute_ShouldThrowValidationException_WhenValidatorFails()
    {
        // arrange
        var repo = new Mock<IPatientRepository>();
        var mrn = new FakeMrnGenerator();

        var validatorMock = new Mock<IValidator<CreatePatientRequest>>();
        validatorMock
            .Setup(v => v.ValidateAsync(It.IsAny<CreatePatientRequest>(), default))
            .ReturnsAsync(new ValidationResult(new[]
            {
            new ValidationFailure("Name", "Required")
            }));

        var useCase = new CreatePatientUseCase(
            repo.Object,
            mrn,
            validatorMock.Object);

        // act
        var act = () => useCase.ExecuteAsync(new CreatePatientRequest());

        // assert
        await Assert.ThrowsAsync<ValidationException>(act);

        repo.Verify(r => r.AddAsync(It.IsAny<Patient>()), Times.Never);
    }

}

class FakeMrnGenerator : IMrnGenerator
{
    public string Generate(int sequence)
        => $"MRN-TEST-{sequence:D6}";
}