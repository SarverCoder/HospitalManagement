using HospitalManagement.Dtos;
using HospitalManagement.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost("schedule")]
        public async Task<IActionResult> ScheduleAppointment([FromBody] ArrangeAppointmentDto dto)
        {
            var result =
                await _appointmentService.ScheduleAppointments(dto.DoctorId, dto.PatientId, dto.AppointmentDate);

            if (result.Contains("successfully"))
            {
                return Ok(new { Message = result });
            }

            return BadRequest(new { Error = result });
        }
    }
}
