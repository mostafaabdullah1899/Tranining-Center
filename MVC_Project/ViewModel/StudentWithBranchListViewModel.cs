using System.Collections.Generic;

namespace MVC_Project.ViewModel
{
    //Custum Class
    public class StudentWithBranchListViewModel
    {
        public string StdName { get; set; }
        public string StdAddress { get; set; }
        public int StdAge { get; set; }
        public List<string> branches { get; set; }
        public string msg { get; set; }

        public int temp { get; set; }
        public string color { get; set; }
    }
}
