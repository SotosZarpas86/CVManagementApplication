using System.ComponentModel.DataAnnotations;

namespace CVManagementApplication.Core.Domain
{
    public class CandidateCreateModel
    {
        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? Mobile { get; set; }

        public int? DegreeID { get; set; }

        public string? CVblob { get; set; }

        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
