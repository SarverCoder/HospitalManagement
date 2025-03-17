namespace HospitalManagement.Services
{
    public class PdpService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PdpService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        //public async Task<string> GetDataAsync()
        //{
        //    var response = await _httpClient.GetAsync(_configuration["PdpApiUrl"]);
        //}
    }
}
