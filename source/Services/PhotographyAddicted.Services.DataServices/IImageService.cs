using PhotographyAddicted.Services.Models.Images;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyAddicted.Services.DataServices
{
    public interface IImageService
    {
        IEnumerable<ImagePreviewViewModel> GetImagesByUser(string userId);

        ImagePreviewViewModel GetImageById(int imageId);

        Task<int> AddImage (AddImageViewModel input);
    }
}
