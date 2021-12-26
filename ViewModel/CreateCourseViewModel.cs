using FPTAppDev.Models;
using System.Collections.Generic;

namespace FPTAppDev.ViewModel
{
    public class CreateCourseViewModel
    {
        public Course Course { get; set; }
        public List<Category> Category { get; set; }
    }
}