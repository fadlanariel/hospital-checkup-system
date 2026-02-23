using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Infrastructure;
using HospitalCheckupSystem.Domain.Entities;

namespace HospitalCheckupSystem.Infrastructure.Persistence.Configurations;

public class MedicalCheckupConfiguration : IEntityTypeConfiguration<MedicalCheckup>
{
    public void Configure(EntityTypeBuilder<MedicalCheckup> builder)
    {
        builder.ToTable("MedicalCheckups");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.McuNumber)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.CheckupDate)
            .IsRequired();

        builder.Property(x => x.IsFinished);

        // ---------- VITALS ----------
        builder.OwnsOne(x => x.Vitals, v =>
        {
            v.ToTable("CheckupVitals");

            v.WithOwner().HasForeignKey("MedicalCheckupId");

            v.Property<Guid>("Id");
            v.HasKey("Id");

            v.Property(p => p.Height);
            v.Property(p => p.Weight);
            v.Property(p => p.Systolic);
            v.Property(p => p.Diastolic);
            v.Property(p => p.Pulse);
        });

        // ---------- ANAMNESIS ----------
        builder.OwnsOne(x => x.Anamnesis, a =>
        {
            a.ToTable("CheckupAnamneses");

            a.WithOwner().HasForeignKey("MedicalCheckupId");

            a.Property<Guid>("Id");
            a.HasKey("Id");

            a.Property(p => p.Complaints);
            a.Property(p => p.PastIllness);
            a.Property(p => p.FamilyHistory);
            a.Property(p => p.Allergies);
            a.Property(p => p.Smoking);
            a.Property(p => p.Alcohol);
            a.Property(p => p.WorkHazards);
        });

        // ---------- PHYSICAL ----------
        builder.OwnsOne(x => x.PhysicalExam, p =>
        {
            p.ToTable("CheckupPhysicalExams");

            p.WithOwner().HasForeignKey("MedicalCheckupId");

            p.Property<Guid>("Id");
            p.HasKey("Id");

            p.Property(x => x.GeneralAppearance);
            p.Property(x => x.Eyes);
            p.Property(x => x.ENT);
            p.Property(x => x.Heart);
            p.Property(x => x.Lungs);
            p.Property(x => x.Abdomen);
            p.Property(x => x.Neurology);
        });

        // ---------- CONCLUSION ----------
        builder.OwnsOne(x => x.Conclusion, c =>
        {
            c.ToTable("CheckupConclusions");

            c.WithOwner().HasForeignKey("MedicalCheckupId");

            c.Property<Guid>("Id");
            c.HasKey("Id");

            c.Property(x => x.FitnessStatus);
            c.Property(x => x.Diagnosis);
            c.Property(x => x.Recommendation);
        });

        // ---------- LAB RESULTS ----------
        builder.OwnsMany(x => x.LabResults, l =>
        {
            l.ToTable("CheckupLabResults");

            l.WithOwner().HasForeignKey("MedicalCheckupId");

            l.Property<Guid>("Id");
            l.HasKey("Id");

            l.Property(x => x.TestName);
            l.Property(x => x.Unit);
            l.Property(x => x.Value);
            l.Property(x => x.NormalMin);
            l.Property(x => x.NormalMax);
        });
    }
}
