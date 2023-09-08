namespace HojeEuCaso.Models
{
    public class OrdemServicoMaterial
    {
        public int OrdemServicoMaterialID { get; set; }
        public int OrdemServicoID { get; set; }
        public OrdemServico OrdemServico { get; set; }
        public int MyProperty { get; set; }
    }
}
