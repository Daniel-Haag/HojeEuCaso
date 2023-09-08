using Microsoft.AspNetCore.Http;

namespace HojeEuCaso.Interfaces
{
    public interface ILoginService
    {
        public void RemoveSession(HttpContext HttpContext);
    }
}
