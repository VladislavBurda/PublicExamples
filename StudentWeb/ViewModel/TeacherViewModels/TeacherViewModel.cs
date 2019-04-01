using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentWeb.ViewModel.TeacherViewModels
{
    public class TeacherViewModel
    {
        public int IdCourse { get; set; }
        public string Title { get; set; }
        public IEnumerable<Student> Students { get; set; }
    }
}
