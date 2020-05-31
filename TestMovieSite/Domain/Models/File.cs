namespace TestMovieSite.Domain.Models
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        
        public int? StorageId { get; set; }
        public Storage Storage { get; set; }
    }
}