using System.Collections.Generic;
using System.Linq;
using HojeEuCaso.Models;

namespace HojeEuCaso.Sessions
{
    public class SessionUsuarioService : ISessionUsuarioService
    {
        private List<Usuario> usuarios;

        public SessionUsuarioService()
        {
            usuarios = new List<Usuario>()
            {
                new Usuario()
                {
                    Nome = "Usuario 1",
                    Senha = "123",
                    Role = new Role()
                    {
                        RoleID = 1,
                        Nome = "Piscineiro",
                        Ativo = true
                    }
                },
                new Usuario()
                {
                    Nome = "Usuario 2",
                    Senha = "123",
                    Role = new Role()
                    {
                        RoleID = 1,
                        Nome = "Piscineiro",
                        Ativo = true
                    }
                }
            };

        }

        public Usuario Login(Usuario usuario)
        {
            return usuarios.SingleOrDefault(x => x.Nome == usuario.Nome && x.Senha == usuario.Senha);
        }
    }
}
