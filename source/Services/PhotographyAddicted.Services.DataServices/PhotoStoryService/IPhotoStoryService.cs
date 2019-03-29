using Microsoft.AspNetCore.Http;
using PhotographyAddicted.Data.Models;
using PhotographyAddicted.Services.Models.Images;
using PhotographyAddicted.Services.Models.PhotoStorys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyAddicted.Services.DataServices.PhotoStoryService
{
    public interface IPhotoStoryService
    {
        Task<int> AddPhotoStoryZ(AddPhotoStoryViewModel input, List<IFormFile> Picture);

        Task<int> AddPhotoStory(AddPhotoStoryViewModel input);

        PreviewPhotoStoriesViewModel PreviewPhotoStories(string input);
    }
}
