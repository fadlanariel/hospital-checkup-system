using FluentAssertions;
using HospitalCheckupSystem.Application.UseCases;
using HospitalCheckupSystem.Domain.Entities;
using HospitalCheckupSystem.Domain.Enums;
using HospitalCheckupSystem.Domain.Interfaces;
using HospitalCheckupSystem.Tests.Helpers;
using Moq;
using Xunit;

namespace HospitalCheckupSystem.Tests.Application;

public class MedicalCheckupConclusionTests
{
    [Fact]
    public void Should_Set_Doctor_Conclusion()
    {
        var checkup = new MedicalCheckup(
            Guid.NewGuid(),
            "MCU-2026-000001",
            DateTime.Today
        );

        checkup.RecordVitals(170, 70, 120, 80, 72);
        checkup.RecordAnamnesis("Headache", "Hypertension", "Diabetes", "Seafood", true, false, "Noise");
        checkup.RecordPhysicalExam("Good", "Normal", "Normal", "Normal", "Clear", "Soft", "Normal");
        checkup.AddLabResult("Hemoglobin", "g/dL", "13.5", 13, 17);

        checkup.MakeConclusion(
            FitnessStatus.Fit,
            "Healthy",
            "Maintain healthy lifestyle"
        );

        checkup.IsFinished.Should().BeTrue();
        checkup.Conclusion.Should().NotBeNull();
        checkup.Conclusion!.FitnessStatus.Should().Be(FitnessStatus.Fit);
        checkup.Conclusion.Diagnosis.Should().Be("Healthy");
    }

    [Fact]
    public void Should_Not_Allow_Conclusion_When_Data_Incomplete()
    {
        var checkup = new MedicalCheckup(
            Guid.NewGuid(),
            "MCU-2026-000001",
            DateTime.Today
        );

        var act = () => checkup.MakeConclusion(FitnessStatus.Fit, "Healthy", "OK");

        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Should_Not_Allow_Change_After_Finished()
    {
        var checkup = CompletedCheckupFactory.CreateCompleted();

        var act = () => checkup.MakeConclusion(FitnessStatus.Fit, "Changed", "Changed");

        act.Should().Throw<InvalidOperationException>();
    }
}
