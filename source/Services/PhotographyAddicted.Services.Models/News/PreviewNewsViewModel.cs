using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Services.Models.News
{
    public class PreviewNewsViewModel
    {
        public IEnumerable<PreviewNewViewModel> PreviewNews { get; set; }

        public string Input { get; set; }
    }
}
