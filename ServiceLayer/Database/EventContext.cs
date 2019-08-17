using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static ServiceLayer.DapperContribDemoService;

namespace ServiceLayer.Database
{
    public class EventContext : DbContext
    {
        public EventContext(DbContextOptions<EventContext> options)
        : base(options)
        { }

        public DbSet<Event> Events { get; set; }
        public DbSet<EventLocation> EventLocations { get; set; }
    }

    public class EventLocation
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public DateTime DateCreated { get; set; }
    }
    
}
