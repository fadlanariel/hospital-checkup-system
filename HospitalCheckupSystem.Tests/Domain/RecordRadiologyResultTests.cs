using FluentAssertions;
using HospitalCheckupSystem.Domain.Entities;

namespace HospitalCheckupSystem.Tests.Domain;

public class RecordRadiologyResultTests
{
    [Fact]
    public void Should_Record_Radiology_Result()
    {
        var checkup = new MedicalCheckup(
            Guid.NewGuid(),
            "MCU-2026-000002",
            DateTime.Today
        );

        checkup.RecordRadiology(
            examination: "Thorax",
            findings: "CTR > 50%, no infiltrate",
            impression: "Cardiomegaly",
            radiologistName: "dr. Aswin Surya Widjaja, SpRad",
            examDate: DateTime.Today
        );

        checkup.Radiology.Should().NotBeNull();
        checkup.Radiology!.Impression.Should().Be("Cardiomegaly");
    }
}
