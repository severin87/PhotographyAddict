using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PhotographyAddicted.Data.Common;
using PhotographyAddicted.Data.Models;
using PhotographyAddicted.Services.Models.Images;
using PhotographyAddicted.Web.Areas.Identity.Data;

namespace PhotographyAddicted.Services.DataServices.ImageService
{
    public class ImageService : IImageService
    {
        private readonly IRepository<Image> imageDbSet;
        private readonly IRepository<FavouriteImage> favouriteImageDbSet;
        private readonly IRepository<Favourite> favouriteDbSet;
        private IRepository<PhotographyAddictedUser> userDbSet;

        public ImageService(IRepository<Image> imageDbSet, IRepository<PhotographyAddictedUser> userDbset
            , IRepository<FavouriteImage> favouriteImageDbSet , IRepository<Favourite> favouriteDbSet)
        {
            this.imageDbSet = imageDbSet;
            this.userDbSet = userDbset;
            this.favouriteImageDbSet = favouriteImageDbSet;
            this.favouriteDbSet = favouriteDbSet;
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

            int byteCount = input.Picture.Length;
            
            if (byteCount > 265000)
            {
                return 0;
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
                    VotedUsers = p.VotedUsers,
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
                    VotedUsers = p.VotedUsers,
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
                    VotedUsers = d.VotedUsers,
                }).FirstOrDefault();

            return image;
        }

        public async Task DeleteImage(PreviewImageViewModel input)
        {
            await DeleteFavouriteImage(input.Id);

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
                    VotedUsers = u.VotedUsers,
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
                    VotedUsers = u.VotedUsers,
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
                    var currentImage = imageDbSet.All().Where(p => (int)p.ImageCategory == i)
                        .OrderByDescending(s => s.UploadedDate).Select(u => new PreviewImageViewModel
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
                        VotedUsers = u.VotedUsers,
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
                    VotedUsers = u.VotedUsers,
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
                    VotedUsers = u.VotedUsers,
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
                        VotedUsers = u.VotedUsers,
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

        public async Task AddImageToFavourites(string userId, int imageId)
        {
            var userFavorite = userDbSet.All().Where(u => u.Id == userId).FirstOrDefault().Favourite;
            var favoriteImage = (new FavouriteImage { FavouriteId = userFavorite.Id, ImageId = imageId });

            if (!favouriteImageDbSet.All().Contains(favoriteImage))
            {
                await favouriteImageDbSet.AddAsync(favoriteImage);
            }           

            await userDbSet.SaveChangesAsync();            
        }

        public async Task DeleteImageToFavourites(string userId, int imageId)
        {
            var userFavoriteImage = userDbSet.All().Where(u => u.Id == userId).FirstOrDefault().Favourite.FavouriteImages
                .Where(x => x.ImageId == imageId).First();

            favouriteImageDbSet.Delete(userFavoriteImage);
           
            await userDbSet.SaveChangesAsync();
        }

        public async Task AddFavorite(string userId)
        {
            var favourite = userDbSet.All().Where(u => u.Id == userId).FirstOrDefault().Favourite;

            if (favourite == null)
            {
                Favourite sev = new Favourite() { PhotographyAddictedUserId = userId };

                await favouriteDbSet.AddAsync(sev);
                await favouriteDbSet.SaveChangesAsync();
            }
        }

        public bool IsImageInFavouriteImage(string userId, int imageId)
        {
            var favourite = userDbSet.All().Where(u => u.Id == userId).FirstOrDefault().Favourite;
            var favouriteImage = favouriteImageDbSet.All().Where(x=>x.FavouriteId == favourite.Id && x.ImageId == imageId).FirstOrDefault();

            if (favouriteImage == null)
            {
                return false;
            }
            else
            {
                return true;
            }           
        }

        public PreviewImagesViewModel PreviewUserFavoriteImages(string userId)
        {
            var images = userDbSet.All().Where(i => i.Id == userId).FirstOrDefault().Favourite.FavouriteImages
                .Select(p => new PreviewImageViewModel
                {
                    Id = p.ImageId,
                    Title = p.Image.Title,
                    Picture = p.Image.Picture,
                    Scores = p.Image.Scores,
                    PhotographyAddictedUserId = p.Image.PhotographyAddictedUserId,
                    UploadedDate = p.Image.UploadedDate,
                })
                 .ToList();

            var userImages = new PreviewImagesViewModel()
            {
                PreviewImages = images,
            };

            return userImages;
        }

        public async Task AddImageScores(string userId, int imageId)
        {
            var userAverageScores = userDbSet.All().Where(i => i.Id == userId).FirstOrDefault().AverageScore;

            int userVoteScores = 0;

            if (userAverageScores >= 0 && userAverageScores <= 100)
            {
                userVoteScores += 1;
            }
            else if (userAverageScores > 100 && userAverageScores <= 200)
            {
                userVoteScores += 2;
            }
            else if (userAverageScores > 200 && userAverageScores <= 350)
            {
                userVoteScores += 3;
            }
            else if (userAverageScores > 350 && userAverageScores <= 600)
            {
                userVoteScores += 4;
            }
            else if (userAverageScores > 700 )
            {
                userVoteScores += 5;
            }

            var image = imageDbSet.All().Where(i => i.Id == imageId).FirstOrDefault();            

            if (!image.VotedUsers.Any(x => x.UserId == userId) && (image.PhotographyAddictedUserId != userId))
            {
                image.Scores += userVoteScores;

                var user = userDbSet.All().Where(i => i.Id == image.PhotographyAddictedUserId).FirstOrDefault();

                int userScores = user.Images.Select(x => x.Scores).Sum();

                user.AverageScore = userScores / user.Images.Count();

                image.VotedUsers.Add(new VotedUser { UserId = userId });

                await userDbSet.SaveChangesAsync();
            }
        }

        public async Task DeleteFavouriteImage(int imageId)
        {
            var favouriteImages = favouriteImageDbSet.All().Where(x => x.ImageId == imageId)
                 .ToList();

            foreach (var favouriteImage in favouriteImages)
            {
                favouriteImageDbSet.Delete(favouriteImage);
            }

            await favouriteImageDbSet.SaveChangesAsync();
        }

        public async Task DeleteFavouriteImage(string userId)
        {
            var userFavouriteId = userDbSet.All().Where(u => u.Id == userId).FirstOrDefault().Favourite.Id;
            var userFavouriteImageId = userDbSet.All().Where(u => u.Id == userId).FirstOrDefault().Images.Select(x => x.Id).ToList();

            var favouriteImages = favouriteImageDbSet.All().Where(x => x.FavouriteId == userFavouriteId)
                .ToList();

            foreach (var favouriteImage in favouriteImages)
            {
                favouriteImageDbSet.Delete(favouriteImage);
            }

            await favouriteImageDbSet.SaveChangesAsync();
            await userDbSet.SaveChangesAsync();
        }

        public PreviewImageViewModel PreviewImageOfTheDay()
        {
            var topImage = imageDbSet.All().Where(t => t.UploadedDate > DateTime.Now.AddDays(-30))
                .OrderByDescending(i => i.Scores).Select(i =>
                new PreviewImageViewModel
                {
                    Picture = i.Picture,
                    PhotographyAddictedUser = i.PhotographyAddictedUser,
                    Id = i.Id,
                    Title = i.Title,
                }).FirstOrDefault();

            return topImage;
        }    
    }
}
