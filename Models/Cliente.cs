namespace HojeEuCaso.Models
{
    public class Cliente
    {
        public int ClienteID { get; set; }
        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; }
        public int PiscineiroID { get; set; }
        public Piscineiro Piscineiro { get; set; }
        public string Endereco { get; set; }
        public int Telefone { get; set; }
    }
}
