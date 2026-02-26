using FluentAssertions;
using HospitalCheckupSystem.Domain.Entities;
using HospitalCheckupSystem.Domain.Interfaces;
using Moq;
using Xunit;

namespace HospitalCheckupSystem.Tests.Domain;

public class RecordVitalsValidationTests
{
    [Fact]
    public void Should_Not_Allow_Invalid_Blood_Pressure()
    {
        var checkup = new MedicalCheckup(
            Guid.NewGuid(),
            "MCU-2026-000001",
            DateTime.Today
        );

        var act = () => checkup.RecordVitals(
            height: 170,
            weight: 70,
            systolic: 70,
            diastolic: 120,
            pulse: 72
        );

        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Should_Not_Allow_Non_Positive_Height()
    {
        var checkup = new MedicalCheckup(
            Guid.NewGuid(),
            "MCU-2026-000001",
            DateTime.Today
        );

        var act = () => checkup.RecordVitals(
            height: 0,
            weight: 70,
            systolic: 120,
            diastolic: 80,
            pulse: 72
        );

        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Should_Not_Allow_Non_Positive_Weight()
    {
        var checkup = new MedicalCheckup(
            Guid.NewGuid(),
            "MCU-2026-000001",
            DateTime.Today
        );

        var act = () => checkup.RecordVitals(
            height: 170,
            weight: 0,
            systolic: 120,
            diastolic: 80,
            pulse: 72
        );

        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Should_Not_Allow_Non_Positive_Pulse()
    {
        var checkup = new MedicalCheckup(
            Guid.NewGuid(),
            "MCU-2026-000001",
            DateTime.Today
        );

        var act = () => checkup.RecordVitals(
            height: 170,
            weight: 70,
            systolic: 120,
            diastolic: 80,
            pulse: 0
        );

        act.Should().Throw<InvalidOperationException>();
    }
}
