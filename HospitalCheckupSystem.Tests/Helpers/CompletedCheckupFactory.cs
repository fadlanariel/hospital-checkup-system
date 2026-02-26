using HospitalCheckupSystem.Domain.Entities;
using HospitalCheckupSystem.Domain.Enums;

namespace HospitalCheckupSystem.Tests.Helpers;

public static class CompletedCheckupFactory
{
    public static MedicalCheckup CreateCompleted(Guid? patientId = null)
    {
        var actualPatientId = patientId ?? Guid.NewGuid();

        var checkup = new MedicalCheckup(
            actualPatientId,
            "MCU-2026-000001",
            DateTime.Today
        );

        checkup.RecordVitals(170, 70, 120, 80, 72);
        checkup.RecordAnamnesis("Headache", "Hypertension", "Diabetes", "Seafood", true, false, "Noise");
        checkup.RecordPhysicalExam("Good", "Normal", "Normal", "Normal", "Clear", "Soft", "Normal");
        checkup.AddLabResult("Hemoglobin", "g/dL", "13.5", 13, 17);

        checkup.MakeConclusion(
            fitnessStatus: FitnessStatus.Fit,
            diagnosis: "Healthy",
            recommendation: "Maintain healthy lifestyle"
        );

        return checkup;
    }

    public static MedicalCheckup CreateCompletedNotFinished(Guid? patientId = null)
    {
        var actualPatientId = patientId ?? Guid.NewGuid();

        var checkup = new MedicalCheckup(
            actualPatientId,
            "MCU-2026-000001",
            DateTime.Today
        );

        checkup.RecordVitals(170, 70, 120, 80, 72);
        checkup.RecordAnamnesis("Headache", "Hypertension", "Diabetes", "Seafood", true, false, "Noise");
        checkup.RecordPhysicalExam("Good", "Normal", "Normal", "Normal", "Clear", "Soft", "Normal");
        checkup.AddLabResult("Hemoglobin", "g/dL", "13.5", 13, 17);

        return checkup;
    }
}
