using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestMovieSite.Domain.Models;
using TestMovieSite.Domain.Storage;
using File = TestMovieSite.Domain.Models.File;

namespace TestMovieSite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<File> Files { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Storage>().HasData(
                new Storage
                {
                    Id = 1,
                    Type = StorageType.LocalStorage,
                    IsDefault = true,
                    StoragePath = @"LocalStorage\"
                }
            );
            
            #region для теств пагинации
            // int moviesSize = 200000;
            // var movies = new Movie[moviesSize];
            // for (int i = 0; i < moviesSize; i++)
            // {
            //     movies[i] = new Movie
            //     {
            //         Id = i+1,
            //         Title = $"Movie №{i} title",
            //         Director = $"Movie №{i} director",
            //         Description = $"Movie №{i} description",
            //         PublishingDate = DateTime.Today
            //     };
            // }
            // modelBuilder.Entity<Movie>().HasData(movies);
            #endregion
        }
    }
}
