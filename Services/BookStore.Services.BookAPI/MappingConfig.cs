using AutoMapper;
using BookStore.Services.BookAPI.Models;
using BookStore.Services.BookAPI.Models.Dtos;


namespace BookStore.Services.BookAPI
{
    public class MappingConfig 
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<BookDto, Book>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
