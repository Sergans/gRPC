namespace ClinicService.Models.Requests
{
    public class CreateConsultationRequest
    {
        public string Document { get; set; }

        public string? Surname { get; set; }

        public string? FirstName { get; set; }

        public string? Patronymic { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }

        public DateTime ConsultationDate { get; set; }
        public string Description { get; set; }
    }
}
