namespace HospitalCheckupSystem.Domain.Entities;

public class EkgResult
{
    public string Rhythm { get; }
    public int HeartRate { get; }
    public string Axis { get; }
    public string Impression { get; }
    public string DoctorName { get; }
    public DateTime ExamDate { get; }

    public EkgResult(
        string rhythm,
        int heartRate,
        string axis,
        string impression,
        string doctorName,
        DateTime examDate)
    {
        if (string.IsNullOrWhiteSpace(rhythm))
            throw new ArgumentException("Rhythm is required");

        if (heartRate <= 0)
            throw new ArgumentException("Heart rate must be positive");

        if (string.IsNullOrWhiteSpace(impression))
            throw new ArgumentException("Impression is required");

        Rhythm = rhythm;
        HeartRate = heartRate;
        Axis = axis;
        Impression = impression;
        DoctorName = doctorName;
        ExamDate = examDate;
    }
}