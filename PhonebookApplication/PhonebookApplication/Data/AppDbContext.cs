using Microsoft.EntityFrameworkCore;
using PhonebookApplication.Models;
using System.Collections.Generic;

namespace PhonebookApplication.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
