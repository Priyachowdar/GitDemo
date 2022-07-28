using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventcore.Models
{
    public class Event
    {
        internal object context;

       
        
        public int StudentId { get; set; }

        public string StudentName { get; set; }

        public int EventId { get; set; }

        public string EventName { get; set; }

        public string DepartmentId { get; set; }

        public int RegisteredDate { get; set; }

        public string EmailAddress { get; set; }

        public int Contact { get; set; }
        


      
    }
}
