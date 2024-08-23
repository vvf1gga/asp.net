using Kyrsova.Models;
using Microsoft.EntityFrameworkCore;


namespace Kyrsova.Data
{
    public class WebappDbContext : DbContext
    {
        public WebappDbContext(DbContextOptions<WebappDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }


    } 
}
