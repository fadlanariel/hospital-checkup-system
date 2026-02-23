using HospitalCheckupSystem.Application.DTOs;
using HospitalCheckupSystem.Domain.Enums;
using HospitalCheckupSystem.Domain.Interfaces;

namespace HospitalCheckupSystem.Application.UseCases;

public class GenerateCheckupReportUseCase
{
    private readonly IMedicalCheckupRepository _repository;
    private readonly IPatientRepository _patientRepository;

    public GenerateCheckupReportUseCase(
        IMedicalCheckupRepository repository,
        IPatientRepository patientRepository)
    {
        _repository = repository;
        _patientRepository = patientRepository;
    }

    public async Task<MedicalCheckupReportDto> Execute(Guid id)
    {
        var checkup = await _repository.GetByIdAsync(id);

        if (checkup == null)
            throw new InvalidOperationException("Checkup not found");

        if (!checkup.IsFinished)
            throw new InvalidOperationException("Checkup not finished");

        var patient = await _patientRepository.GetByIdAsync(checkup.PatientId);

        if (patient == null)
            throw new InvalidOperationException("Patient not found");

        return new MedicalCheckupReportDto
        {
            PatientMrn = patient.Mrn,
            PatientName = patient.Name,
            Gender = patient.Gender,
            DateOfBirth = patient.Dob,

            Id = checkup.Id,
            PatientId = checkup.PatientId,
            McuNumber = checkup.McuNumber,
            CheckupDate = checkup.CheckupDate,

            Vitals = checkup.Vitals == null ? null : new VitalSignsDto
            {
                Height = checkup.Vitals.Height,
                Weight = checkup.Vitals.Weight,
                Systolic = checkup.Vitals.Systolic,
                Diastolic = checkup.Vitals.Diastolic,
                Pulse = checkup.Vitals.Pulse
            },

            Anamnesis = checkup.Anamnesis == null ? null : new AnamnesisDto
            {
                Complaints = checkup.Anamnesis.Complaints,
                PastIllness = checkup.Anamnesis.PastIllness,
                FamilyHistory = checkup.Anamnesis.FamilyHistory,
                Allergies = checkup.Anamnesis.Allergies,
                Smoking = checkup.Anamnesis.Smoking,
                Alcohol = checkup.Anamnesis.Alcohol,
                WorkHazards = checkup.Anamnesis.WorkHazards
            },

            PhysicalExam = checkup.PhysicalExam == null ? null : new PhysicalExamDto
            {
                GeneralAppearance = checkup.PhysicalExam.GeneralAppearance,
                Eyes = checkup.PhysicalExam.Eyes,
                Ent = checkup.PhysicalExam.ENT,
                Heart = checkup.PhysicalExam.Heart,
                Lungs = checkup.PhysicalExam.Lungs,
                Abdomen = checkup.PhysicalExam.Abdomen,
                Neurology = checkup.PhysicalExam.Neurology
            },

            LabResults = checkup.LabResults.Select(l => new LabResultItemDto
            {
                TestName = l.TestName,
                Unit = l.Unit,
                Value = l.Value,
                NormalMin = l.NormalMin,
                NormalMax = l.NormalMax,
                IsNormal = l.IsNormal
            }).ToList(),

            Conclusion = new MedicalConclusionDto
            {
                FitnessStatus = checkup.Conclusion.FitnessStatus switch
                {
                    FitnessStatus.Fit => FitnessStatus.Fit,
                    FitnessStatus.FitWithRestriction => FitnessStatus.FitWithRestriction,
                    FitnessStatus.Unfit => FitnessStatus.Unfit,
                    _ => throw new InvalidOperationException("Unknown fitness status")
                },
                Diagnosis = checkup.Conclusion.Diagnosis,
                Recommendation = checkup.Conclusion.Recommendation
            }
        };
    }
}
