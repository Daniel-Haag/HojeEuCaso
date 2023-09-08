using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HojeEuCaso.Interfaces;

namespace HojeEuCaso.Services
{
    public class LoginService : ILoginService
    {
        public LoginService() { }

        public void RemoveSession(HttpContext HttpContext)
        {
            HttpContext.Session.Remove("Nome");
            HttpContext.Session.Remove("Role");
        }
    }
}
