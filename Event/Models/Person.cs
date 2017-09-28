using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Event.Models
{
    public abstract class Person
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }
        public string Email { get; set; }
    }
}