using System.Collections.Generic;
using TestMovieSite.Domain.Storage;

namespace TestMovieSite.Domain.Models
{
    public class Storage
    {
        public int Id { get; set; }
        public StorageType Type { get; set; }
        public bool IsDefault { get; set; }
        public string StoragePath { get;  set; }
        
        public ICollection<File> Files { get;  set; } = new List<File>();
    }
}