namespace HospitalManagement.Dtos;

public class RoomDto
{
    public int Id { get; set; }
    public string RoomNumber { get; set; }
    public int Capacity { get; set; }
}

public class CreateRoomDto
{
    public string RoomNumber { get; set; }
    public int Capacity { get; set; }
}

public class UpdateRoomDto
{
    public string RoomNumber { get; set; }
    public int Capacity { get; set; }
}