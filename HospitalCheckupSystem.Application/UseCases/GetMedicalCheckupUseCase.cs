using HospitalCheckupSystem.Application.DTOs;
using HospitalCheckupSystem.Domain.Entities;
using HospitalCheckupSystem.Domain.Interfaces;

namespace HospitalCheckupSystem.Application.UseCases;

public class GetMedicalCheckupUseCase
{
    private readonly IMedicalCheckupRepository _repository;

    public GetMedicalCheckupUseCase(IMedicalCheckupRepository repository)
    {
        _repository = repository;
    }

    public async Task<MedicalCheckupResponseDto> Execute(Guid id)
    {
        var checkup = await _repository.GetByIdAsync(id);

        if (checkup == null)
            throw new InvalidOperationException("Checkup not found");

        return new MedicalCheckupResponseDto
        {
            Id = checkup.Id,
            PatientId = checkup.PatientId,
            McuNumber = checkup.McuNumber,
            CheckupDate = checkup.CheckupDate,
            Status = checkup.Status,
            IsFinished = checkup.IsFinished,

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

            LabResults = checkup.LabResults
                .Select(l => new LabResultItemDto
                {
                    TestName = l.TestName,
                    Unit = l.Unit,
                    Value = l.Value,
                    NormalMin = l.NormalMin,
                    NormalMax = l.NormalMax,
                    IsNormal = l.IsNormal
                }).ToList(),

            Conclusion = checkup.Conclusion == null ? null : new MedicalConclusionDto
            {
                FitnessStatus = checkup.Conclusion.FitnessStatus,
                Diagnosis = checkup.Conclusion.Diagnosis,
                Recommendation = checkup.Conclusion.Recommendation
            }
        };
    }
}
