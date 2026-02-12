using Xunit;
using FluentAssertions;
using HospitalCheckupSystem.Domain.Interfaces;

namespace HospitalCheckupSystem.Tests.Domain;

public class MrnGeneratorTests
{
    [Fact]
    public void Generate_ShouldReturnCorrectFormat()
    {
        IMrnGenerator generator = new HospitalCheckupSystem.Domain.Services.MrnGenerator();

        var mrn = generator.Generate(1);

        mrn.Should().StartWith("MRN-");
        mrn.Should().Contain(DateTime.UtcNow.Year.ToString());
        mrn.Should().EndWith("000001");
    }

    [Fact]
    public void Generate_ShouldPadSequenceToSixDigits()
    {
        var gen = new HospitalCheckupSystem.Domain.Services.MrnGenerator();
        gen.Generate(29).Should().EndWith("000029");
    }

    [Fact]
    public void Generate_ShouldHandleLargeSequence()
    {
        var gen = new HospitalCheckupSystem.Domain.Services.MrnGenerator();
        gen.Generate(123456).Should().EndWith("123456");
    }
}
