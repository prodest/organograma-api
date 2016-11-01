namespace Organograma.Apresentacao.Modelos
{
    public class SiteModelo

    {
        public string Url { get; set; }
    }

    public class SiteModeloPatch : SiteModelo
    {
        public int Id { get; set; }
    }
}
