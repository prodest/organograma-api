using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class Atividades
    {
        public decimal Idatividade { get; set; }
        public decimal? Pai { get; set; }
        public string Secao { get; set; }
        public string Divisao { get; set; }
        public string Grupo { get; set; }
        public string Classe { get; set; }
        public string Subclasse { get; set; }
        public string Denominacao { get; set; }
        public DateTime Iniciovigencia { get; set; }
        public DateTime? Fimvigencia { get; set; }
        public string Obsfimvigencia { get; set; }
    }
}
