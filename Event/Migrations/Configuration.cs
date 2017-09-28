namespace Event.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Event.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<Event.DAL.PlannerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Event.DAL.PlannerContext context)
        {
            var planEvents = new List<PlanEvent>
            {
                new PlanEvent { Title = "Dinner", StartDateTime = DateTime.Parse("2017-09-05"), Description = "First Dinner at month",
                Location = "Kalvariyskay 7",  MaxCountGuest = 3 },
                new PlanEvent { Title = "Time travel", StartDateTime = DateTime.Parse("2017-09-17"), Description = "It’s bigger on the inside than the outside",
                Location = "TARDIS",  MaxCountGuest = 2 }
            };
            planEvents.ForEach(s => context.PlanEvents.AddOrUpdate(p => p.Title, s));
            context.SaveChanges();

        }
    }
}
