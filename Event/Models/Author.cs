using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Event.Models
{
    public class Author: Person
    {
        public virtual ICollection<PlanEvent> PlanEvents { get; set; }
        
    }
}