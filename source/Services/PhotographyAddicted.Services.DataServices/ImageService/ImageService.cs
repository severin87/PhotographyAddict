using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotographyAddicted.Data.Common;
using PhotographyAddicted.Data.Models;
using PhotographyAddicted.Services.Models.Images;
using PhotographyAddicted.Web.Areas.Identity.Data;

namespace PhotographyAddicted.Services.DataServices
{
    public class ImageService : IImageService
    {

        private readonly IRepository<Image> imageInfo;
               
        public ImageService(IRepository<Image> imageInfo )
        {
            this.imageInfo = imageInfo;
        }

        public async Task<int> AddImage(AddImageViewModel input)
        {
            var newImage = new Image()
            {
                Title = input.Title,
                Picture = input.Picture,
                ImageCategory = input.ImageCategory,
                Description = input.Description,
                PhotographyAddictedUserId = input.PhotographyAddictedUserId,
                UploadedDate = DateTime.UtcNow,
                
            };
            await imageInfo.AddAsync(newImage);
            await imageInfo.SaveChangesAsync();

            return newImage.Id; 
        }

        public ImagePreviewViewModel GetImageById(int imageId)
        {
            var currentImage = imageInfo.All().Where(i => i.Id == imageId)
                .Select(p => new ImagePreviewViewModel
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
                }).FirstOrDefault();

            return currentImage;
        }

        public async Task<int> UpdateImage(ImagePreviewViewModel input)
        {
            var updateImageInfo = imageInfo.All().SingleOrDefault(t => t.Id == input.Id);

            updateImageInfo.ImageCategory = input.ImageCategory;
            updateImageInfo.Title = input.Title;
            updateImageInfo.Description = input.Description;

            await imageInfo.SaveChangesAsync();

            return updateImageInfo.Id;
        }

        public IEnumerable<ImagePreviewViewModel> GetImagesByUser(string userId)
        {
            var usersImages = imageInfo.All().Where(i => i.PhotographyAddictedUserId == userId)
                .Select( p => new ImagePreviewViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Picture = p.Picture,
                    Scores = p.Scores,
                    ImageCategory = p.ImageCategory,
                    PhotographyAddictedUserId = p.PhotographyAddictedUserId,
                })
                .ToList();

            return usersImages;            
        }

        public DeleteImageViewModel FindDeletingImageById(int Id)
        {
            var image = imageInfo.All().Where(x => x.Id == Id)
                .Select(d => new DeleteImageViewModel
                {
                    Id = d.Id,
                    PhotographyAddictedUserId = d.PhotographyAddictedUserId,
                    Title = d.Title,
                    Picture = d.Picture,

                }).FirstOrDefault();

            return image;
        }

        public async Task DeleteImage(DeleteImageViewModel input)
        {
            var image = imageInfo.All().Where(x => x.Id == input.Id).FirstOrDefault();

            imageInfo.Delete(image);
            await imageInfo.SaveChangesAsync();
        }

        public int GetImagesCount()
        {
            int count = this.imageInfo.All().Count();

            return count;
        }
    }
}
