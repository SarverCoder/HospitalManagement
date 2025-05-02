using AutoMapper;
using HospitalManagement.Dtos;
using HospitalManagement.Repository.Interfaces;
using MediatR;

namespace HospitalManagement.Application.Queries.GetRooms;

public class GetRoomsQuery : IRequest<IEnumerable<RoomDto>>
{
}

public class GetRoomsQueryHandler(IRoomRepository repository, IMapper mapper)
    : IRequestHandler<GetRoomsQuery, IEnumerable<RoomDto>>
{
    public async Task<IEnumerable<RoomDto>> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
    {
        var rooms = await repository.GetAllAsync();

        var roomDtos = mapper.Map<IEnumerable<RoomDto>>(rooms);

        return roomDtos.OrderBy(x => x.Id);

    }
}
