using HospitalCheckupSystem.Application.DTOs;

namespace HospitalCheckupSystem.Application.Interfaces;

public interface IPdfReportGenerator
{
    byte[] GenerateMedicalCheckupReport(MedicalCheckupReportDto report);
}
