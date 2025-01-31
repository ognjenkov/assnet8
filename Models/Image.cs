using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Models
{
    public class Image
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Url { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Extension { get; set; } = string.Empty;
        public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;
        public Guid UserId { get; set; } // Foreign key to User
        public Guid? GalleryId { get; set; } // Foreign key to Gallery

        // Navigation property for ProfileImage (one-to-one)
        public User? ProfileImageUser { get; set; }

        // Navigation property for UploadedImages (one-to-many)
        public User? UploadedImagesUser { get; set; }

        public Gallery? Gallery { get; set; }
        public Field? Field { get; set; }
        public Listing? Listing { get; set; }
        public Organization? Organization { get; set; }
        public Service? Service { get; set; }
        public Team? Team { get; set; }
    }
}