using FluentAssertions;
using HospitalCheckupSystem.Application.UseCases;
using HospitalCheckupSystem.Domain.Entities;
using HospitalCheckupSystem.Domain.Interfaces;
using HospitalCheckupSystem.Tests.Helpers;
using Moq;
using Xunit;

namespace HospitalCheckupSystem.Tests.Application;

public class GenerateCheckupReportTests
{
    [Fact]
    public async Task Should_Generate_Report_When_Checkup_Finished()
    {
        var checkup = CompletedCheckupFactory.CreateCompleted();

        var repoMock = new Mock<IMedicalCheckupRepository>();
        repoMock
            .Setup(r => r.GetByIdAsync(checkup.Id))
            .ReturnsAsync(checkup);

        var useCase = new GenerateCheckupReportUseCase(repoMock.Object);

        var result = await useCase.Execute(checkup.Id);

        result.McuNumber.Should().Be(checkup.McuNumber);
        result.PatientId.Should().Be(checkup.PatientId);
        result.Conclusion!.Diagnosis.Should().NotBeNull();
    }

    [Fact]
    public async Task Should_Throw_When_Checkup_Not_Finished()
    {
        var checkup = new MedicalCheckup(
            Guid.NewGuid(),
            "MCU-2026=000001",
            DateTime.Today
        );

        var repoMock = new Mock<IMedicalCheckupRepository>();
        repoMock
            .Setup(r => r.GetByIdAsync(checkup.Id))
            .ReturnsAsync(checkup);

        var useCase = new GenerateCheckupReportUseCase(repoMock.Object);

        var act = () => useCase.Execute(checkup.Id);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Checkup not finished");
    }

    [Fact]
    public async Task Should_Include_Patient_Data_In_Report()
    {
        var patientId = Guid.NewGuid();

        var checkup = CompletedCheckupFactory.CreateCompleted(patientId);

        var patient = new Patient(
            patientId,
            "JANE DOE",
            "Perempuan",
            new DateTime(1974, 8, 2)
        );

        var checkupRepoMock = new Mock<IMedicalCheckupRepository>();
        checkupRepoMock
            .Setup(r => r.GetByIdAsync(checkup.Id))
            .ReturnsAsync(checkup);

        var patientRepoMock = new Mock<IPatientRepository>();
        patientRepoMock
            .Setup(r => r.GetByIdAsync(patientId))
            .ReturnsAsync(patient);

        var useCase = new GenerateCheckupReportUseCase(
            checkupRepoMock.Object,
            patientRepoMock.Object
        );

        var result = await useCase.Execute(checkup.Id);

        result.PatientName.Should().Be("JANE DOE");
        result.Gender.Should().Be("Perempuan");
        result.DateOfBirth.Should().Be(new DateTime(1974, 8, 2));
    }

    [Fact]
    public async Task Should_Throw_When_Patient_Not_Found()
    {
        var checkup = CompletedCheckupFactory.CreateCompleted();

        var checkupRepoMock = new Mock<IMedicalCheckupRepository>();
        checkupRepoMock
            .Setup(r => r.GetByIdAsync(checkup.Id))
            .ReturnsAsync(checkup);

        var patientRepoMock = new Mock<IPatientRepository>();
        patientRepoMock
            .Setup(r => r.GetByIdAsync(checkup.PatientId))
            .ReturnsAsync((Patient?)null);

        var useCase = new GenerateCheckupReportUseCase(
            checkupRepoMock.Object,
            patientRepoMock.Object
        );

        var act = () => useCase.Execute(checkup.Id);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Patient not found");
    }

}
