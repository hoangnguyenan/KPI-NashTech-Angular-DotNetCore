using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Infrastructure.Data.ApplicationDbContext
{
    public class MovieDbContext : IdentityDbContext
    {
        public MovieDbContext()
        {
        }

        public MovieDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<MovieActor>()
                .HasKey(x => new { x.ActorId, x.MovieId });

            modelBuilder.Entity<MovieCategory>()
                .HasKey(x => new { x.CategoryId, x.MovieId });

            modelBuilder.Entity<MovieCinema>()
                .HasKey(x => new { x.CinemaId, x.MovieId });

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Cinema> Cinemas { get; set;}
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<MovieCategory> MovieCategories { get; set; }
        public DbSet<MovieCinema> MovieCinemas { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}
