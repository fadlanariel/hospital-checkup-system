using FluentAssertions;
using HospitalCheckupSystem.Application.UseCases;
using HospitalCheckupSystem.Application.Validators;
using HospitalCheckupSystem.Domain.Entities;
using HospitalCheckupSystem.Domain.Enums;
using HospitalCheckupSystem.Domain.Interfaces;
using Moq;
using Xunit;

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

        var validator = new MakeDoctorConclusionCommandValidator();

        var useCase = new MakeDoctorConclusionUseCase(repo.Object, validator);

        await useCase.ExecuteAsync(new MakeDoctorConclusionCommand
        {
            CheckupId = checkupId,
            FitnessStatus = FitnessStatus.Fit,
            Diagnosis = "Healthy",
            Recommendation = "OK"
        });

        checkup.IsFinished.Should().BeTrue();
        repo.Verify(r => r.UpdateAsync(checkup), Times.Once);
    }

    [Fact]
    public async Task Should_Reject_Empty_Diagnosis()
    {
        var validator = new MakeDoctorConclusionCommandValidator();

        var result = await validator.ValidateAsync(
            new MakeDoctorConclusionCommand
            {
                CheckupId = Guid.NewGuid(),
                FitnessStatus = FitnessStatus.Fit,
                Diagnosis = "",
                Recommendation = "OK"
            });

        result.IsValid.Should().BeFalse();
    }
}