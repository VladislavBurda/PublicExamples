using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        public string Title { get; set; }

        public ICollection<UserCourse> UserCourses { get; set; }
    }
}
