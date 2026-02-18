using Xunit;
using Moq;
using FluentAssertions;
using HospitalCheckupSystem.Application.UseCases;
using HospitalCheckupSystem.Domain.Entities;
using HospitalCheckupSystem.Domain.Interfaces;

namespace HospitalCheckupSystem.Tests.Application;

public class MakeDoctorConclusionUseCaseTests
{
    [Fact]
    public async Task Should_Finalize_Checkup_And_Save()
    {
        var checkupId = Guid.NewGuid();

        var checkup = new MedicalCheckup(
            Guid.NewGuid(),
            "MCU-2026-000001",
            DateTime.Today
        );

        checkup.RecordVitals(170, 70, 120, 80, 72);
        checkup.RecordAnamnesis("Headache", "Hypertension", "Diabetes", "Seafood", true, false, "Noise");
        checkup.RecordPhysicalExam("Good", "Normal", "Normal", "Normal", "Clear", "Soft", "Normal");
        checkup.AddLabResult("Hemoglobin", "g/dL", "13.5", 13, 17);

        var repo = new Mock<IMedicalCheckupRepository>();
        repo.Setup(r => r.GetByIdAsync(checkupId))
            .ReturnsAsync(checkup);

        var useCase = new MakeDoctorConclusionUseCase(repo.Object);

        await useCase.ExecuteAsync(new MakeDoctorConclusionCommand
        {
            CheckupId = checkupId,
            Fit = true,
            Diagnosis = "Healthy",
            Recommendation = "OK"
        });

        checkup.IsFinished.Should().BeTrue();
        repo.Verify(r => r.UpdateAsync(checkup), Times.Once);
    }
}