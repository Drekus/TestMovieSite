using System;
using Microsoft.AspNetCore.Identity;

namespace TestMovieSite.Domain.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Description { get; set; }
        
        public DateTime PublishingDate { get; set; }
        public File Poster { get; set; }      
        public IdentityUser Downloader { get; set; }
    }
}
