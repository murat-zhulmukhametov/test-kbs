using System;
using System.Collections.Generic;
using System.Text;

namespace KBSTestTask.Shared
{
    public class City
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CitizensCount { get; set; }
        public DateTime FoundationDate { get; set; }
    }
}
