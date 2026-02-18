using HospitalCheckupSystem.Application.DTOs;
using HospitalCheckupSystem.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace HospitalCheckupSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CheckupsController : ControllerBase
{
    private readonly StartMedicalCheckupUseCase _startUseCase;

    public CheckupsController(StartMedicalCheckupUseCase startUseCase)
    {
        _startUseCase = startUseCase;
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
}
