namespace HospitalManagement.JwtConfiguration.Options
{
    public class JwtSettings
    {
        public string Key { get; set; }

        public string[] Issuers { get; set; }

        public string[] Audiences { get; set; }
    }
}
