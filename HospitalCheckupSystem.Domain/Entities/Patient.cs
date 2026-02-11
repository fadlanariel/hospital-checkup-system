namespace HospitalCheckupSystem.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Patient
{
    public Guid Id { get; private set; }
    public string Mrn { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public DateTime Dob { get; private set; }
    public string Gender { get; private set; } = default!;
    public string Phone { get; private set; } = default!;
    public string Address { get; private set; } = default!;
    public string? Insurance { get; private set; }

    private Patient() { }

    public Patient(string mrn, string name, DateTime dob,
        string gender, string phone, string address, string? insurance)
    {
        Id = Guid.NewGuid();
        Mrn = mrn;
        Name = name;
        Dob = dob;
        Gender = gender;
        Phone = phone;
        Address = address;
        Insurance = insurance;
    }
}
