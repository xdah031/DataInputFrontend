using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInputt.Models
{
    class Project
    {
        public int Id { get; set; }
        public string Abstract { get; set; }
        public string Position { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public bool UntilToday { get; set; }
        public string Description { get; set; }
        public string[] Tasks { get; set; }
        public string[] Tools { get; set; }
        public string Sector { get; set; }
    }
}
