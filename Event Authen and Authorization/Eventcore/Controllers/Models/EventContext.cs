using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Eventcore.Models
{
    public class EventContext : IdentityDbContext
    {
        public EventContext(DbContextOptions<EventContext> options) : base(options)
        {

        }
        public DbSet<Event> EventRegister { get; set; }
       
        public object Event { get; internal set; }
    }
}
