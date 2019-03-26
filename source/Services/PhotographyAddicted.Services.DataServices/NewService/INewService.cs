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
        Task AddNew(AddNewViewModel input, IFormFile NewImage);

        IEnumerable<PreviewNewViewModel> PreviewAllNews();

        IEnumerable<PreviewNewViewModel> PreviewSearchedNews(string input);

        PreviewNewsViewModel PreviewNews(string input);

        PreviewNewViewModel ViewSpecificNew(int id);

        DeleteNewViewModel FindNewById(int Id);

        Task DeleteUserNew(DeleteNewViewModel input);

        UpdateNewViewModel FindUpdateNewById(int Id);

        Task<int> UpdateNew(UpdateNewViewModel input);
    }
}
