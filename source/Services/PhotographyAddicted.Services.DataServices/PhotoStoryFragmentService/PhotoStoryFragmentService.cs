using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public async Task<int> DeletePhotoStoryFragment(PreviewPhotoStoryFragmentViewModel input)
        {
            var photoStoryFragment = photoStoryFragmentDbSet.All().Where(x => x.Id == input.Id).FirstOrDefault();

            photoStoryFragmentDbSet.Delete(photoStoryFragment);

            await photoStoryFragmentDbSet.SaveChangesAsync();

            return (int)photoStoryFragment.PhotoStoryId;
        }

        public PreviewPhotoStoryFragmentViewModel FindPhotoStoryFragmenById(int PhotoStoryFragmentId)
        {
            var currentPhotoFragment = PreviewPhotoStoryFragment(PhotoStoryFragmentId);

            return currentPhotoFragment;
        }

        public PreviewPhotoStoryFragmentViewModel PreviewPhotoStoryFragment(int PhotoStoryFragmentId)
        {
            var currentPhotoFragment = photoStoryFragmentDbSet.All().Where(i => i.Id == PhotoStoryFragmentId)
                 .Select(p => new PreviewPhotoStoryFragmentViewModel
                 {
                     Picture = p.Picture,
                     Description = p.Description,
                     Id = p.Id,
                     PhotoStory = p.PhotoStory,
                     PhotoStoryId = p.PhotoStoryId,
                     Place = p.Place,
                 }).FirstOrDefault();

            return currentPhotoFragment;
        }

        public async Task<int> UpdatePhotoStoryFragment(PreviewPhotoStoryFragmentViewModel PhotoStoryFragment)
        {
            var updatePhotoStoryFragment = photoStoryFragmentDbSet.All().SingleOrDefault(t => t.Id == PhotoStoryFragment.Id);

            updatePhotoStoryFragment.Place = PhotoStoryFragment.Place;

            updatePhotoStoryFragment.Description = PhotoStoryFragment.Description;

            await photoStoryFragmentDbSet.SaveChangesAsync();

            return (int)updatePhotoStoryFragment.PhotoStoryId;
        }
    }
}
