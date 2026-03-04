using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using HospitalCheckupSystem.Application.DTOs;
using HospitalCheckupSystem.Application.Interfaces;

namespace HospitalCheckupSystem.Infrastructure.Services;

public class QuestPdfReportGenerator : IPdfReportGenerator
{
    public byte[] GenerateMedicalCheckupReport(MedicalCheckupReportDto report)
    {
        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);

                page.Header()
                    .PaddingBottom(15)
                    .Text("MEDICAL CHECKUP REPORT")
                    .SemiBold()
                    .FontSize(20)
                    .AlignCenter();


                page.Content().Column(col =>
                {
                    col.Spacing(15);

                    AddPatientSection(col, report);
                    AddVitalSection(col, report);
                    AddAnamnesisSection(col, report);
                    AddPhysicalExamSection(col, report);
                    AddRadiologySection(col, report);
                    AddEkgSection(col, report);
                    AddLabSection(col, report);
                    AddConclusionSection(col, report);
                });

                page.Footer()
                    .AlignRight()
                    .Text($"Generated on {DateTime.Now:dd MMM yyyy}");
            });
        }).GeneratePdf();
    }

    private void AddPatientSection(ColumnDescriptor col, MedicalCheckupReportDto report)
    {
        col.Item().Text("PATIENT INFORMATION").Bold().FontSize(14);

        col.Item().Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn();
                columns.RelativeColumn();
            });

            table.Cell().Text("MCU Number:");
            table.Cell().Text(report.McuNumber);

            table.Cell().Text("Medical Record Number:");
            table.Cell().Text(report.PatientMrn);

            table.Cell().Text("Patient Name:");
            table.Cell().Text(report.PatientName);

            table.Cell().Text("Gender:");
            table.Cell().Text(report.Gender);

            table.Cell().Text("Date of Birth:");
            table.Cell().Text(report.DateOfBirth.ToString("dd MMM yyyy"));
        });
    }

    private void AddVitalSection(ColumnDescriptor col, MedicalCheckupReportDto report)
    {
        if (report.Vitals == null) return;

        col.Item().PaddingTop(10);
        col.Item().Text("VITAL SIGNS").Bold().FontSize(14);

        var heightMeter = (double)report.Vitals.Height / 100.0;
        var bmi = heightMeter > 0
            ? (double)report.Vitals.Weight / (heightMeter * heightMeter)
            : 0;

        col.Item().Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn();
                columns.RelativeColumn();
            });

            table.Cell().Text("Height:");
            table.Cell().Text($"{report.Vitals.Height} cm");

            table.Cell().Text("Weight:");
            table.Cell().Text($"{report.Vitals.Weight} kg");

            table.Cell().Text("Blood Pressure:");
            table.Cell().Text($"{report.Vitals.Systolic}/{report.Vitals.Diastolic} mmHg");

            table.Cell().Text("Pulse:");
            table.Cell().Text($"{report.Vitals.Pulse} bpm");

            table.Cell().Text("BMI:");
            table.Cell().Text($"{bmi:F2}");
        });
    }

    private void AddAnamnesisSection(ColumnDescriptor col, MedicalCheckupReportDto report)
    {
        if (report.Anamnesis == null) return;

        col.Item().PaddingTop(10);
        col.Item().Text("MEDICAL HISTORY (ANAMNESIS)").Bold().FontSize(14);

        col.Item().PaddingBottom(-5).Text($"Complaints: {report.Anamnesis.Complaints}");
        col.Item().PaddingBottom(-5).Text($"Past Illness: {report.Anamnesis.PastIllness}");
        col.Item().PaddingBottom(-5).Text($"Family History: {report.Anamnesis.FamilyHistory}");
        col.Item().PaddingBottom(-5).Text($"Allergies: {report.Anamnesis.Allergies}");
        col.Item().PaddingBottom(-5).Text($"Smoking: {(report.Anamnesis.Smoking ? "Yes" : "No")}");
        col.Item().PaddingBottom(-5).Text($"Alcohol: {(report.Anamnesis.Alcohol ? "Yes" : "No")}");
        col.Item().PaddingBottom(-5).Text($"Work Hazards: {report.Anamnesis.WorkHazards}");
    }

    private void AddPhysicalExamSection(ColumnDescriptor col, MedicalCheckupReportDto report)
    {
        if (report.PhysicalExam == null) return;

        col.Item().PaddingTop(10);
        col.Item().Text("PHYSICAL EXAMINATION").Bold().FontSize(14);

        col.Item().PaddingBottom(-5).Text($"General Appearance: {report.PhysicalExam.GeneralAppearance}");
        col.Item().PaddingBottom(-5).Text($"Eyes: {report.PhysicalExam.Eyes}");
        col.Item().PaddingBottom(-5).Text($"ENT: {report.PhysicalExam.Ent}");
        col.Item().PaddingBottom(-5).Text($"Heart: {report.PhysicalExam.Heart}");
        col.Item().PaddingBottom(-5).Text($"Lungs: {report.PhysicalExam.Lungs}");
        col.Item().PaddingBottom(-5).Text($"Abdomen: {report.PhysicalExam.Abdomen}");
        col.Item().PaddingBottom(-5).Text($"Neurology: {report.PhysicalExam.Neurology}");
    }

    private void AddLabSection(ColumnDescriptor col, MedicalCheckupReportDto report)
    {
        if (report.LabResults == null || !report.LabResults.Any()) return;

        col.Item().PaddingTop(10);
        col.Item().Text("LABORATORY RESULTS").Bold().FontSize(14);

        col.Item().Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(2);
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
            });

            table.Header(header =>
            {
                header.Cell().Text("Test").Bold();
                header.Cell().Text("Value").Bold();
                header.Cell().Text("Normal Range").Bold();
                header.Cell().Text("Status").Bold();
            });

            foreach (var lab in report.LabResults)
            {
                var normalRange = lab.NormalMin.HasValue && lab.NormalMax.HasValue
                    ? $"{lab.NormalMin} - {lab.NormalMax}"
                    : "-";

                table.Cell().Text(lab.TestName);
                table.Cell().Text($"{lab.Value} {lab.Unit}");
                table.Cell().Text(normalRange);
                table.Cell().Text(lab.IsNormal ? "Normal" : "Abnormal");
            }
        });
    }

    private void AddRadiologySection(ColumnDescriptor col, MedicalCheckupReportDto report)
    {
        if (report.Radiology == null) return;

        var radio = report.Radiology;

        col.Item().PaddingTop(15);

        col.Item().Text("I. PEMERIKSAAN PENUNJANG")
            .Bold()
            .FontSize(14);

        col.Item().PaddingTop(5);

        col.Item().Text("Pemeriksaan Radiologi")
            .Bold()
            .FontSize(12);

        col.Item().PaddingTop(8);

        col.Item().Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(1);
                columns.RelativeColumn(3);
            });

            void Row(string label, string value)
            {
                table.Cell().PaddingVertical(3).Text(label).SemiBold();
                table.Cell().PaddingVertical(3).Text(value);
            }

            Row("Examination", radio.Examination);
            Row("Tanggal", radio.ExamDate.ToString("dd MMMM yyyy"));
            Row("Findings", radio.Findings);
            table.Cell().PaddingVertical(3).Text("Impression").SemiBold();
            table.Cell().PaddingVertical(3).Text(radio.Impression)
                .Bold()
                .FontColor(Colors.Red.Medium);
            Row("Radiologist", radio.RadiologistName);
        });
    }

    private void AddEkgSection(ColumnDescriptor col, MedicalCheckupReportDto report)
    {
        if (report.Ekg == null) return;

        var ekg = report.Ekg;

        col.Item().PaddingTop(15);

        col.Item().Text("PEMERIKSAAN EKG")
            .Bold()
            .FontSize(12);

        col.Item().PaddingTop(8);

        col.Item().Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(1);
                columns.RelativeColumn(3);
            });

            void Row(string label, string value)
            {
                table.Cell().PaddingVertical(3).Text(label).SemiBold();
                table.Cell().PaddingVertical(3).Text(value);
            }

            Row("Tanggal", ekg.ExamDate.ToString("dd MMMM yyyy"));
            Row("Rhythm", ekg.Rhythm);
            Row("Heart Rate", $"{ekg.HeartRate} bpm");
            Row("Axis", ekg.Axis);

            table.Cell().PaddingVertical(3).Text("Impression").SemiBold();
            table.Cell().PaddingVertical(3)
                .Text(ekg.Impression)
                .Bold();

            Row("Dokter Pemeriksa", ekg.DoctorName);
        });
    }
    private void AddConclusionSection(ColumnDescriptor col, MedicalCheckupReportDto report)
    {
        if (report.Conclusion == null) return;

        col.Item().PaddingTop(10);
        col.Item().Text("FINAL CONCLUSION").Bold().FontSize(14);

        col.Item().Text($"Diagnosis: {report.Conclusion.Diagnosis}");
        col.Item().Text($"Recommendation: {report.Conclusion.Recommendation}");

        col.Item().PaddingTop(10);

        col.Item().Text("FINAL STATUS:")
            .Bold();

        col.Item().Text(report.Conclusion.FitnessStatus.ToString())
            .Bold()
            .FontSize(14);

        col.Item().PaddingTop(20);
        col.Item().AlignRight().Column(signature =>
        {
            signature.Item().Text($"Jakarta, {DateTime.Now:dd MMM yyyy}");
            signature.Item().Text("Doctor");
            signature.Item().Text("Medical Coordinator");
        });
    }
}
