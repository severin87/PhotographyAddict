using Microsoft.AspNetCore.Http;
using PhotographyAddicted.Services.Models.Images;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyAddicted.Services.DataServices
{
    public interface IImageService
    {
        PreviewImagesViewModel PreviewImages(string input);

        PreviewImagesViewModel PreviewUserImages(string userId);

        PreviewImagesViewModel PreviewImagesByCategoriesAndDates();

        PreviewImagesViewModel PreviewCategoryImages(int category);

        PreviewImagesViewModel PreviewTopImagesLasтThirtyDaysByCategory();

        PreviewImagesViewModel PreviewTopImagesLasтThirtyDays(int category);

        PreviewImageViewModel PreviewImage(int imageId);

        PreviewImageViewModel FindImageById(int Id);

        Task<int> AddImage(AddImageViewModel input, IFormFile ProfilePicture);

        Task<int> UpdateImage(PreviewImageViewModel input);

        Task DeleteImage(PreviewImageViewModel input);

        int GetImagesCount(); 
    }
}
