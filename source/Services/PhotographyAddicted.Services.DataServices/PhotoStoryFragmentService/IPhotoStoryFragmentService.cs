using Microsoft.AspNetCore.Http;
using PhotographyAddicted.Services.Models.PhotoStoryFragments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyAddicted.Services.DataServices.PhotoStoryFragmentService
{
    public interface IPhotoStoryFragmentService
    {
        Task<int> AddPhotoStoryFragment(PreviewPhotoStoryFragmentViewModel input, IFormFile Picture);

        PreviewPhotoStoryFragmentViewModel PreviewPhotoStoryFragment(int PhotoStoryFragmentId);

        PreviewPhotoStoryFragmentViewModel FindPhotoStoryFragmenById(int PhotoStoryFragmentId);

        Task<int> UpdatePhotoStoryFragment(PreviewPhotoStoryFragmentViewModel PhotoStoryFragment);

        Task<int> DeletePhotoStoryFragment(PreviewPhotoStoryFragmentViewModel input);
    }
}
