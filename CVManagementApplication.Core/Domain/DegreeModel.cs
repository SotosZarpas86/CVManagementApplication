using System.ComponentModel.DataAnnotations;

namespace CVManagementApplication.Core.Domain
{
    public class DegreeModel
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
