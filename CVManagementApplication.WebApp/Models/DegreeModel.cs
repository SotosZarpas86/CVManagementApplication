using System.ComponentModel.DataAnnotations;

namespace CVManagementApplication.WebApp.Models
{
    public class DegreeModel
    {
        public int ID { get; set; }

        [Required]
        public string? Name { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
