using FluentAssertions;
using HospitalCheckupSystem.Application.UseCases;
using HospitalCheckupSystem.Domain.Entities;
using HospitalCheckupSystem.Domain.Interfaces;
using Moq;

namespace HospitalCheckupSystem.Tests.Application;

public class RecordEkgUseCaseTests
{
    [Fact]
    public async Task Should_Save_Ekg_Result()
    {
        var checkupId = Guid.NewGuid();

        var checkup = new MedicalCheckup(
            Guid.NewGuid(),
            "MCU-2026-000011",
            DateTime.Today
        );

        var repoMock = new Mock<IMedicalCheckupRepository>();
        repoMock.Setup(r => r.GetByIdAsync(checkupId))
                .ReturnsAsync(checkup);

        var useCase = new RecordEkgUseCase(repoMock.Object);

        await useCase.ExecuteAsync(new RecordEkgCommand
        {
            CheckupId = checkupId,
            Rhythm = "Sinus Rhythm",
            HeartRate = 72,
            Axis = "Normal",
            Impression = "Normal ECG",
            DoctorName = "dr. Budi",
            ExamDate = DateTime.Today
        });

        checkup.Ekg.Should().NotBeNull();
        repoMock.Verify(r => r.UpdateAsync(checkup), Times.Once);
    }
}
