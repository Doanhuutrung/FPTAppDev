using FPTAppDev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPTAppDev.ViewModel
{
    public class TrainerCourseViewModel
    {
        public Course Course { get; set; }
        public int CourseId { get; set; }
        public List<Course> Courses { get; set; }
        public string TrainerId { get; set; }
        public List<Trainer> Trainers { get; set; }
        public Trainer Trainer { get; set; }
    }
}