namespace Organograma.Apresentacao.Modelos
{
    public class MunicipioModeloPost
    {
        public int CodigoIbge { get; set; }
        public string Nome { get; set; }
        public string Uf { get; set; }
    }

    public class MunicipioModeloPut : MunicipioModeloPost
    {
        public string Guid { get; set; }
    }
    
    public class MunicipioModeloGet : MunicipioModeloPost
    {
        public string Guid { get; set; }
    }


}
