namespace HojeEuCaso.Models
{
    public class Franqueado
    {
        public int FranqueadoID { get; set; }
        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; }
        public int FranquiaID { get; set; }
        public Franquia Franquia { get; set; }
    }
}
