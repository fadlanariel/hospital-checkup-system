using HospitalCheckupSystem.Application.DTOs;
using HospitalCheckupSystem.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace HospitalCheckupSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CheckupsController : ControllerBase
{
    private readonly StartMedicalCheckupUseCase _startUseCase;
    private readonly RecordAnamnesisUseCase _anamnesisUseCase;
    private readonly RecordVitalsUseCase _vitalsUseCase;
    private readonly RecordPhysicalExamUseCase _physicalUseCase;
    private readonly RecordLabResultUseCase _labUseCase;
    private readonly MakeDoctorConclusionUseCase _conclusionUseCase;

    public CheckupsController(
        StartMedicalCheckupUseCase startUseCase,
        RecordAnamnesisUseCase anamnesisUseCase,
        RecordVitalsUseCase vitalsUseCase,
        RecordPhysicalExamUseCase physicalUseCase,
        RecordLabResultUseCase labUseCase,
        MakeDoctorConclusionUseCase conclusionUseCase)
    {
        _startUseCase = startUseCase;
        _anamnesisUseCase = anamnesisUseCase;
        _vitalsUseCase = vitalsUseCase;
        _physicalUseCase = physicalUseCase;
        _labUseCase = labUseCase;
        _conclusionUseCase = conclusionUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> StartCheckup(
        [FromBody] StartMedicalCheckupRequest request)
    {
        var result = await _startUseCase.ExecuteAsync(
            request.PatientId,
            request.CheckupDate);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result);
    }

    // placeholder for later
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        return Ok();
    }

    [HttpPost("{id}/anamnesis")]
    public async Task<IActionResult> RecordAnamnesis(
        Guid id,
        [FromBody] RecordAnamnesisRequest request)
    {
        await _anamnesisUseCase.ExecuteAsync(new RecordAnamnesisCommand
        {
            CheckupId = id,
            Complaints = request.Complaints,
            PastIllness = request.PastIllness,
            FamilyHistory = request.FamilyHistory,
            Allergies = request.Allergies,
            Smoking = request.Smoking,
            Alcohol = request.Alcohol,
            WorkHazards = request.WorkHazards
        });

        return NoContent();
    }

    [HttpPost("{id}/vitals")]
    public async Task<IActionResult> RecordVitals(
        Guid id,
        [FromBody] RecordVitalsRequest request)
    {
        await _vitalsUseCase.ExecuteAsync(new RecordVitalsCommand
        {
            CheckupId = id,
            Height = request.Height,
            Weight = request.Weight,
            Systolic = request.Systolic,
            Diastolic = request.Diastolic,
            Pulse = request.Pulse
        });

        return NoContent();
    }

    [HttpPost("{id}/physical-exam")]
    public async Task<IActionResult> RecordPhysicalExam(
        Guid id,
        [FromBody] RecordPhysicalExamRequest request)
    {
        await _physicalUseCase.ExecuteAsync(new RecordPhysicalExamCommand
        {
            CheckupId = id,
            GeneralAppearance = request.GeneralAppearance,
            Eyes = request.Eyes,
            ENT = request.ENT,
            Heart = request.Heart,
            Lungs = request.Lungs,
            Abdomen = request.Abdomen,
            Neurology = request.Neurology
        });

        return NoContent();
    }

    [HttpPost("{id}/lab-results")]
    public async Task<IActionResult> RecordLabResult(
        Guid id,
        [FromBody] RecordLabResultRequest request)
    {
        await _labUseCase.ExecuteAsync(new RecordLabResultCommand
        {
            CheckupId = id,
            TestName = request.TestName,
            Unit = request.Unit,
            Value = request.Value,
            NormalMin = request.NormalMin,
            NormalMax = request.NormalMax
        });

        return NoContent();
    }

    [HttpPost("{id}/conclusion")]
    public async Task<IActionResult> MakeConclusion(
        Guid id,
        [FromBody] MakeConclusionRequest request)
    {
        await _conclusionUseCase.ExecuteAsync(new MakeDoctorConclusionCommand
        {
            CheckupId = id,
            Fit = request.Fit,
            Diagnosis = request.Diagnosis,
            Recommendation = request.Recommendation
        });

        return NoContent();
    }
}
