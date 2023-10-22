using System.Collections.Generic;
using System.Linq;
using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;

namespace HojeEuCaso.Sessions
{
    public class SessionUsuarioService : ISessionUsuarioService
    {
        private List<Usuario> usuarios;
        private readonly IUsuarioSistemaService _usuarioSistemaService;

        public SessionUsuarioService(IUsuarioSistemaService usuarioSistemaService)
        {
            _usuarioSistemaService = usuarioSistemaService; 
        }

        public UsuarioSistema Login(UsuarioSistema usuario)
        {
            var usuarioSistema = _usuarioSistemaService.GetUsuarioSistemaLogin(usuario);
            return usuarioSistema;
        }
    }
}
