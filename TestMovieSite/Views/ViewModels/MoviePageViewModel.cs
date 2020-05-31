using System.Collections.Generic;

namespace TestMovieSite.Views.ViewModels
{
    public class MoviePageViewModel : PageViewModel
    {
        public ICollection<MovieViewModel> Movies;

        public MoviePageViewModel(int pageNumber, int pageSize, int totalPages, ICollection<MovieViewModel> movies) : base(pageNumber, pageSize, totalPages)
        {
            Movies = movies;
        }
    }
}
