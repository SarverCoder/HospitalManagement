using HospitalManagement.Application.Commands.CreateRoom;
using HospitalManagement.Application.Commands.DeleteRoom;
using HospitalManagement.Application.Commands.UpdateRoom;
using HospitalManagement.Application.Queries.GetRooms;
using HospitalManagement.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] CreateRoomDto roomDto)
        {
            if (roomDto == null)
            {
                return BadRequest("Room data is null");
            }

            try
            {
              var roomId =  await mediator.Send(new CreateRoomCommand(roomDto));
              return CreatedAtAction(nameof(GetRoomByIdTask), new { roomId }, roomDto);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("Room wast created");
            }

        }

        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetRoomByIdTask(int roomId)
        {
            var roomById = await mediator.Send(new GetRoomByIdQuery(roomId));
            if (roomById != null)
            {
                return Ok(roomById);
            }
            return NotFound("Room not found");
        }

        [HttpGet]
        public async Task<IActionResult> GetRooms()
        {
            try
            {
                var rooms = await mediator.Send(new GetRoomsQuery());
                return Ok(rooms);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("Rooms not found");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] UpdateRoomDto roomDto)
        {
            if (roomDto == null)
                return BadRequest("Invalid request");

            var result = await mediator.Send(new UpdateRoomCommand(id, roomDto));

            if (result != null)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("No update");
            }
  
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            try
            {
                var result = await mediator.Send(new DeleteRoomCommand(id));
                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound("Room not found");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("Room not deleted");
            }
        }


    }


}
