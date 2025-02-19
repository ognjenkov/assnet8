using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Models
{
    public class Image
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required Guid S3Id { get; set; }
        public required string Title { get; set; }
        public required string Extension { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;
        public Guid UserId { get; set; }
        public Guid? GalleryId { get; set; }
        public User? ProfileImageUser { get; set; }
        public User? UploadedImagesUser { get; set; }
        public Gallery? Gallery { get; set; }
        public Field? Field { get; set; }
        public Listing? Listing { get; set; }
        public Organization? Organization { get; set; }
        public Service? Service { get; set; }
        public Team? Team { get; set; }
    }
}