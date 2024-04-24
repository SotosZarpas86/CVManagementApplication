using System.ComponentModel.DataAnnotations;

namespace CVManagementApplication.Core.Domain
{
    public class DegreeCreateModel
    {
        [Required]
        public string Name { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
