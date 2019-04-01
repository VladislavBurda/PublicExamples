using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentWeb.ViewModel.TeacherViewModels
{
    public class AddStudentViewModel
    {
        public int IdCourse { get; set; }
        public string Title { get; set; }
        public List<Student> Students { get; set; }
    }
}
