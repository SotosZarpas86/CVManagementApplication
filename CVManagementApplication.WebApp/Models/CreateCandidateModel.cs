using System.ComponentModel.DataAnnotations;

namespace CVManagementApplication.WebApp.Models
{
    public class CreateCandidateModel
    {
        [Required(ErrorMessage = "First name is required field")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Last name is required field")]
        public string? FirstName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required field")]
        public string? Email { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number should be 10 digits")]
        public string? Mobile { get; set; }

        public int DegreeID { get; set; }

        public string? CVblob { get; set; }
    }
}
