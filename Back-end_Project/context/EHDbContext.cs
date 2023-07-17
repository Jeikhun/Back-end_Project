using Back_end_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Back_end_Project.context
{
    public class EHDbContext:DbContext
    {
        public DbSet<Slide>? slides { get; set; }
        public DbSet<Notice>? notices { get; set; }
        public DbSet<Course>? courses { get; set; }
        public DbSet<Information>? information { get; set; }
        public DbSet<Person>? people { get; set; }

        public EHDbContext(DbContextOptions<EHDbContext> options) : base(options) 
        { 

        }
    }
}
