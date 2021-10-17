using Microsoft.AspNetCore.Http;
using SnachPat.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SnachPat.Services
{
    public class PhotoService
    {
        private readonly AppDBContext _context;
        public PhotoService(AppDBContext context)
        {
            _context = context;
        }

        public bool AddPhoto(IFormFile photo)
        {
            if (photo is null
                || string.IsNullOrEmpty(photo.FileName)
                && (!photo.FileName.EndsWith(".jpg")
                || !photo.FileName.EndsWith(".PNG")
                || !photo.FileName.EndsWith(".png")))
            {
                Console.WriteLine("Parameter for adding photo is empty");
                return false;
            }
            try
            {
                string path = SavePhoto(photo);
                var photoChanged = photoToDB(photo, path);
                _context.Add(photoChanged);
                _context.SaveChanges();
                Console.WriteLine("Photo added succesful");
            }
            catch (Exception e)
            {
                Console.WriteLine("Add photo exc " + e.Message);
                throw;
            }

            return false;
        }
        private PhotoModel photoToDB(IFormFile photo, string path) => new PhotoModel { photoPath = path, photoName = photo.FileName, DateOfAdd = DateTime.Now };
        private string SavePhoto(IFormFile file)
        {
            string path = Path.GetDirectoryName(@"C:\Users\sonye\Source\Repos\SnachPat\SnachPat\wwwroot\Images\first");

            string appPath = path + @$"\{file.FileName}";

            using (var stream = File.Create(appPath))
            {
                file.CopyTo(stream);
                stream.Close();
            }

            return path;
        }

        public string GetNewestPhotoPath()
        {
            var photo = _context.photos.Where(s => s.DateOfAdd == _context.photos.Max(x => x.DateOfAdd))
                        .FirstOrDefault();
            if (photo.photoPath.Contains("wwwroot"))
            {
                var value = photo.photoPath.Split("wwwroot");
                string path = "~" + value[value.Length - 1].Replace('\\', '/') + "/" + photo.photoName;
                return path;
            }
            return null;
        }

    }
}
