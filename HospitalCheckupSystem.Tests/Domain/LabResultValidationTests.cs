using FluentAssertions;
using HospitalCheckupSystem.Domain.Entities;
using HospitalCheckupSystem.Domain.Enums;
using HospitalCheckupSystem.Domain.Interfaces;
using HospitalCheckupSystem.Tests.Helpers;
using Moq;
using Xunit;

namespace HospitalCheckupSystem.Tests.Domain;

public class LabResultValidationTests
{
    [Fact]
    public void Should_Not_Allow_Invalid_Lab_Normal_Range()
    {
        var checkup = CompletedCheckupFactory.CreateCompletedNotFinished();

        var act = () => checkup.AddLabResult(
            testName: "Hemoglobin",
            unit: "g/dL",
            value: "13.5",
            normalMin: 17,
            normalMax: 13 
        );

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("*normal*");
    }

    [Fact]
    public void Should_Not_Allow_Empty_Lab_Test_Name()
    {
        var checkup = CompletedCheckupFactory.CreateCompletedNotFinished();

        var act = () => checkup.AddLabResult(
            testName: "",
            unit: "g/dL",
            value: "13.5",
            normalMin: 13,
            normalMax: 17
        );

        act.Should().Throw<InvalidOperationException>();
    }
}
