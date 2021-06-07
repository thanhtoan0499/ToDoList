using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models
{
    public class CVContext : DbContext
    {
        public CVContext(DbContextOptions<CVContext> options)
            :base(options)
        { }
        public DbSet<CV> Congviec { get; set; }
        public DbSet<NoiDungCV> NoiDungCV { get; set; }
        public const string ConnectStrring = "Server=DESKTOP-FAHIKA6\\SQLEXPRESS;Database=TODOLIST;Trusted_Connection=True;MultipleActiveResultSets=true";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectStrring);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CV>(x =>
            {
                x.HasKey(p => p.ID);
                x.Property(p => p.TenCV).IsRequired(true);
            });
            modelBuilder.Entity<NoiDungCV>(x =>
            {
                x.HasKey(e => e.ID);
            });

        }
    }
}
