using Xunit;
using Moq;
using FluentAssertions;
using HospitalCheckupSystem.Application.UseCases;
using HospitalCheckupSystem.Domain.Entities;
using HospitalCheckupSystem.Domain.Interfaces;

namespace HospitalCheckupSystem.Tests.Application;

public class RecordAnamnesisTests
{
    [Fact]
    public async Task Should_Record_Anamnesis_To_Checkup()
    {
        var checkupId = Guid.NewGuid();

        var checkup = new MedicalCheckup(
            Guid.NewGuid(),
            "MCU-2026-000001",
            DateTime.Today);

        var repo = new Mock<IMedicalCheckupRepository>();
        repo.Setup(r => r.GetByIdAsync(checkupId))
            .ReturnsAsync(checkup);

        var useCase = new RecordAnamnesisUseCase(repo.Object);

        await useCase.ExecuteAsync(new RecordAnamnesisCommand 
        {
            CheckupId = checkupId,
            Complaints = "Headache",
            PastIllness = "Hypertension",
            FamilyHistory = "Diabetes",
            Allergies = "Seafood",
            Smoking = true,
            Alcohol = false,
            WorkHazards = "Noise"
        });

        checkup.Anamnesis.Should().NotBeNull();
        checkup.Anamnesis!.Complaints.Should().Be("Headache");

        repo.Verify(r => r.UpdateAsync(checkup), Times.Once);
    }
}
