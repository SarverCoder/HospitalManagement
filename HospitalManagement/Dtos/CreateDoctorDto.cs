namespace HospitalManagement.Dtos;

public class DoctorDto
{
    public int DoctorId { get; set; }

    public string FullName { get; set; }    

    public bool IsActive { get; set; }

    public int SpecialityId { get; set; }

}


public class CreateDoctorDto
{

    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public bool IsActive { get; set; }

    public int SpecialityId { get; set; }
}