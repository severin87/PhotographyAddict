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

        PreviewNewViewModel ViewSpecificNew(int id);
    }
}
