using HospitalManagement.DataAccess;
using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Repository.Interfaces;

namespace HospitalManagement.Repository
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        private readonly HospitalContext _context;
        public RoomRepository(HospitalContext context) : base(context)
        {
            _context = context;
        }
    }
}
