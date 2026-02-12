using HospitalCheckupSystem.Application.DTOs;
using HospitalCheckupSystem.Domain.Entities;
using HospitalCheckupSystem.Domain.Interfaces;

namespace HospitalCheckupSystem.Application.UseCases;

public class CreatePatientUseCase
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMrnGenerator _mrnGenerator;

    public CreatePatientUseCase(
        IPatientRepository patientRepository,
        IMrnGenerator mrnGenerator)
    {
        _patientRepository = patientRepository;
        _mrnGenerator = mrnGenerator;
    }

    public async Task<CreatePatientResponse> ExecuteAsync(CreatePatientRequest request)
    {
        var sequence = await _patientRepository.GetNextSequenceAsync();
        var mrn = _mrnGenerator.Generate(sequence);

        var patient = new Patient(
            mrn,
            request.Name,
            request.Dob,
            request.Gender,
            request.Phone,
            request.Address,
            request.Insurance);

        await _patientRepository.AddAsync(patient);

        return new CreatePatientResponse
        {
            Id = patient.Id,
            Mrn = patient.Mrn,
            Name = patient.Name
        };
    }
}
