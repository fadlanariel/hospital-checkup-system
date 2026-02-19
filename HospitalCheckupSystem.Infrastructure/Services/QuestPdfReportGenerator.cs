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

                page.Content().Column(col =>
                {
                    col.Item().Text("Medical Checkup Report")
                        .FontSize(20)
                        .Bold();

                    col.Item().Text($"MCU Number: {report.McuNumber}");
                    col.Item().Text($"Date: {report.CheckupDate:yyyy-MM-dd}");
                    col.Item().Text($"Patient ID: {report.PatientId}");

                    col.Item().PaddingTop(10).Text("Vitals").Bold();
                    if (report.Vitals != null)
                    {
                        col.Item().Text($"Height: {report.Vitals.Height} cm");
                        col.Item().Text($"Weight: {report.Vitals.Weight} kg");
                        col.Item().Text($"Blood Pressure: {report.Vitals.Systolic}/{report.Vitals.Diastolic}");
                        col.Item().Text($"Pulse: {report.Vitals.Pulse}");
                    }

                    col.Item().PaddingTop(10).Text("Conclusion").Bold();
                    col.Item().Text($"Fit: {(report.Conclusion.Fit ? "Yes" : "No")}");
                    col.Item().Text($"Diagnosis: {report.Conclusion.Diagnosis}");
                    col.Item().Text($"Recommendation: {report.Conclusion.Recommendation}");
                });
            });
        }).GeneratePdf();
    }
}
