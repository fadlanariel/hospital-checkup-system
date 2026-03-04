using FluentAssertions;
using HospitalCheckupSystem.Domain.Entities;

namespace HospitalCheckupSystem.Tests.Domain;

public class EkgResultDomainTests
{
    [Fact]
    public void Should_Record_Ekg_Result()
    {
        var checkup = new MedicalCheckup(
            Guid.NewGuid(),
            "MCU-2026-000010",
            DateTime.Today
        );

        checkup.RecordEkg(
            rhythm: "Sinus Rhythm",
            heartRate: 72,
            axis: "Normal",
            impression: "Normal ECG",
            doctorName: "dr. Budi, SpJP",
            examDate: DateTime.Today
        );

        checkup.Ekg.Should().NotBeNull();
        checkup.Ekg!.Impression.Should().Be("Normal ECG");
    }
}
