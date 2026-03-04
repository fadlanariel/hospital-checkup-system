namespace HospitalCheckupSystem.Domain.Entities;

public class RadiologyResult
{
    public string Examination { get; }
    public string Findings { get; }
    public string Impression { get; }
    public string RadiologistName { get; }
    public DateTime ExamDate { get; }

    public RadiologyResult(
        string examination,
        string findings,
        string impression,
        string radiologistName,
        DateTime examDate)
    {
        if (string.IsNullOrWhiteSpace(examination))
            throw new ArgumentException("Examination is required");

        if (string.IsNullOrWhiteSpace(impression))
            throw new ArgumentException("Impression is required");

        Examination = examination;
        Findings = findings;
        Impression = impression;
        RadiologistName = radiologistName;
        ExamDate = examDate;
    }
}