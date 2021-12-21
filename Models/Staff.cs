using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FPTAppDev.Models
{
    public class Staff
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        [Range(0,1000)]
        public int Age { get; set; }
        [Required]
        public string Address { get; set; }
        [Key]
        [ForeignKey("User")]
        public string StaffId { get; set; }
        public ApplicationUser User { get; set; }
 
    }
}