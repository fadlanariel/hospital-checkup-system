namespace HospitalCheckupSystem.Application.DTOs;

public class RecordLabResultRequest
{
    public string TestName { get; set; } = default!;
    public string Unit { get; set; } = default!;
    public string Value { get; set; } = default!;
    public decimal? NormalMin { get; set; }
    public decimal? NormalMax { get; set; }
}
