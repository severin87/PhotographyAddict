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
using PhotographyAddicted.Services.Models.PhotoStorys;

namespace PhotographyAddicted.Services.DataServices.PhotoStoryService
{
    public class PhotoStoryService : IPhotoStoryService
    {
        private readonly IRepository<PhotoStory> photoStoryDbSet;

        public PhotoStoryService(IRepository<PhotoStory> photoStoryDbSet)
        {
            this.photoStoryDbSet = photoStoryDbSet;
        }

        public async Task<int> AddPhotoStory(AddPhotoStoryViewModel input)
        {
            var photoStory = new PhotoStory
            {
                PhotographyAddictedUserId = input.PhotographyAddictedUserId,
                Introduction = input.Introduction,
                Title = input.Title,
                UploadedDate = DateTime.UtcNow,
                Author = input.Author,
                Published = input.Published,
            };

            await photoStoryDbSet.AddAsync(photoStory);
            await photoStoryDbSet.SaveChangesAsync();

            return photoStory.Id;
        }

        public async Task<int> AddPhotoStoryZ(AddPhotoStoryViewModel input, List<IFormFile> Picture)
        {
            for (int i = 0; i < Picture.Count; i++)
            {
                if (Picture[i].Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await Picture[i].CopyToAsync(stream);
                        // input.PhotoStoryFragments[i].Picture = stream.ToArray();
                    }
                }
            }

            var photoStory = new PhotoStory()
            {
                Title = input.Title,
                Author = input.Author,
                Conclusion = input.Conclusion,
                Introduction = input.Introduction,
                //  PhotoStoryFragments = input.PhotoStoryFragments,
                PhotographyAddictedUserId = input.PhotographyAddictedUserId,
                PhotographyAddictedUser = input.PhotographyAddictedUser,
                UploadedDate = DateTime.UtcNow,

            };

            await photoStoryDbSet.AddAsync(photoStory);
            await photoStoryDbSet.SaveChangesAsync();

            return photoStory.Id;
        }

        public PreviewPhotoStoriesViewModel PreviewPhotoStories(string input)
        {
            PreviewPhotoStoriesViewModel photoStory = new PreviewPhotoStoriesViewModel();

            if (input == null)
            {
                photoStory.PreviewPhotoStories = photoStoryDbSet.All().Select(u =>
                new PreviewPhotoStoryViewModel
                {
                    Title = u.Title,
                    Id = u.Id,
                    PhotographyAddictedUser = u.PhotographyAddictedUser,
                    Published = u.Published,
                    UploadedDate = u.UploadedDate,
                    PhotoStoryFragments = u.PhotoStoryFragments
                });
            }
            else
            {
                photoStory.PreviewPhotoStories = photoStoryDbSet.All().Where(n => n.Title.Contains(input)).Select(u =>
               new PreviewPhotoStoryViewModel
               {
                   Title = u.Title,
                   Id = u.Id,
                   PhotographyAddictedUser = u.PhotographyAddictedUser,
                   Published = u.Published,
                   UploadedDate = u.UploadedDate,
                   PhotoStoryFragments = u.PhotoStoryFragments
               });
            }

            return photoStory;
        }

        public PreviewPhotoStoryViewModel PreviewPhotoStory(int id)
        {
            var photoStory = photoStoryDbSet.All().Where(x => x.Id == id).Select(m =>
            new PreviewPhotoStoryViewModel
            {
                Id = m.Id,
                Title = m.Title,
                PhotographyAddictedUserId = m.PhotographyAddictedUserId,
                PhotographyAddictedUser = m.PhotographyAddictedUser,
                Author = m.Author,
                PhotoStoryFragments = m.PhotoStoryFragments,
                Conclusion = m.Conclusion,
                UploadedDate = m.UploadedDate,
                Introduction = m.Introduction,
                PhotoStoryComments = m.PhotoStoryComments,
                Published = m.Published,
            }).FirstOrDefault();

            return photoStory;
        }
              

        public async Task ChangeStatus(int id)
        {
            bool publish = photoStoryDbSet.All().Where(x => x.Id == id).FirstOrDefault().Published;

            if (publish == false)
            {
                var photoStory = photoStoryDbSet.All().Where(x => x.Id == id).FirstOrDefault();
                photoStory.Published = true;
                await photoStoryDbSet.SaveChangesAsync();
            }
            else
            {
                var photoStory = photoStoryDbSet.All().Where(x => x.Id == id).FirstOrDefault();
                photoStory.Published = false;
                await photoStoryDbSet.SaveChangesAsync();
            }
        }
    }
}
