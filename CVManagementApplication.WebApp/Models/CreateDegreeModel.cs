using System.ComponentModel.DataAnnotations;

namespace CVManagementApplication.WebApp.Models
{
    public class CreateDegreeModel
    {
        [Required(ErrorMessage = "Name is required field")]
        public string? Name { get; set; }
    }
}
