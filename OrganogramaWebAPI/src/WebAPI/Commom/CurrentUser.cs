using Microsoft.AspNetCore.Http;
using Organograma.Infraestrutura.Comum;
using Organograma.Negocio.Base;
using Organograma.Negocio.Commom.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Organograma.WebAPI.Commom
{
    public class CurrentUser : ICurrentUserProvider
    {
        private string _userCpf;
        private string _userNome;
        private List<Guid> _userGuidsOrganizacao;
        private List<Guid> _userGuidsOrganizacaoPatriarca;

        private IGuidOrganizacaoProvider _service;

        public CurrentUser(IHttpContextAccessor httpContextAccessor, IGuidOrganizacaoProvider service)
        {
            _service = service;

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
            Guid guidOrganizacaoUsuario = _service.Search(organizacaoSigla);

            if (_userGuidsOrganizacao == null)
                _userGuidsOrganizacao = new List<Guid>();

            _userGuidsOrganizacao.Add(guidOrganizacaoUsuario);

            Guid userGuidOrganizacaoPatriarca = _service.SearchPatriarca(guidOrganizacaoUsuario);
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