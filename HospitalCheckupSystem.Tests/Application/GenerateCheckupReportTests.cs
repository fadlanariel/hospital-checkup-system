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
        var patient = new Patient(
            "MRN-2026-000001",
            "JANE DOE",
            new DateTime(1974, 8, 2),
            "Perempuan",
            "08123456789",
            "Jakarta",
            "BPJS"
        );

        var checkup = CompletedCheckupFactory.CreateCompleted(patient.Id);

        var repoMock = new Mock<IMedicalCheckupRepository>();
        repoMock
            .Setup(r => r.GetByIdAsync(checkup.Id))
            .ReturnsAsync(checkup);

        var patientRepoMock = new Mock<IPatientRepository>();
        patientRepoMock
            .Setup(r => r.GetByIdAsync(checkup.PatientId))
            .ReturnsAsync(patient);

        var useCase = new GenerateCheckupReportUseCase(repoMock.Object, patientRepoMock.Object);

        var result = await useCase.Execute(checkup.Id);

        result.McuNumber.Should().Be(checkup.McuNumber);
        result.PatientId.Should().Be(checkup.PatientId);
        result.PatientMrn.Should().Be("MRN-2026-000001");
        result.PatientName.Should().Be("JANE DOE");
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
        var patientRepoMock = new Mock<IPatientRepository>();
        repoMock
            .Setup(r => r.GetByIdAsync(checkup.Id))
            .ReturnsAsync(checkup);

        var useCase = new GenerateCheckupReportUseCase(repoMock.Object, patientRepoMock.Object);

        var act = () => useCase.Execute(checkup.Id);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Checkup not finished");
    }

    [Fact]
    public async Task Should_Include_Patient_Data_In_Report()
    {
        var patient = new Patient(
            "MRN-2026-000001",
            "JANE DOE",
            new DateTime(1974, 8, 2),
            "Perempuan",
            "08123456789",
            "Jakarta",
            "BPJS"
        );
     
        var checkup = CompletedCheckupFactory.CreateCompleted(patient.Id);

        var checkupRepoMock = new Mock<IMedicalCheckupRepository>();
        checkupRepoMock
            .Setup(r => r.GetByIdAsync(checkup.Id))
            .ReturnsAsync(checkup);

        var patientRepoMock = new Mock<IPatientRepository>();
        patientRepoMock
            .Setup(r => r.GetByIdAsync(patient.Id))
            .ReturnsAsync(patient);

        var useCase = new GenerateCheckupReportUseCase(
            checkupRepoMock.Object,
            patientRepoMock.Object
        );

        var result = await useCase.Execute(checkup.Id);

        result.PatientMrn.Should().Be("MRN-2026-000001");
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
