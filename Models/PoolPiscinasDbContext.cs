using Microsoft.EntityFrameworkCore;

namespace HojeEuCaso.Models
{
    public class HojeEuCasoDbContext : DbContext
    {
        public HojeEuCasoDbContext(DbContextOptions<HojeEuCasoDbContext> options) : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Agenda> Agendas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Documentos> Documentos { get; set; }
        public DbSet<Franqueado> Franqueados { get; set; }
        public DbSet<Franquia> Franquias { get; set; }
        public DbSet<Material> Material { get; set; }
        public DbSet<OrdemServico> OrdemServicos { get; set; }
        public DbSet<OrdemServicoMaterial> OrdemServicoMateriais { get; set; }
        public DbSet<Piscineiro> Piscineiros { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet <Usuario> Usuarios { get; set; }
    }
}
