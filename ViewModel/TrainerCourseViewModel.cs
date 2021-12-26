using FPTAppDev.Models;
using System.Collections.Generic;

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