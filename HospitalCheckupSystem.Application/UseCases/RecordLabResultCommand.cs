namespace HospitalCheckupSystem.Application.UseCases;

public class RecordLabResultCommand
{
    public Guid CheckupId { get; set; }
    public string TestName { get; set; } = "";
    public string Unit { get; set; } = "";
    public string Value { get; set; } = "";
    public decimal? NormalMin { get; set; }
    public decimal? NormalMax { get; set; }
}
