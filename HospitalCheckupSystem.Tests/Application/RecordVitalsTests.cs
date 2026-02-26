using Xunit;
using Moq;
using FluentAssertions;
using HospitalCheckupSystem.Application.UseCases;
using HospitalCheckupSystem.Domain.Entities;
using HospitalCheckupSystem.Domain.Interfaces;

namespace HospitalCheckupSystem.Tests.Application;

public class RecordVitalsTests
{
    [Fact]
    public async Task Execute_ShouldRecordVitalSignsToCheckup()
    {
        var checkupId = Guid.NewGuid();

        var checkup = new MedicalCheckup(
            Guid.NewGuid(),
            "MCU-2026-000001",
            DateTime.Today);

        var repo = new Mock<IMedicalCheckupRepository>();
        repo.Setup(r => r.GetByIdAsync(checkupId))
            .ReturnsAsync(checkup);

        var useCase = new RecordVitalsUseCase(repo.Object);

        await useCase.ExecuteAsync(new RecordVitalsCommand
        {
            CheckupId = checkupId,
            Height = 170,
            Weight = 70,
            Systolic = 120,
            Diastolic = 80,
            Pulse = 72
        });

        checkup.Vitals.Should().NotBeNull();
        checkup.Vitals!.BMI.Should().BeInRange(24.1m, 24.3m);

        repo.Verify(r => r.UpdateAsync(checkup), Times.Once);
    }

    [Fact]
    public void Should_Not_Allow_Invalid_Blood_Pressure()
    {
        var checkupId = Guid.NewGuid();

        var checkup = new MedicalCheckup(
            Guid.NewGuid(),
            "MCU-2026-000001",
            DateTime.Today
        );

        var repo = new Mock<IMedicalCheckupRepository>();
        repo.Setup(r => r.GetByIdAsync(checkupId))
            .ReturnsAsync(checkup);

        var act = () => checkup.RecordVitals(
            height: 170,
            weight: 70,
            systolic: 70,
            diastolic: 120,   // invalid
            pulse: 72
        );

        act.Should().Throw<InvalidOperationException>();
    }
}
