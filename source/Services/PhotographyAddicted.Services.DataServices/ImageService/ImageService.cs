using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PhotographyAddicted.Data.Common;
using PhotographyAddicted.Data.Models;
using PhotographyAddicted.Services.Models.Images;
using PhotographyAddicted.Web.Areas.Identity.Data;

namespace PhotographyAddicted.Services.DataServices
{
    public class ImageService : IImageService
    {
        private readonly IRepository<Image> imageDbSet;
               
        public ImageService(IRepository<Image> imageDbSet )
        {
            this.imageDbSet = imageDbSet;
        }

        public async Task<int> AddImage(AddImageViewModel input, IFormFile Picture)
        {
            if (Picture.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await Picture.CopyToAsync(stream);
                    input.Picture = stream.ToArray();
                }
            }

            var newImage = new Image()
            {
                Title = input.Title,
                Picture = input.Picture,
                ImageCategory = input.ImageCategory,
                Description = input.Description,
                PhotographyAddictedUserId = input.PhotographyAddictedUserId,
                UploadedDate = DateTime.UtcNow,
                
            };
            await imageDbSet.AddAsync(newImage);
            await imageDbSet.SaveChangesAsync();

            return newImage.Id; 
        }

        public PreviewImageViewModel PreviewImage(int imageId)
        {
            var currentImage = imageDbSet.All().Where(i => i.Id == imageId)
                .Select(p => new PreviewImageViewModel
                {
                    Id = imageId,
                    Title = p.Title,
                    Picture = p.Picture,
                    Scores = p.Scores,
                    ImageCategory = p.ImageCategory,
                    PhotographyAddictedUser = p.PhotographyAddictedUser,
                    PhotographyAddictedUserId = p.PhotographyAddictedUserId,     
                    Description =p.Description,
                    ImageComments =p.ImageComments,
                    UploadedDate = p.UploadedDate,
                }).FirstOrDefault();

            return currentImage;
        }

        public async Task<int> UpdateImage(PreviewImageViewModel input)
        {
            var updateImageDbSet = imageDbSet.All().SingleOrDefault(t => t.Id == input.Id);

            updateImageDbSet.ImageCategory = input.ImageCategory;
            updateImageDbSet.Title = input.Title;
            updateImageDbSet.Description = input.Description;

            await imageDbSet.SaveChangesAsync();

            return updateImageDbSet.Id;
        }

        public IEnumerable<PreviewImageViewModel> GetImagesByUser(string userId)
        {
            var userImages = imageDbSet.All().Where(i => i.PhotographyAddictedUserId == userId)
                .Select( p => new PreviewImageViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Picture = p.Picture,
                    Scores = p.Scores,
                    ImageCategory = p.ImageCategory,
                    PhotographyAddictedUserId = p.PhotographyAddictedUserId,
                    UploadedDate = p.UploadedDate,
                })
                .ToList();

            return userImages;            
        }

        public PreviewImageViewModel FindImageById(int Id)
        {
            var image = imageDbSet.All().Where(x => x.Id == Id)
                .Select(d => new PreviewImageViewModel
                {
                    Id = d.Id,
                    PhotographyAddictedUserId = d.PhotographyAddictedUserId,
                    Title = d.Title,
                    Picture = d.Picture,
                    UploadedDate = d.UploadedDate,
                }).FirstOrDefault();

            return image;
        }

        public async Task DeleteImage(PreviewImageViewModel input)
        {
            var image = imageDbSet.All().Where(x => x.Id == input.Id).FirstOrDefault();

            imageDbSet.Delete(image);
            await imageDbSet.SaveChangesAsync();
        }

        public int GetImagesCount()
        {
            int count = this.imageDbSet.All().Count();

            return count;
        }

        public PreviewImagesViewModel PreviewImages(string input)
        {
            PreviewImagesViewModel images = new PreviewImagesViewModel();

            if (input == null)
            {
                images.PreviewImages = imageDbSet.All().Select(u =>
                new PreviewImageViewModel
                {
                    Title = u.Title,
                    Id = u.Id,
                    PhotographyAddictedUser = u.PhotographyAddictedUser,
                    PhotographyAddictedUserId = u.PhotographyAddictedUserId,
                    Description = u.Description,
                    ImageCategory = u.ImageCategory,
                    ImageComments = u.ImageComments,
                    Picture = u.Picture,
                    Scores = u.Scores,
                    UploadedDate = u.UploadedDate,
                });
            }
            else
            {
                images.PreviewImages = imageDbSet.All().Where(n => n.Title.Contains(input)).Select(u =>
                new PreviewImageViewModel
                {
                    Title = u.Title,
                    Id = u.Id,
                    PhotographyAddictedUser = u.PhotographyAddictedUser,
                    PhotographyAddictedUserId = u.PhotographyAddictedUserId,
                    Description = u.Description,
                    ImageCategory = u.ImageCategory,
                    ImageComments = u.ImageComments,
                    Picture = u.Picture,
                    Scores = u.Scores,
                    UploadedDate =u.UploadedDate,
                });
            }

            return images;
        }
    }
}
