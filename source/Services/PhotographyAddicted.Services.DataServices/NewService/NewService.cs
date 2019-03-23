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
                PhotographyAddictedUserId = input.PhotographyAddictedUserId,
                NewImage = input.NewImage,
                Title = input.Title,
                TextContent = input.TextContent,  
                CreationDate = DateTime.UtcNow,
            };

            await newDbSet.AddAsync(currentNew);
            await newDbSet.SaveChangesAsync();

        }

        public IEnumerable<PreviewNewViewModel> PreviewAllNews()
        {
            var news = newDbSet.All().Select(u => 
             new PreviewNewViewModel
             {
                 CreationDate = u.CreationDate,
                 ComentsCount = u.ComentsCount,
                 NewImage = u.NewImage,
                 Title = u.Title,
                 Id = u.Id,
                 TextContent = u.TextContent,
             });

            return news;
        }
    }
}
