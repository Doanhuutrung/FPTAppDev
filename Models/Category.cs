using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FPTAppDev.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Index("UniqueName", 1, IsUnique = true)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}