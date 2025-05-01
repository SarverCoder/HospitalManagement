using AutoMapper;
using HospitalManagement.appsettingsModel;
using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Dtos;
using HospitalManagement.Repository.Interfaces;
using Microsoft.Extensions.Options;

namespace HospitalManagement.Services;

public interface IAppointmentService
{
    bool CanCancelAppointment(DateTime appointmentTime);

    Task CreateAppointment(ArrangeAppointmentDto appointmentDto);

    Task<string> ScheduleAppointments(int doctorId, int patientId, DateTime appointmentDate);

}
public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly AppointmentSettings _options;
    private readonly IMapper _mapper;

    public AppointmentService(IAppointmentRepository appointmentRepository,
        IOptionsSnapshot<AppointmentSettings> options, IMapper mapper)
    {
        _appointmentRepository = appointmentRepository;
        _mapper = mapper;
        _options = options.Value;
    }




    public bool CanCancelAppointment(DateTime appointmentTime)
    {
        TimeSpan timeRemaining = appointmentTime - DateTime.Now;
        return timeRemaining.TotalHours > _options.CancellationDeadlineHours;
    }

    public async Task CreateAppointment(ArrangeAppointmentDto appointmentDto)
    {
        var appointment = appointmentDto.ToAppointment();

        await _appointmentRepository.AddAsync(appointment);
        await _appointmentRepository.SaveChangesAsync();
    }

    public async Task<string> ScheduleAppointments(int doctorId, int patientId, DateTime appointmentDate)
    {
        return await _appointmentRepository.SheduleAppointment(doctorId, patientId, appointmentDate);
    }
}
