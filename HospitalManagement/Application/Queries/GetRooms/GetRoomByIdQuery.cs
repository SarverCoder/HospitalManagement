using AutoMapper;
using HospitalManagement.Dtos;
using HospitalManagement.Repository.Interfaces;
using MediatR;

namespace HospitalManagement.Application.Queries.GetRooms;

public class GetRoomByIdQuery(int id) : IRequest<RoomDto>
{
    public int Id { get; set; } = id;
}

public class GetRoomByIdQueryHandler(IRoomRepository repository, IMapper mapper)
    : IRequestHandler<GetRoomByIdQuery, RoomDto>
{
    public async Task<RoomDto> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
    {
        var room = await repository.GetByIdAsync(request.Id);
        var roomDto = mapper.Map<RoomDto>(room);
        return roomDto;
    }
}
