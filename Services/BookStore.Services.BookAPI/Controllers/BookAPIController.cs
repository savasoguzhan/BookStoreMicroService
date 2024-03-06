using AutoMapper;
using BookStore.Services.BookAPI.Data;
using BookStore.Services.BookAPI.Models;
using BookStore.Services.BookAPI.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace BookStore.Services.BookAPI.Controllers
{
    [Route("api/book")]
    [ApiController]
    //[Authorize]
    public class BookAPIController : ControllerBase
    {
        private readonly UygulamaDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<BookAPIController> _logger;
        private readonly ResponseDto _responseDto;

        public BookAPIController(UygulamaDbContext dbContext, IMapper mapper, ILogger<BookAPIController> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _responseDto = new ResponseDto();
        }



        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Book> objList = _dbContext.Books.ToList();
                _responseDto.Result = _mapper.Map<IEnumerable<BookDto>>(objList);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get Books Request failed: {ex.Message}");

                _responseDto.IsSuccess= false;
                _responseDto.Message=  ex.Message;
            }
            _logger.LogInformation($"Get All Book Request : {_responseDto.IsSuccess}");
            return _responseDto;
        }



        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Book objList = _dbContext.Books.First(d =>d.BookId == id);
                _responseDto.Result = _mapper.Map<BookDto>(objList);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get Book ById Request failed : {ex.Message}");
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            _logger.LogInformation($"Get  Book ById Request : {_responseDto.Result}");
            return _responseDto;
        }


        


        [HttpPost]
        public ResponseDto Post([FromBody] BookDto discountDto)
        {
            try
            {
                Book obj = _mapper.Map<Book>(discountDto);
                _dbContext.Books.Add(obj);
                _dbContext.SaveChanges();

                _responseDto.Result=_mapper.Map<BookDto>(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Add New  Book  Request failed : {ex.Message}");
                _responseDto.IsSuccess=false;
              
            }
            _logger.LogError($"Add new Book Successfuly  : {_responseDto.Result}");
            return _responseDto;
        }


        [HttpPut]
        public ResponseDto Put([FromBody] BookDto discountDto)
        {
            try
            {
                Book obj = _mapper.Map<Book>(discountDto);
                _dbContext.Books.Update(obj);
                _dbContext.SaveChanges();

                _responseDto.Result = _mapper.Map<BookDto>(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update Book  Request failed : {ex.Message}");
                _responseDto.IsSuccess = false;

            }
            _logger.LogError($"Update Book Request Successfully : {_responseDto.Result}");
            return _responseDto;
        }


        [HttpDelete]
        public ResponseDto Delete(int id)
        {
            try
            {
                Book obj = _dbContext.Books.First(d =>d.BookId== id);
                _dbContext.Remove(obj);
                _dbContext.SaveChanges();

                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Remove  Book  Request failed : {ex.Message}");
                _responseDto.IsSuccess = false;

            }
            _logger.LogError($"Remove Book : {_responseDto.Result}");
            return _responseDto;
        }

    }
}
