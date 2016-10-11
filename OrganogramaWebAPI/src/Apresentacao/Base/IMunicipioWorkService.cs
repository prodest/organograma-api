﻿using Organograma.Apresentacao.Modelos;
using System.Collections.Generic;

namespace Apresentacao.Base
{
    public interface IMunicipioWorkService
    {
        List<MunicipioModeloGet> Listar();
        MunicipioModeloGet Pesquisar(int id);
        MunicipioModeloGet Inserir(MunicipioModeloPost municipioPost);
        void Alterar(int id, MunicipioModeloPut municipioPut);
        void Excluir(int id);
    }

}