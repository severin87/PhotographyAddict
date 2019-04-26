using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PhotographyAddicted.Data.Common;
using PhotographyAddicted.Data.Models;
using PhotographyAddicted.Services.Models.News;
using PhotographyAddicted.Services.Models.NewsComments;

namespace PhotographyAddicted.Services.DataServices
{
    public class NewService : INewService
    {
        private IRepository<New> newDbSet;

        public NewService(IRepository<New> newDbSet)
        {
            this.newDbSet = newDbSet;
        }

        public async Task<int> AddNew(AddNewViewModel input, IFormFile NewImage)
        {
            if (NewImage.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await NewImage.CopyToAsync(stream);
                    input.NewImage = stream.ToArray();
                }
            }

            int byteCount = input.NewImage.Length;

            if (byteCount > 265000)
            {
                return 0;
            }

            var currentNew = new New
            {
                Id =input.Id,
                PhotographyAddictedUserId = input.PhotographyAddictedUserId,
                NewImage = input.NewImage,
                Title = input.Title,
                TextContent = input.TextContent,  
                CreationDate = DateTime.UtcNow,
            };

            await newDbSet.AddAsync(currentNew);
            await newDbSet.SaveChangesAsync();

            return currentNew.Id;
        }
        
        public async Task DeleteNew(PreviewNewViewModel input)
        {
            var userNew = newDbSet.All().Where(x => x.Id == input.Id).FirstOrDefault();
            newDbSet.Delete(userNew);
            await newDbSet.SaveChangesAsync();                        
        }

        public PreviewNewsViewModel PreviewNews(string input)
        {
            PreviewNewsViewModel allNews = new PreviewNewsViewModel();          
          
            if (input == null)
            {
                allNews.PreviewNews = newDbSet.All().Select(u =>
                new PreviewNewViewModel
                {
                    CreationDate = u.CreationDate,
                    ComentsCount = u.NewComments.Count(),
                    NewImage = u.NewImage,
                    Title = u.Title,
                    Id = u.Id,
                    TextContent = u.TextContent,
                    PhotographyAddictedUserId = u.PhotographyAddictedUserId,

                });
            }
            else
            {
                allNews.PreviewNews = newDbSet.All().Where(n => n.Title.Contains(input)).Select(u =>
                new PreviewNewViewModel
                {
                    CreationDate = u.CreationDate,
                    ComentsCount = u.NewComments.Count(),
                    NewImage = u.NewImage,
                    Title = u.Title,
                    Id = u.Id,
                    TextContent = u.TextContent,
                    PhotographyAddictedUserId = u.PhotographyAddictedUserId,
                });
                allNews.Input = input;
            }

            return allNews;
        }

        public async Task UpdateNew(PreviewNewViewModel input)
        {
            var updateNew = newDbSet.All().SingleOrDefault(t => t.Id == input.Id);

            updateNew.TextContent = input.TextContent;
            updateNew.Title = input.Title;
            
            await newDbSet.SaveChangesAsync();
        }

        public PreviewNewViewModel PreviewNew(int id)
        {
            var specificNew = newDbSet.All()
               .Where(x => x.Id == id).Select(m => new PreviewNewViewModel
               {
                   Id = m.Id,
                   Title = m.Title,
                   PhotographyAddictedUserId = m.PhotographyAddictedUserId,
                   ComentsCount = m.NewComments.Count(),
                   CreationDate = m.CreationDate,
                   NewImage = m.NewImage,
                   NewComments = m.NewComments,
                   TextContent = m.TextContent,

               }).FirstOrDefault();

            return specificNew;
        }

        public int GetNewsCount()
        {
            int count = this.newDbSet.All().Count();

            return count;
        }

        public PreviewNewViewModel FindNewBy(int Id)
        {
            var userNew = newDbSet.All().Where(x => x.Id == Id)
                  .Select(d => new PreviewNewViewModel
                  {
                      Id = d.Id,
                      PhotographyAddictedUserId = d.PhotographyAddictedUserId,
                      Title = d.Title,
                      TextContent = d.TextContent,
                      NewImage = d.NewImage,
                  }).FirstOrDefault();

            return userNew;
        }
    }
}
