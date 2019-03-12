using AutoMapper;

namespace PhotographyAddicted.Services.Mapping
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}