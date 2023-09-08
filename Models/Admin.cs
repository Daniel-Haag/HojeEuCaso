namespace HojeEuCaso.Models
{
    public class Admin
    {
        public int AdminID { get; set; }
        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; }
    }
}
