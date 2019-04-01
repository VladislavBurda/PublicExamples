using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentWeb.ViewModel.StudentViewModels
{
    public class Course
    {
        public string CourseName { get; set; }
        public IEnumerable<int> Rate { get; set; }
        public double AverageRate { get; set; }
    }
}
