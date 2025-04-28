using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Services.Images;

public interface ICloudImageService
{ // nacin na koji sam implementiraio awsimageservice nije dobar jer brizem iz moje baze i brisem iz clouda a trebam samo iz clouda, ovaj interfejs treba da se koristi u jos jednom servisu koji ce se zvati image service
    public Task<Image> UploadImage(User user, IFormFile imageFile);
    public Task<(byte[] ImageData, string ContentType)> GetImage(Image image);
    public Task DeleteImage(Image image);
}