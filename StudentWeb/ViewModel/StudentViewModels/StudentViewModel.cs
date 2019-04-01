using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentWeb.ViewModel.StudentViewModels
{
    public class StudentViewModel
    {
        public string Name { get; set; }
        public string SurrName { get; set; }

        public List<Course> Courses { get; set; }
    }
}
