using BookStore.Web.Models;

namespace BookStore.Web.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDto> SendAsync(RequestDto requestDto,bool withBearer = true);
    }
}
