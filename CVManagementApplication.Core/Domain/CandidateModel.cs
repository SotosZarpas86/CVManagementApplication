namespace CVManagementApplication.Core.Domain
{
    public class CandidateModel
    {
        public int Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public int DegreeID { get; set; }

        public string? CVblob { get; set; }

        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
