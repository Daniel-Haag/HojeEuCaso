using HojeEuCaso.Models;

namespace HojeEuCaso.Sessions
{
    public interface ISessionUsuarioService
    {
        public UsuarioSistema Login(UsuarioSistema usuario);
    }
}
