using HospitalManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.DataAccess
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options)
        {
            
        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientBlank> PatientBlanks { get; set; }
        public DbSet<Speciality> Specialities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(builder =>
            {
                builder.HasKey(x => x.AppointmentId);

                builder.HasOne(x => x.Patient)
                    .WithMany(x => x.Appointments);
            });


            modelBuilder.Entity<Doctor>(builder => {
                builder.HasKey(x => x.DoctorId);

                builder.HasMany(x => x.Appointments)
                    .WithOne(x => x.Doctor);

                builder.HasOne(x => x.Speciality)
                    .WithMany();
            });

            modelBuilder.Entity<Patient>(builder =>
            {
                builder.HasKey(r => r.PatientId);

                builder.HasOne(r => r.PatientBlank)
                    .WithOne(r => r.Patient);

                builder.HasMany(r => r.Appointments)
                    .WithOne(r => r.Patient);
            });

            modelBuilder.Entity<PatientBlank>(builder =>
            {
                builder.HasKey(r => r.PatientBlankId);

                builder.HasOne(r => r.Patient)
                    .WithOne(r => r.PatientBlank);
            });

            modelBuilder.Entity<Speciality>(builder =>
            {
                builder.HasKey(r => r.SpecialityId);
            });
        }
    }
}
