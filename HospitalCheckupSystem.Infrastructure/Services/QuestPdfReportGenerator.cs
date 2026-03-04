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

                page.Header().Column(header =>
                {
                    header.Item().AlignCenter().Text("MEDICAL CHECKUP REPORT")
                        .SemiBold().FontSize(20);

                    header.Item().AlignCenter().Text("General Medical Examination")
                        .FontSize(11);

                    header.Item().PaddingTop(5).LineHorizontal(1);
                });


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
                    .AlignCenter()
                    .Text(text =>
                    {
                        text.Span("Confidential Medical Document - ");
                        text.Span($"Generated on {DateTime.Now:dd MMM yyyy}")
                            .SemiBold();
                    });
            });
        }).GeneratePdf();
    }
    private void SectionTitle(ColumnDescriptor col, string title)
    {
        col.Item().PaddingTop(15);

        col.Item().Text(title)
            .Bold()
            .FontSize(14);

        col.Item().LineHorizontal(0.5f)
            .LineColor(Colors.Grey.Lighten2);

        col.Item().PaddingTop(5);
    }

    private void AddPatientSection(ColumnDescriptor col, MedicalCheckupReportDto report)
    {
        SectionTitle(col, "PATIENT INFORMATION");
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
        SectionTitle(col, "VITAL SIGNS");

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
                columns.RelativeColumn();
            });

            table.Cell().Text("Height").SemiBold();
            table.Cell().Text("Weight").SemiBold();
            table.Cell().Text("BMI").SemiBold();

            table.Cell().Text($"{report.Vitals.Height} cm");
            table.Cell().Text($"{report.Vitals.Weight} kg");
            table.Cell().Text($"{bmi:F2}");

            table.Cell().Text("Blood Pressure").SemiBold();
            table.Cell().Text("Pulse").SemiBold();
            table.Cell().Text("");

            table.Cell().Text($"{report.Vitals.Systolic}/{report.Vitals.Diastolic} mmHg");
            table.Cell().Text($"{report.Vitals.Pulse} bpm");
            table.Cell().Text("");
        });
    }

    private void AddAnamnesisSection(ColumnDescriptor col, MedicalCheckupReportDto report)
    {
        if (report.Anamnesis == null) return;

        col.Item().PaddingTop(10);
        SectionTitle(col, "MEDICAL HISTORY (ANAMNESIS)");

        col.Spacing(3);
        col.Item().Text($"Complaints: {report.Anamnesis.Complaints}");
        col.Item().Text($"Past Illness: {report.Anamnesis.PastIllness}");
        col.Item().Text($"Family History: {report.Anamnesis.FamilyHistory}");
        col.Item().Text($"Allergies: {report.Anamnesis.Allergies}");
        col.Item().Text($"Smoking: {(report.Anamnesis.Smoking ? "Yes" : "No")}");
        col.Item().Text($"Alcohol: {(report.Anamnesis.Alcohol ? "Yes" : "No")}");
        col.Item().Text($"Work Hazards: {report.Anamnesis.WorkHazards}");
    }

    private void AddPhysicalExamSection(ColumnDescriptor col, MedicalCheckupReportDto report)
    {
        if (report.PhysicalExam == null) return;

        col.Item().PaddingTop(10);
        SectionTitle(col, "PHYSICAL EXAMINATION");

        col.Spacing(3);
        col.Item().Text($"General Appearance: {report.PhysicalExam.GeneralAppearance}");
        col.Item().Text($"Eyes: {report.PhysicalExam.Eyes}");
        col.Item().Text($"ENT: {report.PhysicalExam.Ent}");
        col.Item().Text($"Heart: {report.PhysicalExam.Heart}");
        col.Item().Text($"Lungs: {report.PhysicalExam.Lungs}");
        col.Item().Text($"Abdomen: {report.PhysicalExam.Abdomen}");
        col.Item().Text($"Neurology: {report.PhysicalExam.Neurology}");
    }

    private void AddLabSection(ColumnDescriptor col, MedicalCheckupReportDto report)
    {
        if (report.LabResults == null || !report.LabResults.Any()) return;

        col.Item().PaddingTop(10);
        SectionTitle(col, "LABORATORY RESULTS");

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
                header.Cell().Background(Colors.Grey.Lighten3).Padding(4).Text("Test").Bold();
                header.Cell().Background(Colors.Grey.Lighten3).Padding(4).Text("Value").Bold();
                header.Cell().Background(Colors.Grey.Lighten3).Padding(4).Text("Normal Range").Bold();
                header.Cell().Background(Colors.Grey.Lighten3).Padding(4).Text("Status").Bold();
            });

            foreach (var lab in report.LabResults)
            {
                var normalRange = lab.NormalMin.HasValue && lab.NormalMax.HasValue
                    ? $"{lab.NormalMin} - {lab.NormalMax}"
                    : "-";

                table.Cell().Text(lab.TestName);
                table.Cell().Text($"{lab.Value} {lab.Unit}");

                table.Cell().Text(normalRange);

                table.Cell()
                    .Text(lab.IsNormal ? "Normal" : "Abnormal")
                    .FontColor(lab.IsNormal ? Colors.Black : Colors.Red.Medium)
                    .Bold();
            }
        });
    }

    private void AddRadiologySection(ColumnDescriptor col, MedicalCheckupReportDto report)
    {
        if (report.Radiology == null) return;

        var radio = report.Radiology;

        col.Item().PaddingTop(15);

        SectionTitle(col, "SUPPORTING EXAMINATION");

        col.Item().PaddingTop(5);

        SectionTitle(col, "RADIOLOGY EXAMINATION");

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
                table.Cell().PaddingVertical(3).Text(value ?? "-");
            }

            Row("Examination Type", radio.Examination);
            Row("Examination Date", radio.ExamDate.ToString("dd MMMM yyyy"));
            Row("Findings", radio.Findings);

            table.Cell().PaddingVertical(3).Text("Impression").SemiBold();
            table.Cell().PaddingVertical(3)
                .Text(radio.Impression ?? "-")
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

        SectionTitle(col, "EKG EXAMINATION");

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
                table.Cell().PaddingVertical(3).Text(value ?? "-");
            }

            Row("Examination Date", ekg.ExamDate.ToString("dd MMMM yyyy"));
            Row("Heart Rhythm", ekg.Rhythm);
            Row("Heart Rate", $"{ekg.HeartRate} bpm");
            Row("Cardiac Axis", ekg.Axis);

            table.Cell().PaddingVertical(3).Text("Impression").SemiBold();
            table.Cell().PaddingVertical(3)
                .Text(ekg.Impression ?? "-")
                .Bold();

            Row("Examining Doctor", ekg.DoctorName);
        });
    }

    private void AddConclusionSection(ColumnDescriptor col, MedicalCheckupReportDto report)
    {
        if (report.Conclusion == null) return;

        col.Item().PaddingTop(10);
        SectionTitle(col, "FINAL CONCLUSION");

        col.Item().PaddingTop(5).Text("Diagnosis").Bold();
        col.Item().Text(report.Conclusion.Diagnosis);

        col.Item().PaddingTop(5).Text("Recommendation").Bold();
        col.Item().Text(report.Conclusion.Recommendation);

        col.Item().PaddingTop(15);

        col.Item().Background(Colors.Grey.Lighten4)
            .Padding(10)
            .Column(status =>
            {
                status.Item().Text("FITNESS STATUS")
                    .Bold();

                status.Item().Text(report.Conclusion.FitnessStatus.ToString())
                    .Bold()
                    .FontSize(16)
                    .FontColor(Colors.Blue.Darken2);
            });

        col.Item().PaddingTop(30);

        col.Item().AlignRight().Column(signature =>
        {
            signature.Item().Text($"Jakarta, {DateTime.Now:dd MMMM yyyy}");

            signature.Item().PaddingTop(40); // space for real signature

            signature.Item().Text("Dr. __________________________")
                .Bold();

            signature.Item().Text("Medical Coordinator");
        });
    }
}
