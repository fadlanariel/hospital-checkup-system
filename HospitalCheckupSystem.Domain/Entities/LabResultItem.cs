using System.Globalization;

namespace HospitalCheckupSystem.Domain.Entities;

public class LabResultItem
{
    public string TestName { get; private set; }
    public string Unit { get; private set; }
    public string Value { get; private set; }
    public decimal? NormalMin { get; private set; }
    public decimal? NormalMax { get; private set; }
    public bool IsNormal { get; private set; }

    private LabResultItem() { }

    public LabResultItem(
        string testName,
        string unit,
        string value,
        decimal? normalMin,
        decimal? normalMax)
    {
        TestName = testName;
        Unit = unit;
        Value = value;
        NormalMin = normalMin;
        NormalMax = normalMax;

        EvaluateNormal();
    }

    private void EvaluateNormal()
    {
        if (!decimal.TryParse(
                Value,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out var numeric))
        {
            IsNormal = true;
            return;
        }

        if (NormalMin == null || NormalMax == null)
        {
            IsNormal = true;
            return;
        }

        IsNormal = numeric >= NormalMin && numeric <= NormalMax;
    }

}
