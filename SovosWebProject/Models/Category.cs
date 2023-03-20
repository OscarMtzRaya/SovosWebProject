using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SovosWebProject.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(length: 100)]
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
