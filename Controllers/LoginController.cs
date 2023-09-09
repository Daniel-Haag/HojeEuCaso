using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HojeEuCaso.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HojeEuCaso.Sessions;
using Microsoft.AspNetCore.Http;
using HojeEuCaso.Interfaces;

namespace HojeEuCaso.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private ISessionUsuarioService _sessionUsuarioService;
        private ILoginService _loginService;
        private IUsuarioService _usuarioService;

        public LoginController(ILogger<LoginController> logger,
                               ISessionUsuarioService sessionUsuarioService,
                               ILoginService loginService,
                               IUsuarioService usuarioService)
        {
            _logger = logger;
            _sessionUsuarioService = sessionUsuarioService;
            _loginService = loginService;
            _usuarioService = usuarioService;
        }

        public IActionResult Login()
        {
            _loginService.RemoveSession(HttpContext);
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuario usuarioLogin)
        {
            if ((usuarioLogin.Email == "teste@teste.com.br" && usuarioLogin.Senha == "123"))
            {
                var usuarioLogando = new Usuario
                {
                    Nome = "Usuario 1",
                    Senha = "123",
                    Role = new Role()
                    {
                        RoleID = 1,
                        Nome = "Franqueado",
                        Ativo = true
                    }
                };

                var usuario = _sessionUsuarioService.Login(usuarioLogando);

                if (usuario != null)
                {
                    HttpContext.Session.SetString("Nome", usuario.Nome);
                    HttpContext.Session.SetString("Role", usuario.Role.Nome);
                    return RedirectToAction("Index", "Home");
                }

                //-----------------------------------

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Nome), // substituir por um nome de usuário real
                    //Adicionar outras claims aqui, se necessário
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true, 
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
                };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Tentativa inválida de login.");
                return View();
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            _loginService.RemoveSession(HttpContext);
            return RedirectToAction("Login", "Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult CreateUsuario([FromBody]Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ApiResponse 
                {
                    Success = false,
                    Message = "Erro de validação do modelo."
                });
            }

            _usuarioService.CreateNewUser(usuario);

            return Json(new ApiResponse
            {
                Success = true,
                Message = "Usuário cadastrado com sucesso."
            });
        }
    }
}
