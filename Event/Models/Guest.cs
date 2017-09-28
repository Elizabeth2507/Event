using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Event.Models
{
    public class Guest: Person
    {
        public Guid ActivationCode { get; set; }
        public virtual ICollection<PlanEvent> PlanEvents { get; set; }

    }
}