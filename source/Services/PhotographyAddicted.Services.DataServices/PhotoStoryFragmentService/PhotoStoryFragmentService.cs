using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PhotographyAddicted.Data.Common;
using PhotographyAddicted.Data.Models;
using PhotographyAddicted.Services.Models.PhotoStoryFragments;

namespace PhotographyAddicted.Services.DataServices.PhotoStoryFragmentService
{
    public class PhotoStoryFragmentService : IPhotoStoryFragmentService
    {
        private readonly IRepository<PhotoStoryFragment> photoStoryFragmentDbSet;

        public PhotoStoryFragmentService(IRepository<PhotoStoryFragment> photoStoryFragmentDbSet)
        {
            this.photoStoryFragmentDbSet = photoStoryFragmentDbSet;
        }

        public async Task<int> AddPhotoStoryFragment(PreviewPhotoStoryFragmentViewModel input, IFormFile Picture)
        {
            if (Picture.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await Picture.CopyToAsync(stream);
                    input.Picture = stream.ToArray();
                }
            }

            var photoStoryFragment = new PhotoStoryFragment()
            {  
                Picture = input.Picture,
                Description = input.Description,
                Place = input.Place,
                PhotoStoryId = input.PhotoStoryId,
            };

            await photoStoryFragmentDbSet.AddAsync(photoStoryFragment);
            await photoStoryFragmentDbSet.SaveChangesAsync();

            return (int)photoStoryFragment.PhotoStoryId;
        }
    }
}
