using Microsoft.AspNetCore.Mvc;
using HospitalCheckupSystem.Application.DTOs;
using HospitalCheckupSystem.Application.UseCases;

namespace HospitalCheckupSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly CreatePatientUseCase _createPatient;

    public PatientsController(CreatePatientUseCase createPatient)
    {
        _createPatient = createPatient;
    }

    [HttpPost]
    public async Task<ActionResult<CreatePatientResponse>> Create([FromBody] CreatePatientRequest request)
    {
        var result = await _createPatient.ExecuteAsync(request);
        return Ok(result);
    }
}
