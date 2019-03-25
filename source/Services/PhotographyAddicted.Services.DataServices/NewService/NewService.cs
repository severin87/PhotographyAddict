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

        public async Task AddNew(AddNewViewModel input, IFormFile NewImage)
        {
            if (NewImage.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await NewImage.CopyToAsync(stream);
                    input.NewImage = stream.ToArray();
                }
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

        }
        
        public async Task DeleteUserNew(DeleteNewViewModel input)
        {
            var userNew = newDbSet.All().Where(x => x.Id == input.Id).FirstOrDefault();
            newDbSet.Delete(userNew);
            await newDbSet.SaveChangesAsync();
                        
        }
        public DeleteNewViewModel FindNewById(int Id)
    {
            var userNew = newDbSet.All().Where(x => x.Id == Id)
                .Select(d => new DeleteNewViewModel
                {
                    Id = d.Id,
                    PhotographyAddictedUserId = d.PhotographyAddictedUserId,                    
                    NewImage = d.NewImage,
                    Title = d.Title,
                    TextContent = d.TextContent,
                    
                }).FirstOrDefault();

            return userNew;
        }

        public UpdateNewViewModel FindUpdateNewById(int Id)
        {
            var userNew = newDbSet.All().Where(x => x.Id == Id)
                 .Select(d => new UpdateNewViewModel
                 {
                     Id = d.Id,
                     PhotographyAddictedUserId = d.PhotographyAddictedUserId,
                     Title = d.Title,
                     TextContent = d.TextContent,

                 }).FirstOrDefault();

            return userNew;
        }

        public IEnumerable<PreviewNewViewModel> PreviewAllNews()
        {
            var news = newDbSet.All().Select(u => 
             new PreviewNewViewModel
             {
                 CreationDate = u.CreationDate,
                 ComentsCount = u.NewComments.Count(),
                 NewImage = u.NewImage,
                 Title = u.Title,
                 Id = u.Id,
                 TextContent = u.TextContent,
             });

            return news;
        }

        public async Task<int> UpdateNew(UpdateNewViewModel input)
        {
            var updateNew = newDbSet.All().SingleOrDefault(t => t.Id == input.Id);

            updateNew.TextContent = input.TextContent;
            updateNew.Title = input.Title;
            
            await newDbSet.SaveChangesAsync();

            return updateNew.Id;
        }

        public PreviewNewViewModel ViewSpecificNew(int id)
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
    }
}
