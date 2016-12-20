namespace Organograma.Apresentacao.Modelos
{
    public class ContatoModelo
    {
        public string Telefone { get; set; }
        public int IdTipoContato{ get; set; }
        //public string Nome { get; set; }
    }

    public class ContatoModeloGet
    {
        public string Telefone { get; set; }
        public TipoContatoModeloGet TipoContato { get; set; }
    }

    public class TipoContatoModeloGet
    {
        public string Descricao { get; set; }
    }
}
