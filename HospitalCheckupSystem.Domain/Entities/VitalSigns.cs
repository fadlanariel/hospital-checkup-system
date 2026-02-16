namespace HospitalCheckupSystem.Domain.Entities;

public class VitalSigns
{
    public decimal Height { get; private set; }
    public decimal Weight { get; private set; }
    public int Systolic { get; private set; }
    public int Diastolic { get; private set; }
    public int Pulse { get; private set; }
    public decimal BMI { get; private set; }

    private VitalSigns() { }

    public VitalSigns(decimal height, decimal weight, int systolic, int diastolic, int pulse)
    {
        Height = height;
        Weight = weight;
        Systolic = systolic;
        Diastolic = diastolic;
        Pulse = pulse;

        var heightMeter = height / 100m;
        BMI = weight / (heightMeter * heightMeter);
    }
}
