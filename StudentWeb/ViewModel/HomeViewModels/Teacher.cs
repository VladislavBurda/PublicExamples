using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentWeb.ViewModel.HomeViewModels
{
    public class Teacher
    {
        public string Name { get; set; }
        public int CountStudent { get; set; }
        public List<string> CourseName { get; set; }
    }
}
