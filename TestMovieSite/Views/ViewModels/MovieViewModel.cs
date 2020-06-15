using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using TestMovieSite.Domain.Models;

namespace TestMovieSite.Views.ViewModels
{
    public class MovieViewModel : BaseViewModel
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Director { get; set; }
        
        [Required]
        public string Description { get; set; }

        [Display(Name = "Publishing Date" )]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime PublishingDate { get; set; }
        
        [Display(Name = "Poster" )]
        public string PosterUrl { get; set; }

        [Display(Name = "Downloader")]
        public string DownloaderName { get; set; }
        
        public bool? IsCurrentUserDownloader { get; set; }
        
        [Display(Name = "Add a new poster")]
        [DataType(DataType.Upload)]
        public IFormFile  NewPoster { get; set; }

        public string ActionName { get; set; }

        public static MovieViewModel FromModel(Movie movie, bool? isCurrentUserDownloader = null)
        {
            return new MovieViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Director = movie.Director,
                Description = movie.Description,
                PublishingDate = movie.PublishingDate,
                PosterUrl = movie.Poster?.Url,
                DownloaderName = movie.Downloader?.UserName,
                IsCurrentUserDownloader = isCurrentUserDownloader
            };
        }
        
        public Movie ToModel()
        {
            return new Movie
            {
                Id = Id,
                Title = Title,
                Director = Director,
                Description = Description,
                PublishingDate = PublishingDate
            };
        }
    }
}
