namespace HospitalManagement.Dtos;

public class PatientDto
{
    public int Id { get; set; }
    public string FullName { get; set; }

    public string? DateOfBirth { get; set; }

    public DateTime RegisteredDate { get; set; }

    public int? PatientBlankId { get; set; }

    public string? BlankIdentifier { get; set; }

}
