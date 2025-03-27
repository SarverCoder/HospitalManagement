namespace HospitalManagement.Dtos
{
    public class DoctorDto
    {
        public int DoctorId { get; set; }

        public string FullName { get; set; }    

        public bool IsActive { get; set; }

        public int SpecialityId { get; set; }

    }
}
