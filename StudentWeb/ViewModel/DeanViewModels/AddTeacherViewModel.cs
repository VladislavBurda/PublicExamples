using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentWeb.ViewModel.DeanViewModels
{
    public class AddTeacherViewModel
    {
        public string Title { get; set; }
        public int CourseId { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
    }
}
