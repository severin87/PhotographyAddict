using Microsoft.AspNetCore.Http;
using PhotographyAddicted.Services.Models.Images;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyAddicted.Services.DataServices.ImageService
{
    public interface IImageService
    {
        PreviewImagesViewModel PreviewImages(string input);

        PreviewImagesViewModel PreviewUserImages(string userId);

        PreviewImagesViewModel PreviewUserFavoriteImages(string userId);

        PreviewImagesViewModel PreviewImagesByCategoriesAndDates();

        PreviewImagesViewModel PreviewCategoryImages(int category);

        PreviewImagesViewModel PreviewTopImagesLasтThirtyDaysByCategory();

        PreviewImagesViewModel PreviewTopImagesLasтThirtyDays(int category);

        PreviewImageViewModel PreviewImage(int imageId);

        PreviewImageViewModel FindImageById(int Id);

        Task<int> AddImage(AddImageViewModel input, IFormFile Picture);

        Task AddImageToFavourites(string userId, int imageId);

        Task DeleteImageToFavourites(string userId, int imageId);

        bool IsImageInFavouriteImage(string userId, int imageId);

        Task AddFavorite(string userId);

        Task<int> UpdateImage(PreviewImageViewModel input);

        Task DeleteImage(PreviewImageViewModel input);

        int GetImagesCount(); 
    }
}
