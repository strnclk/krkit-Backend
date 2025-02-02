using Microsoft.EntityFrameworkCore;
using krkit_Backend.Models;
using System.Collections.Generic;

namespace krkit_Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
