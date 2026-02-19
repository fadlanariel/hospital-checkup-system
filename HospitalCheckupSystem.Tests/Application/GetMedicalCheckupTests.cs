using Xunit;
using Moq;
using FluentAssertions;
using HospitalCheckupSystem.Application.UseCases;
using HospitalCheckupSystem.Domain.Entities;
using HospitalCheckupSystem.Domain.Interfaces;

namespace HospitalCheckupSystem.Tests.Application;

public class GetMedicalCheckupTests
{
    [Fact]
    public async Task Should_Return_Checkup_When_Exists()
    {
        var checkupId = Guid.NewGuid();

        var checkup = new MedicalCheckup(
            Guid.NewGuid(),
            "MCU-2026-000001",
            DateTime.Today
        );

        var repositoryMock = new Mock<IMedicalCheckupRepository>();
        repositoryMock
            .Setup(r => r.GetByIdAsync(checkupId))
            .ReturnsAsync(checkup);

        var useCase = new GetMedicalCheckupUseCase(repositoryMock.Object);

        var result = await useCase.Execute(checkupId);

        result.Should().NotBeNull();
        result.Id.Should().Be(checkup.Id);
        result.McuNumber.Should().Be("MCU-2026-000001");
    }

    [Fact]
    public async Task Should_Throw_When_Checkup_Not_Found()
    {
        var checkupId = Guid.NewGuid();

        var repositoryMock = new Mock<IMedicalCheckupRepository>();
        repositoryMock
            .Setup(r => r.GetByIdAsync(checkupId))
            .ReturnsAsync((MedicalCheckup?)null);

        var useCase = new GetMedicalCheckupUseCase(repositoryMock.Object);

        var act = () => useCase.Execute(checkupId);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Checkup not found");
    }
}