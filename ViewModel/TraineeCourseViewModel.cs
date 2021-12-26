using FPTAppDev.Models;
using System.Collections.Generic;

namespace FPTAppDev.ViewModel
{
    public class TraineeCourseViewModel
    {
        public Course Course { get; set; }
        public int CourseId { get; set; }
        public List<Course> Courses { get; set; }
        public string TraineeId { get; set; }
        public List<Trainee> Trainees { get; set; }
        public Trainee Trainee { get; set; }
    }
}