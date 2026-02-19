namespace HospitalCheckupSystem.Application.DTOs;

public class MedicalCheckupResponseDto
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public string McuNumber { get; set; } = default!;
    public DateTime CheckupDate { get; set; }
    public string Status { get; set; } = default!;
    public bool IsFinished { get; set; }

    public VitalSignsDto? Vitals { get; set; }
    public AnamnesisDto? Anamnesis { get; set; }
    public PhysicalExamDto? PhysicalExam { get; set; }
    public List<LabResultItemDto> LabResults { get; set; } = new();
    public MedicalConclusionDto? Conclusion { get; set; }
}

public class VitalSignsDto
{
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public int Systolic { get; set; }
    public int Diastolic { get; set; }
    public int Pulse { get; set; }
}

public class AnamnesisDto
{
    public string Complaints { get; set; } = default!;
    public string PastIllness { get; set; } = default!;
    public string FamilyHistory { get; set; } = default!;
    public string Allergies { get; set; } = default!;
    public bool Smoking { get; set; }
    public bool Alcohol { get; set; }
    public string WorkHazards { get; set; } = default!;
}

public class PhysicalExamDto
{
    public string GeneralAppearance { get; set; } = default!;
    public string Eyes { get; set; } = default!;
    public string Ent { get; set; } = default!;
    public string Heart { get; set; } = default!;
    public string Lungs { get; set; } = default!;
    public string Abdomen { get; set; } = default!;
    public string Neurology { get; set; } = default!;
}

public class LabResultItemDto
{
    public string TestName { get; set; } = default!;
    public string Unit { get; set; } = default!;
    public string Value { get; set; } = default!;
    public decimal? NormalMin { get; set; }
    public decimal? NormalMax { get; set; }
    public bool IsNormal { get; set; }
}

public class MedicalConclusionDto
{
    public bool Fit { get; set; }
    public string Diagnosis { get; set; } = default!;
    public string Recommendation { get; set; } = default!;
}
