namespace Organograma.Apresentacao.Modelos
{
    public class ContatoModelo
    {
        public string Telefone { get; set; }
        public int IdTipoContato{ get; set; }
        public string Nome { get; set; }
    }

    public class ContatoModeloPut : ContatoModelo
    {
        public bool? Excluir { get; set; }
    }
}
