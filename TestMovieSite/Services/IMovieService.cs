using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using TestMovieSite.Domain.Helpers;
using TestMovieSite.Domain.Models;
using TestMovieSite.Views.ViewModels;

namespace TestMovieSite.Services
{
    public interface IMovieService
    {
        Task<MoviePageViewModel> GetMoviePage(int page);

        Task<OperationResult<Movie>> GetMovie(int id);

        Task<OperationResult<Movie>> AddOrEditMovie(Movie movie, IFormFile newPoster, IdentityUser currentUser);

        Task<bool> DeleteMovie(Movie removedMovie);
    }
}