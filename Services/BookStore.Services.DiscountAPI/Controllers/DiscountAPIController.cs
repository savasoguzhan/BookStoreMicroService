using AutoMapper;
using BookStore.Services.DiscountAPI.Data;
using BookStore.Services.DiscountAPI.Model;
using BookStore.Services.DiscountAPI.Model.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace BookStore.Services.DiscountAPI.Controllers
{
    [Route("api/discount")]
    [ApiController]
    //[Authorize]
    public class DiscountAPIController : ControllerBase
    {
        private readonly UygulamaDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<DiscountAPIController> _logger;
        private readonly ResponseDto _responseDto;

        public DiscountAPIController(UygulamaDbContext dbContext, IMapper mapper, ILogger<DiscountAPIController> logger)
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
                IEnumerable<Discount> objList = _dbContext.Discounts.ToList();
                _responseDto.Result = _mapper.Map<IEnumerable<DiscountDto>>(objList);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get All Discount Request failed : {ex.Message}");
                _responseDto.IsSuccess= false;
                _responseDto.Message=  ex.Message;
            }
            _logger.LogError($"Get All Discount Request Success : {_responseDto.Result}");
            return _responseDto;
        }



        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Discount objList = _dbContext.Discounts.First(d =>d.DiscountId == id);
                _responseDto.Result = _mapper.Map<DiscountDto>(objList);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get  Discount ById Request failed : {ex.Message}");
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            _logger.LogError($"Get Discount ById Request Successfull: {_responseDto.Result}");
            return _responseDto;
        }


        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                Discount objList = _dbContext.Discounts.FirstOrDefault(d => d.CouponCode.ToLower() == code.ToLower());
                if(objList == null)
                {
                    _responseDto.Result = false;
                }
                _responseDto.Result = _mapper.Map<DiscountDto>(objList);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get Discount ByCode Request failed : {ex.Message}");
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            _logger.LogError($"Get Discount ByCode Request failed : {_responseDto.Result}");
            return _responseDto;
        }


        [HttpPost]
        public ResponseDto Post([FromBody] DiscountDto discountDto)
        {
            try
            {
                Discount obj = _mapper.Map<Discount>(discountDto);
                _dbContext.Discounts.Add(obj);
                _dbContext.SaveChanges();

                _responseDto.Result=_mapper.Map<DiscountDto>(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Add New Discount Request Failed  : {ex.Message}");
                _responseDto.IsSuccess=false;
              
            }
            _logger.LogError($"Add New Discount Request Successfully : {_responseDto.Result}");
            return _responseDto;
        }


        [HttpPut]
        public ResponseDto Put([FromBody] DiscountDto discountDto)
        {
            try
            {

                Discount obj = _mapper.Map<Discount>(discountDto);
                _dbContext.Discounts.Update(obj);
                _dbContext.SaveChanges();

                _responseDto.Result = _mapper.Map<DiscountDto>(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update Discount Request failed : {ex.Message}");
                _responseDto.IsSuccess = false;

            }
            _logger.LogError($"Update Discount Request Successfully : {_responseDto.Result}");
            return _responseDto;
        }


        [HttpDelete]
        public ResponseDto Delete(int id)
        {
            try
            {
                Discount obj = _dbContext.Discounts.First(d =>d.DiscountId== id);
                _dbContext.Remove(obj);
                _dbContext.SaveChanges();

                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Remove Discount Request failed : {ex.Message}");
                _responseDto.IsSuccess = false;

            }
            _logger.LogError($"Remove Discount Request successfully : {_responseDto.Result}");
            return _responseDto;
        }

    }
}
