using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FPTAppDev.Models
{
    public class Trainer
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [Range(0, 1000)]
        public int Age { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Specialty { get; set; }

        [Key]
        [ForeignKey("User")]
        public string TrainerId { get; set; }

        public ApplicationUser User { get; set; }
    }
}