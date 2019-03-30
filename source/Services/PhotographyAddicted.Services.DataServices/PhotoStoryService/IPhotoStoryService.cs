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
        Task<int> AddPhotoStory(AddPhotoStoryViewModel input);

        PreviewPhotoStoriesViewModel PreviewPhotoStories(string input);

        PreviewPhotoStoryViewModel PreviewPhotoStory(int id);

        PreviewPhotoStoryViewModel FindPhotoStoryById(int id);

        Task ChangeStatus(int id);

        Task<int> UpdatePreviewPhotoStory(PreviewPhotoStoryViewModel PhotoStoryFragment);

        Task DeletePreviewPhotoStory(PreviewPhotoStoryViewModel input);
    }
}
