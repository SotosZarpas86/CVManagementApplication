namespace CVManagementApplication.WebApp.Models
{
    public class CandidateModel
    {
        public int ID { get; set; }

        public string? LastName { get; set; }

        public string? FirstName { get; set; }

        public string? Email { get; set; }

        public string? Mobile { get; set; }

        public int DegreeID { get; set; }

        public string? DegreeName { get; set; }

        public string? CVblob { get; set; }

        public string? CVFileName { get; set; }

        public string? CVDownloadLink { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
