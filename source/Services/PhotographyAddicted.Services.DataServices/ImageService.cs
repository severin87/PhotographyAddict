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
                    PhotographyAddictedUserId = p.PhotographyAddictedUserId,                 
                }).FirstOrDefault();

            return currentImage;
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
    }
}
