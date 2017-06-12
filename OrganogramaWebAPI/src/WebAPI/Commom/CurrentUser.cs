using Apresentacao.Base;
using Microsoft.AspNetCore.Http;
using Organograma.Apresentacao.Modelos;
using Organograma.Infraestrutura.Comum;
using Organograma.Negocio.Base;
using Organograma.Negocio.Commom.Base;
using Organograma.WebAPI.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Organograma.WebAPI.Commom
{
    public class CurrentUser : ICurrentUserProvider
    {
        private string _userCpf;
        private string _userNome;
        private List<Guid> _userGuidsOrganizacao;
        private List<Guid> _userGuidsOrganizacaoPatriarca;

        private IOrganizacaoWorkService _serviceOrganizacao;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor != null && httpContextAccessor.HttpContext != null)
                FillUser(httpContextAccessor.HttpContext.User);
        }

        public string UserCpf
        {
            get
            {
                return _userCpf;
            }
        }

        public string UserNome
        {
            get
            {
                return _userNome;
            }
        }

        public List<Guid> UserGuidsOrganizacao
        {
            get
            {
                return _userGuidsOrganizacao;
            }
        }

        public List<Guid> UserGuidsOrganizacaoPatriarca
        {
            get
            {
                return _userGuidsOrganizacaoPatriarca;
            }
        }

        private void FillUser(ClaimsPrincipal user)
        {
            if (user != null)
            {
                Claim claimCpf = user.FindFirst("cpf");
                Claim claimNome = user.FindFirst("nome");
                if (claimCpf != null && claimNome != null)
                {
                    _userCpf = claimCpf.Value;
                    _userNome = claimNome.Value;

                    List<Claim> claimsOrganizacao = user.FindAll("orgao").ToList();

                    if (claimsOrganizacao != null && claimsOrganizacao.Count > 0)
                    {
                        foreach (Claim c in claimsOrganizacao)
                        {
                            FillOrgaoEPatriarca(c.Value);
                        }
                    }
                }
            }
        }

        private void FillOrgaoEPatriarca(string organizacaoSigla)
        {
            OrganizacaoModeloGet organizacaoUsuario = _serviceOrganizacao.PesquisarPorSigla(organizacaoSigla);

            if (_userGuidsOrganizacao == null)
                _userGuidsOrganizacao = new List<Guid>();

            _userGuidsOrganizacao.Add(new Guid(organizacaoUsuario.Guid));

            Guid userGuidOrganizacaoPatriarca = new Guid(_serviceOrganizacao.PesquisarPatriarca(organizacaoUsuario.Guid).Guid);
            if (userGuidOrganizacaoPatriarca == null)
                throw new OrganogramaException("Não foi possível obter a organização patriarca do usuário.");

            if (_userGuidsOrganizacaoPatriarca == null)
                _userGuidsOrganizacaoPatriarca = new List<Guid>();

            bool existsUserGuidOrganizacaoPatriarca = false;
            if (_userGuidsOrganizacaoPatriarca.Count > 0)
            {
                var g = _userGuidsOrganizacaoPatriarca.Where(x => x.Equals(userGuidOrganizacaoPatriarca))
                                                      .SingleOrDefault();

                if (g != null)
                    existsUserGuidOrganizacaoPatriarca = true;
            }


            if (!existsUserGuidOrganizacaoPatriarca)
                _userGuidsOrganizacaoPatriarca.Add(userGuidOrganizacaoPatriarca);
        }
    }
}
