using Xunit;
using Moq;
using FluentAssertions;
using HospitalCheckupSystem.Application.UseCases;
using HospitalCheckupSystem.Domain.Entities;
using HospitalCheckupSystem.Domain.Interfaces;
using HospitalCheckupSystem.Tests.Fakes;

namespace HospitalCheckupSystem.Tests.Application;

public class StartMedicalCheckupTests
{
    [Fact]
    public async Task Execute_ShouldCreateCheckup_WithYearlyMcuNumber()
    {
        var patientId = Guid.NewGuid();

        var repo = new Mock<IMedicalCheckupRepository>();
        repo.Setup(r => r.GetNextSequenceAsync(2026))
            .ReturnsAsync(1);

        var generator = new FakeMcuNumberGenerator();

        var useCase = new StartMedicalCheckupUseCase(
            repo.Object,
            generator);

        var result = await useCase.ExecuteAsync(patientId, new DateTime(2026, 5, 1));

        result.McuNumber.Should().Be("MCU-2026-000001");

        repo.Verify(r => r.AddAsync(It.IsAny<MedicalCheckup>()), Times.Once);
    }
}
