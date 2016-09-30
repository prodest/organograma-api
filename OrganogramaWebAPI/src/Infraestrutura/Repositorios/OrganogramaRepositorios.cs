﻿using Organograma.Dominio.Base;
using Organograma.Dominio.Modelos;
using Organograma.Infraestrutura.Mapeamento;

namespace Organograma.Infraestrutura.Repositorios
{
    public class OrganogramaRepositorios : IOrganogramaRepositorios
    {
        public OrganogramaRepositorios()
        {
            UnitOfWork = new EFUnitOfWork(new OrganogramaContext());

            Municipios = UnitOfWork.MakeGenericRepository<Municipio>();

        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public IRepositorioGenerico<Municipio> Municipios { get; private set; }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}