using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Event.Models
{
    public class EmailForm
    {
        public int ID { get; set; }
        [Required, Display(Name = "Require your name")]
        public string FromName { get; set; }
        [Required, Display(Name = "Require reciever email")]
        public string ToEmail { get; set; }
        [Required, Display(Name = "Require your email"), EmailAddress]
        public string FromEmail { get; set; }
        [Required]
        public string Message { get; set; }
        public  virtual ICollection<PlanEvent> PlanEvents { get; set; }
    }
}