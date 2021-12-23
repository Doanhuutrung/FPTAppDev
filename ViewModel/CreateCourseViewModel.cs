using FPTAppDev.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FPTAppDev.ViewModel
{
    public class CreateCourseViewModel
    {
        public Course Course { get; set; }
        public List<Category> Category { get; set; }
    }
}