using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace StudentWeb.ViewModel.HomeViewModels
{
    public class HomeViewModel
    {
        public IPagedList<Student> Students { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
        public IEnumerable<string> TeachersAllStudents { get; set; } //dean
        public IEnumerable<Student> StudentsAvarageRateAbove { get; set; } //dean
        public IEnumerable<string> TeachersLowStudents { get; set; } //den
    }
}
