using FluentValidation;
using HospitalCheckupSystem.Application.DTOs;
using HospitalCheckupSystem.Domain.Entities;
using HospitalCheckupSystem.Domain.Interfaces;

namespace HospitalCheckupSystem.Application.UseCases;

public class CreatePatientUseCase
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMrnGenerator _mrnGenerator;
    private readonly IValidator<CreatePatientRequest> _validator;

    public CreatePatientUseCase(
        IPatientRepository patientRepository,
        IMrnGenerator mrnGenerator,
        IValidator<CreatePatientRequest> validator)
    {
        _patientRepository = patientRepository;
        _mrnGenerator = mrnGenerator;
        _validator = validator;
    }

    public async Task<CreatePatientResponse> ExecuteAsync(CreatePatientRequest request)
    {
        var validation = await _validator.ValidateAsync(request);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

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
