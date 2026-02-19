using Xunit;
using Moq;
using FluentAssertions;
using HospitalCheckupSystem.Application.UseCases;
using HospitalCheckupSystem.Domain.Entities;
using HospitalCheckupSystem.Domain.Interfaces;

namespace HospitalCheckupSystem.Tests.Application;

public class GetPatientCheckupsTests
{
    [Fact]
    public async Task Should_Return_Checkups_For_Patient()
    {
        var patientId = Guid.NewGuid();

        var checkups = new List<MedicalCheckup>
        {
            new MedicalCheckup(patientId, "MCU-2026-000001", DateTime.Today.AddDays(-1)),
            new MedicalCheckup(patientId, "MCU-2026-000002", DateTime.Today)
        };

        var repoMock = new Mock<IMedicalCheckupRepository>();
        repoMock.Setup(r => r.GetByPatientIdAsync(patientId))
            .ReturnsAsync(checkups);

        var useCase = new GetPatientCheckupsUseCase(repoMock.Object);

        var result = await useCase.ExecuteAsync(patientId);

        result.Should().HaveCount(2);
        result.First().McuNumber.Should().Be("MCU-2026-000002");
    }

    [Fact]
    public async Task Should_Return_Empty_List_When_No_Checkups()
    {
        var patientId = Guid.NewGuid();

        var repoMock = new Mock<IMedicalCheckupRepository>();
        repoMock
            .Setup(r => r.GetByPatientIdAsync(patientId))
            .ReturnsAsync(new List<MedicalCheckup>());

        var useCase = new GetPatientCheckupsUseCase(repoMock.Object);

        var result = await useCase.ExecuteAsync(patientId);

        result.Should().BeEmpty();
    }
}
