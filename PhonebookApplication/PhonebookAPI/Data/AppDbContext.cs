﻿using Microsoft.EntityFrameworkCore;
using PhonebookAPI.Models;

namespace PhonebookAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Contact> Contacts { get; set; }
    }
}
