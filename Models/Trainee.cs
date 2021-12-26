using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FPTAppDev.Models
{
    public class Trainee
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [Range(0, 1000)]
        public int Age { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Education { get; set; }

        [Key]
        [ForeignKey("User")]
        public string TraineeId { get; set; }

        public ApplicationUser User { get; set; }
    }
}