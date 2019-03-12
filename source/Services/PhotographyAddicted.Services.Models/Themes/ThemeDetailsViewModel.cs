using AutoMapper;
using PhotographyAddicted.Data.Models;
using PhotographyAddicted.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Services.Models.Themes
{
    public class ThemeDetailsViewModel : IMapFrom<Theme>,IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorOpinion { get; set; }

        public string UserName { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            //throw new NotImplementedException();
        }
    }
}
