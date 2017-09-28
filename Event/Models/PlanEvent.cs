using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Event.Models
{
    public class PlanEvent
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime StartDateTime { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int MaxCountGuest { get; set; }
        public Author EventAuthor { get; set; }
        public virtual ICollection<Guest> Guests { get; set; }
        public virtual ICollection<EmailForm> Emails { get; set; }
    }
}