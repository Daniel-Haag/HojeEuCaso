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
        private readonly IFornecedorService _fornecedorService;

        public LoginController(ILogger<LoginController> logger,
                               ISessionUsuarioService sessionUsuarioService,
                               ILoginService loginService,
                               IFornecedorService fornecedorService)
        {
            _logger = logger;
            _sessionUsuarioService = sessionUsuarioService;
            _loginService = loginService;
            _fornecedorService = fornecedorService;
        }

        public IActionResult Login()
        {
            _loginService.RemoveSession(HttpContext);
            return View();
        }

        [HttpPost]
        public IActionResult Login(UsuarioSistema usuarioLogin)
        {
            var usuario = _sessionUsuarioService.Login(usuarioLogin);

            if (usuario != null)
            {
                _loginService.AddSession(HttpContext, usuario);
                return RedirectToAction("Index", "Home");
            }
            else if (usuario == null)
            {
                var fornecedor = _fornecedorService.GetFornecedorLogin(usuarioLogin);

                if (fornecedor != null)
                {
                    _loginService.AddSession(HttpContext, fornecedor);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tentativa inválida de login.");
                    return View();
                }
            }

            return View();
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
        public IActionResult CreateUsuario([FromBody] Usuario usuario)
        {
            //if (!ModelState.IsValid)
            //{
            //    return Json(new ApiResponse
            //    {
            //        Success = false,
            //        Message = "Erro de validação do modelo."
            //    });
            //}

            //_usuarioService.CreateNewUser(usuario);

            //return Json(new ApiResponse
            //{
            //    Success = true,
            //    Message = "Usuário cadastrado com sucesso."
            //});
            return null;
        }
    }
}
