using Xunit;
using Moq;
using FluentAssertions;
using HospitalCheckupSystem.Application.UseCases;
using HospitalCheckupSystem.Domain.Entities;
using HospitalCheckupSystem.Domain.Interfaces;

namespace HospitalCheckupSystem.Tests.Application;

public class MakeDoctorConclusionTests
{
    [Fact]
    public void Should_Set_Doctor_Conclusion_With_Fitness_Status()
    {
        var checkup = CompletedCheckupFactory.CreateReadyForConclusion();

        checkup.MakeConclusion(
            FitnessStatus.Fit,
            "Healthy",
            "Maintain healthy lifestyle"
        );

        checkup.IsFinished.Should().BeTrue();
        checkup.Conclusion!.FitnessStatus.Should().Be(FitnessStatus.Fit);
    }
}
