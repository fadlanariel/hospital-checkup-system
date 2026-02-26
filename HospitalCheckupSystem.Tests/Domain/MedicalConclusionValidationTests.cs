using FluentAssertions;
using HospitalCheckupSystem.Domain.Entities;
using HospitalCheckupSystem.Domain.Enums;
using HospitalCheckupSystem.Domain.Interfaces;
using HospitalCheckupSystem.Tests.Helpers;
using Moq;
using Xunit;

namespace HospitalCheckupSystem.Tests.Domain;

public class MedicalConclusionValidationTests
{
    [Fact]
    public void Should_Not_Allow_Conclusion_Without_Lab_Results()
    {
        var checkup = new MedicalCheckup(
            Guid.NewGuid(),
            "MCU-2026-000001",
            DateTime.Today
        );

        checkup.RecordVitals(170, 70, 120, 80, 72);
        checkup.RecordAnamnesis("OK", "-", "-", "-", false, false, "-");
        checkup.RecordPhysicalExam("Good", "Normal", "Normal", "Normal", "Clear", "Soft", "Normal");

        var act = () => checkup.MakeConclusion(
            FitnessStatus.Fit,
            "Healthy",
            "OK"
        );

        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Should_Not_Allow_Adding_Lab_After_Checkup_Finished()
    {
        var checkup = CompletedCheckupFactory.CreateCompleted();

        var act = () => checkup.AddLabResult(
            "Cholesterol",
            "mg/dL",
            "200",
            0,
            200
        );

        act.Should().Throw<InvalidOperationException>();
    }
}
