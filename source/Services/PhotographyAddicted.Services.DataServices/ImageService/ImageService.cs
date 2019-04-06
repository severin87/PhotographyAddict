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

        private IRepository<PhotographyAddictedUser> userDbset;

        public ImageService(IRepository<Image> imageDbSet, IRepository<PhotographyAddictedUser> userDbset)
        {
            this.imageDbSet = imageDbSet;
            this.userDbset = userDbset;
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
                Equipment = input.Equipment,
                Settings = input.Settings,
                
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
                    Equipment = p.Equipment,
                    Settings = p.Settings,
                }).FirstOrDefault();

            return currentImage;
        }

        public async Task<int> UpdateImage(PreviewImageViewModel input)
        {
            var updateImageDbSet = imageDbSet.All().SingleOrDefault(t => t.Id == input.Id);

            updateImageDbSet.ImageCategory = input.ImageCategory;
            updateImageDbSet.Title = input.Title;
            updateImageDbSet.Description = input.Description;
            updateImageDbSet.Equipment = input.Equipment;
            updateImageDbSet.Settings = input.Settings;

            await imageDbSet.SaveChangesAsync();

            return updateImageDbSet.Id;
        }

        public PreviewImagesViewModel PreviewUserImages(string userId)
        {
            var images = imageDbSet.All().Where(i => i.PhotographyAddictedUserId == userId)
                .Select( p => new PreviewImageViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Picture = p.Picture,
                    Scores = p.Scores,
                    PhotographyAddictedUserId = p.PhotographyAddictedUserId,
                    UploadedDate = p.UploadedDate,
                })
                .ToList();

            var userImages = new PreviewImagesViewModel()
            {
                PreviewImages = images,
            };

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
                    ImageCategory = d.ImageCategory,
                    Description = d.Description,
                    Equipment = d.Equipment,
                    Settings = d.Settings,
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
                    Equipment = u.Equipment,
                    Settings = u.Settings,
                }).OrderByDescending(d => d.UploadedDate);
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
                    Equipment = u.Equipment,
                    Settings = u.Settings,
                }).OrderByDescending(d => d.UploadedDate);
            }

            return images;
        }

        public PreviewImagesViewModel PreviewImagesByCategoriesAndDates()
        {
            var images = new List<PreviewImageViewModel>();

            for (int i = 1; i < 13; i++)
            {
                var checkCategory = imageDbSet.All().Where(p => (int)p.ImageCategory == i);
                if (checkCategory.Count()!=0)
                {
                    var currentImage = imageDbSet.All().Where(p => (int)p.ImageCategory == i).OrderByDescending(s => s.UploadedDate).Select(u => new PreviewImageViewModel
                    {
                        Id = u.Id,
                        PhotographyAddictedUser = u.PhotographyAddictedUser,
                        PhotographyAddictedUserId = u.PhotographyAddictedUserId,
                        Description = u.Description,
                        ImageCategory = u.ImageCategory,
                        ImageComments = u.ImageComments,
                        Picture = u.Picture,
                        Scores = u.Scores,
                        UploadedDate = u.UploadedDate,
                        Equipment = u.Equipment,
                        Settings = u.Settings,
                    }).First();

                    images.Add(currentImage);
                }                   
            }

            var imagesByCategory = new PreviewImagesViewModel()
            {
                PreviewImages = images,
            };

            return imagesByCategory;
        }

        public PreviewImagesViewModel PreviewCategoryImages(int category)
        {
            var images = imageDbSet.All().Where(i => (int)i.ImageCategory == category).OrderByDescending(d => d.UploadedDate)
                .Select(u => new PreviewImageViewModel
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
                    Equipment = u.Equipment,
                    Settings = u.Settings,
                }).ToList();

            var imagesByCategory = new PreviewImagesViewModel()
            {
                PreviewImages = images,
            };

            return imagesByCategory;
        }

        public PreviewImagesViewModel PreviewTopImagesLasтThirtyDays(int category)
        {
            var images = imageDbSet.All().Where(i => (int)i.ImageCategory == category)
                .Where(t => t.UploadedDate > DateTime.Now.AddDays(-30)).OrderByDescending(d => d.Scores)
                .Select(u => new PreviewImageViewModel
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
                    Equipment = u.Equipment,
                    Settings = u.Settings,
                }).ToList();

            var imagesByCategory = new PreviewImagesViewModel()
            {
                PreviewImages = images,
            };

            return imagesByCategory;
        }

        public PreviewImagesViewModel PreviewTopImagesLasтThirtyDaysByCategory()
        {
            var images = new List<PreviewImageViewModel>();

            for (int i = 1; i < 13; i++)
            {
                var checkCategory = imageDbSet.All().Where(p => (int)p.ImageCategory == i).Where(t => t.UploadedDate > DateTime.Now.AddDays(-30));
                if (checkCategory.Count() != 0)
                {
                    var currentImage = imageDbSet.All().Where(p => (int)p.ImageCategory == i)
                        .Where(t => t.UploadedDate > DateTime.Now.AddDays(-30)).OrderByDescending(s => s.Scores).Select(u => new PreviewImageViewModel
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
                        Equipment = u.Equipment,
                        Settings = u.Settings,
                        }).First();

                    images.Add(currentImage);
                }
            }

            var imagesByCategory = new PreviewImagesViewModel()
            {
                PreviewImages = images,
            };

            return imagesByCategory;
        }

        public async Task<int> AddImageToFavourites(string userId, int imageId)
        {
            var image = imageDbSet.All().Where(x => x.Id == imageId).FirstOrDefault();

            var user = userDbset.All().Where(u => u.Id == userId).FirstOrDefault();

            user.Favourite.FavouriteImages.Add(new FavouriteImage { Image = image});
           
            await userDbset.SaveChangesAsync();

            return image.Id;
        }
    }
}
