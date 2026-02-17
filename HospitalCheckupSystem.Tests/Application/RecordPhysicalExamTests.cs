using Xunit;
using Moq;
using FluentAssertions;
using HospitalCheckupSystem.Application.UseCases;
using HospitalCheckupSystem.Domain.Entities;
using HospitalCheckupSystem.Domain.Interfaces;

namespace HospitalCheckupSystem.Tests.Application;

public class RecordPhysicalExamTests
{
    [Fact]
    public async Task Should_Record_Physical_Exam_To_Checkup()
    {
        var checkupId = Guid.NewGuid();

        var checkup = new MedicalCheckup(
            Guid.NewGuid(),
            "MCU-2026-000001",
            DateTime.Today);

        var repo = new Mock<IMedicalCheckupRepository>();
        repo.Setup(r => r.GetByIdAsync(checkupId))
            .ReturnsAsync(checkup);

        var useCase = new RecordPhysicalExamUseCase(repo.Object);

        await useCase.ExecuteAsync(new RecordPhysicalExamCommand 
        {
            CheckupId = checkupId,
            GeneralAppearance = "Good",
            Eyes = "Normal",
            ENT = "Normal",
            Heart = "Normal",
            Lungs = "Clear",
            Abdomen = "Soft",
            Neurology = "Normal"
        });

        checkup.PhysicalExam.Should().NotBeNull();
        checkup.PhysicalExam!.Heart.Should().Be("Normal");

        repo.Verify(r => r.UpdateAsync(checkup), Times.Once);
    }
}
