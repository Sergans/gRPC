namespace ClinicService.Models.Requests
{
    public class UpdateConsultationRequest
    {
        public int ConsultationId { get; set; }
        public DateTime ConsultationDate { get; set; }
        public string Description { get; set; }
    }
}
