namespace Organograma.Negocio.Modelos
{
    public class ContatoModeloNegocio
    {
        public int Id { get; set; }
        public string Telefone { get; set; }
        public string Nome { get; set; }
        public bool? Excluir { get; set; }

        public TipoContatoModeloNegocio TipoContato { get; set; }
    }
}
