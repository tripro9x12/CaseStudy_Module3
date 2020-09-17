using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAnime.Models.Entities;

namespace TAnime.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieCategory> MovieCategories { get; set; }
        public DbSet<Episode> Episodes  { get; set; }
        public DbSet<Country> Countries { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MovieCategory>().HasKey(mc => new { mc.MovieId, mc.CategoryId });
            modelBuilder.Entity<MovieCategory>()
            .HasOne<Movie>(mc => mc.Movie)
            .WithMany(m => m.movieCategories)
            .HasForeignKey(mc => mc.MovieId);
            modelBuilder.Entity<MovieCategory>()
            .HasOne<Category>(mc => mc.Category)
            .WithMany(m => m.movieCategories)
            .HasForeignKey(mc => mc.CategoryId);
        }
    }
}
