using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentWeb.ViewModel.TeacherViewModels
{
    public class UserRateViewModel
    {
        public string Name { get; set; }
        public string SurrName { get; set; }
        public IEnumerable<int> Rate { get; set; }
        public int CourseId { get; set; }
        public string UserId { get; set; }
        [Range (1,5)]
        public int AddRate { get; set; }
        public string CourseName { get; set; }
    }
}
