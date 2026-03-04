using Xunit;
using Moq;
using FluentAssertions;
using HospitalCheckupSystem.Application.UseCases;
using HospitalCheckupSystem.Domain.Entities;
using HospitalCheckupSystem.Domain.Interfaces;

namespace HospitalCheckupSystem.Tests.Application;

public class RadiologyResultTests
{
    [Fact]
    public async Task Should_Save_Radiology_Result()
    {
        var checkupId = Guid.NewGuid();

        var checkup = new MedicalCheckup(
            Guid.NewGuid(),
            "MCU-2026-000003",
            DateTime.Today
        );

        var repoMock = new Mock<IMedicalCheckupRepository>();
        repoMock.Setup(r => r.GetByIdAsync(checkupId))
                .ReturnsAsync(checkup);

        var useCase = new RecordRadiologyUseCase(repoMock.Object);

        await useCase.ExecuteAsync(new RecordRadiologyCommand
        {
            CheckupId = checkupId,
            Examination = "Thorax",
            Findings = "CTR > 50%",
            Impression = "Cardiomegaly",
            RadiologistName = "dr. Aswin",
            ExamDate = DateTime.Today
        });

        checkup.Radiology.Should().NotBeNull();
        repoMock.Verify(r => r.UpdateAsync(checkup), Times.Once);
    }
}
