using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Event.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Event.DAL
{
    public class PlannerContext: DbContext
    {
        public PlannerContext(): base("PlannerContext")
        { }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<PlanEvent> PlanEvents { get; set; }
        public DbSet<EmailForm> EmailForms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}