using DataInputt.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataInputt
{
    public class DataContext : DbContext
    {
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Medium> Media { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        public DataContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=DataInputt.DataContext;Trusted_Connection=True;");
            }
        }
    }
}
