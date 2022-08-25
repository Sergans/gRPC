namespace ClinicService.Models.Responses
{
    public class ConsultationResponse
    {
        public DateTime ConsultationDate { get; set; }
        public string Description { get; set; }
        public string ClientName { get; set; }
        public string PetName { get; set; }
    }
}
