namespace HospitalManagement.DataAccess.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public int Capacity { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
