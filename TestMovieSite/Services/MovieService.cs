using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestMovieSite.Data;
using TestMovieSite.Domain.Helpers;
using TestMovieSite.Domain.Models;
using TestMovieSite.Domain.Storage;
using TestMovieSite.DTO;
using TestMovieSite.Views.ViewModels;
using File = TestMovieSite.Domain.Models.File;

namespace TestMovieSite.Services
{
    public class MovieService: IMovieService
    {
        private readonly ApplicationDbContext _db;
        private readonly StorageFactory _storageFactory;
        private readonly ILogger<MovieService> _logger;
        private const int MOVIES_PER_PAGE = 5;
    
        public MovieService(ApplicationDbContext db, ILogger<MovieService> logger, StorageFactory storageFactory)
        {
            _db = db;
            _logger = logger;
            _storageFactory = storageFactory;
        }

        public async Task<MoviePageViewModel> GetMoviePage(int page)
        {
            IQueryable<Movie> source = _db.Movies.Include(m=>m.Downloader);
            var count = await source.CountAsync();

            if(count == 0)
            {
                return new MoviePageViewModel(pageNumber:1, MOVIES_PER_PAGE, totalPages:1, new List<MovieViewModel>());
            }

            var totalPages = (int)Math.Ceiling(count / (double)MOVIES_PER_PAGE);
            if (page < 1)
            {
                page = 1;
            }
            else if (page > totalPages)
            {
                page = totalPages;
            }

            var movies = await source
                .Skip((page - 1) * MOVIES_PER_PAGE)
                .Take(MOVIES_PER_PAGE)
                .Select(m=>MovieViewModel.FromModel(m, null))
                .ToListAsync();
            var pageViewModel = new MoviePageViewModel(page, MOVIES_PER_PAGE, totalPages, movies);
            return pageViewModel;
        }

        public async Task<OperationResult<Movie>> GetMovie(int id)
        {
            try
            {
               var movie = await _db.Movies
                    .Include(m => m.Downloader)
                    .Include(m => m.Poster)
                    .FirstOrDefaultAsync(m => m.Id == id);
               return new OperationResult<Movie>(movie, isSuccess: true);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to find the movie with id: {id}");
                return new OperationResult<Movie>(data:null, isSuccess: false);
            }
        }
        
        public async Task<OperationResult<Movie>> AddOrEditMovie(Movie movie, IFormFile newPoster, IdentityUser currentUser)
        {
            try
            {
                if (newPoster != null)
                {
                    var result = await AddNewPoster(newPoster);
                    if (!result.IsSuccess)
                    {
                        return new OperationResult<Movie>(data:null, isSuccess: false);
                    }
                    movie.Poster = result.Data;
                }

                movie.Downloader = currentUser;
                if (movie.Id == 0)
                {
                    _db.Movies.Add(movie);
                }
                else
                {
                    var result = await GetMovie(movie.Id);
                    if (!result.IsSuccess)
                    {
                        return new OperationResult<Movie>(data: movie, isSuccess: false); ;
                    }

                    var dbMovie = result.Data;
                    dbMovie.Title = movie.Title;
                    dbMovie.Description = movie.Description;
                    dbMovie.Director = movie.Director;
                    dbMovie.PublishingDate = movie.PublishingDate;

                    if (newPoster != null)
                    {
                        _db.Files.Remove(dbMovie.Poster);
                        dbMovie.Poster = movie.Poster;
                    }
                }
                await _db.SaveChangesAsync();
                return new OperationResult<Movie>(data:movie, isSuccess: true);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to add a new movie.");
                return new OperationResult<Movie>(data:null, isSuccess: false);
            }

        }
        
        public async Task<bool> DeleteMovie(Movie removedMovie)
        {
            try
            {
                _db.Movies.Remove(removedMovie);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to delete a movie.");
                return false;
            }
        }

        private async Task<OperationResult<File>> AddNewPoster(IFormFile newPoster)
        {
            var fileDto = new FileDto { Name = newPoster.FileName };
            using var binaryReader = new BinaryReader(newPoster.OpenReadStream());
            fileDto.FileData = binaryReader.ReadBytes((int)newPoster.Length);
            var storage = await _storageFactory.GetDefaultStorage();
            var result = storage.TryUploadFile(fileDto);
            if (!result.IsSuccess)
            {
                return new OperationResult<File>(data:null, isSuccess: false);
            }
            var file = new File
            {
                Name = newPoster.FileName,
                Url = result.Data,
                StorageId = storage.Id
            };
            // await _db.Files.AddAsync(file);
            // await _db.SaveChangesAsync();
            return new OperationResult<File>(data:file, isSuccess: true);
        }
    }
}