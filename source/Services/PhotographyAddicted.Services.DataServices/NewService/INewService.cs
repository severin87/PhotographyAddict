using Microsoft.AspNetCore.Http;
using PhotographyAddicted.Services.Models.News;
using PhotographyAddicted.Services.Models.NewsComments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyAddicted.Services.DataServices
{
    public interface INewService
    {     
        PreviewNewsViewModel PreviewNews(string input);

        PreviewNewViewModel PreviewNew(int id);

        PreviewNewViewModel FindNewBy(int Id);

        Task<int> AddNew(AddNewViewModel input, IFormFile NewImage);

        Task UpdateNew(PreviewNewViewModel input);

        Task DeleteNew(PreviewNewViewModel input);

        int GetNewsCount();
    }
}
