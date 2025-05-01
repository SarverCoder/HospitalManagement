using HospitalManagement.Application.Queries.GetDoctors;
using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Repository.Interfaces;
using HospitalManagement.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HospitalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableRateLimiting("fixed")]
    public class DoctorsController : ControllerBase
    {
        
        private readonly IDoctorService _doctorService;

        private static Dictionary<int, Doctor> _doctorCache = new Dictionary<int, Doctor>();
        private readonly IMediator _mediator;
        public DoctorsController(IDoctorRepository doctorRepository, IDoctorService doctorService, IMediator mediator)
        {
            _doctorService = doctorService;
            _mediator = mediator;
        }

        [HttpGet("workload")]
        public async Task<IActionResult> GetDoctorWorkload([FromQuery] int doctorId, [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            var result = await _doctorService.GetDoctorWorkloadAsync(doctorId, startDate, endDate);
            if (result.TotalAppointments > 0)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet("{doctorId}")]
        public async Task<IActionResult> GetDoctorById(int doctorId)
        {

            var result = await _doctorService.GetDoctorByIdAsync(doctorId);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        {
            var result = await _mediator.Send(new GetDoctorsQuery());
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

    }
}
