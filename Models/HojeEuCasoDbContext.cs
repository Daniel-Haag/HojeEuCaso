using Microsoft.EntityFrameworkCore;

namespace HojeEuCaso.Models
{
    public class HojeEuCasoDbContext : DbContext
    {
        public HojeEuCasoDbContext(DbContextOptions<HojeEuCasoDbContext> options) : base(options)
        {
        }

        public DbSet<Agenda> Agendas { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<TipoCategoria> TiposCategorias { get; set; }
        public DbSet <Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<FornecedorIndicado> FornecedoresIndicados { get; set; }
        public DbSet<Pacote> Pacotes { get; set; }
        public DbSet<Plano> Planos { get; set; }
        public DbSet<UsuarioSistema> UsuariosSistema { get; set; }
        public DbSet<ItensDePacotes> ItensDePacotes { get; set; }
        public DbSet<ItensDePacotesDeUsuarios> ItensDePacotesDeUsuarios { get; set; }
        public DbSet<CategoriaDosPlanos> CategoriasDosPlanos { get; set; }
        public DbSet<PacotesDeUsuarios> PacotesDeUsuarios { get; set; }
        public DbSet<ClausulaContrato> ClausulasContratos { get; set; }
        public DbSet<FotosServicos> FotosServicos { get; set; }
        public DbSet<PlanoFornecedor> PlanosFornecedores { get; set; }
        public DbSet<Orcamento> Orcamentos { get; set; }
    }
}
