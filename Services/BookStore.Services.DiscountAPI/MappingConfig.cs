using AutoMapper;
using BookStore.Services.DiscountAPI.Model;
using BookStore.Services.DiscountAPI.Model.Dto;

namespace BookStore.Services.DiscountAPI
{
    public class MappingConfig 
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<DiscountDto, Discount>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
