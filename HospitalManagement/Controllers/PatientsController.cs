using HospitalManagement.appsettingsModel;
using HospitalManagement.Dtos;
using HospitalManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;


namespace HospitalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly ILogger<PatientsController> _logger;
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly DoctorSettings _doctorTime;

        public PatientsController(
            IOptionsSnapshot<DoctorSettings> doctorTime,
            ILogger<PatientsController> _logger,
            IAppointmentService appointmentService,
            IPatientService patientService)
        {
            this._logger = _logger;
            _appointmentService = appointmentService;
            _patientService = patientService;
            _doctorTime = doctorTime.Value;
        }


        [HttpGet]
        public IActionResult GetAllPatients()
        {
            var patients = _patientService.GetAllPatients();

            return Ok(patients);
        }

        [HttpPost]
        public async Task<IActionResult> ArrangeAppointment([FromBody] ArrangeAppointmentDto arrangeAppointmentDto)
        {
            var timeApp = TimeOnly.FromDateTime(arrangeAppointmentDto.AppointmentDate);
        

            if (!timeApp.IsBetween(_doctorTime.WorkTime.Start, _doctorTime.WorkTime.End))
            {
                _logger.LogWarning("Doctor is not available at this time");

                return BadRequest("Doctor is not available at this time");
            }

            await _appointmentService.CreateAppointment(arrangeAppointmentDto);

            return Ok("Your application arranged");
        }

        [HttpPost("CancelAppointment")]
        public IActionResult CancelAppointment([FromBody] CancelAppointmentRequest request)
        {
            if (_appointmentService.CanCancelAppointment(request.AppointmentDate))
            {
                return Ok("Appointment successfully cancelled.");
            }

            return BadRequest("Cannot cancel appointment within the deadline.");
        }




    }
}
