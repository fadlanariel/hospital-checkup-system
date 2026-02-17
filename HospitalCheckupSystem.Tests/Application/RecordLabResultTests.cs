using Xunit;
using Moq;
using FluentAssertions;
using HospitalCheckupSystem.Application.UseCases;
using HospitalCheckupSystem.Domain.Entities;
using HospitalCheckupSystem.Domain.Interfaces;

namespace HospitalCheckupSystem.Tests.Application;

public class RecordLabResultTests
{
    [Fact]
    public async Task Should_Add_Lab_Result_Item_To_Checkup()
    {
        var checkupId = Guid.NewGuid();

        var checkup = new MedicalCheckup(
            Guid.NewGuid(),
            "MCU-2026-000001",
            DateTime.Today);

        var repo = new Mock<IMedicalCheckupRepository>();
        repo.Setup(r => r.GetByIdAsync(checkupId))
            .ReturnsAsync(checkup);

        var useCase = new RecordLabResultUseCase(repo.Object);

        await useCase.ExecuteAsync(new RecordLabResultCommand
        {
            CheckupId = checkupId,
            TestName = "Hemoglobin",
            Unit = "g/dL",
            Value = "13.5",
            NormalMin = 13,
            NormalMax = 17
        });

        checkup.LabResults.Should().HaveCount(1);
        checkup.LabResults.First().IsNormal.Should().BeTrue();

        repo.Verify(r => r.UpdateAsync(checkup), Times.Once);
    }
}
