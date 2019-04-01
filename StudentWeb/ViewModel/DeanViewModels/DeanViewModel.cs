using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentWeb.ViewModel.DeanViewModels
{
    public class DeanViewModel
    {
        public int IdCourse { get; set; }
        public string Title { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
    }
}
