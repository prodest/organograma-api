using IdentityModel.Client;
using Microsoft.Extensions.Options;
using Organograma.WebAPI.Base;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Organograma.WebAPI.Config
{
    public class AcessoCidadaoClientAccessToken : IClientAccessToken
    {
        private readonly string _clientId;
        private readonly string _secret;
        private readonly IOptions<AutenticacaoIdentityServer> _autenticacaoIdentityServerConfig;
        private string _accessToken;
        private DateTime? _expireHalfTime;
        private DateTime? _expireTime;
        private object _thisLock = new object();
        //Instantiate a object of the Semaphore with a value of 1. This means that only 1 thread can be granted access at a time.
        private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        public AcessoCidadaoClientAccessToken(IOptions<AutenticacaoIdentityServer> autenticacaoIdentityServerConfig)
        {
            _clientId = Environment.GetEnvironmentVariable("OrganogramaApiClientId");
            _secret = Environment.GetEnvironmentVariable("OrganogramaApiSecret");
            _autenticacaoIdentityServerConfig = autenticacaoIdentityServerConfig;
        }

        public string AccessToken
        {
            get
            {
                return GetAccessTokenAsync().Result;
            }
        }

        private async Task<string> GetAccessTokenAsync()
        {
            DateTime now = DateTime.Now;

            if (!AccessTokenIsValid(now))
            {
                //Caso o access token ainda não possua valor ou já tenha atingido do tempo limite de expiração ele será obitido de forma síncrona.
                await SetAccessTokenAsync(now);
            }
            else if (AccessTokenIsInSecondHalfTime(now))
            {
                //Caso o access token já tenha passado da metade do tempo de expiração, mas ainda não tenha chegado no limite do tempo de expiração
                //o access token será renovado de forma assincrona de forma que o usuário não fique esperando esta operação.
                SetAccessTokenAsync(now);
            }

            return _accessToken;
        }

        private bool AccessTokenIsValid(DateTime now)
        {
            bool valid = false;

            if (!AccessTokenIsNullOrWhiteSpace() && !AccessTokenExpired(now))
                valid = true;

            return valid;
        }

        private bool AccessTokenIsInSecondHalfTime(DateTime now)
        {
            bool isInSecondHalf = false;

            if (_expireHalfTime.HasValue && _expireHalfTime.Value < now && !AccessTokenExpired(now))
                isInSecondHalf = true;

            return isInSecondHalf;
        }

        private bool AccessTokenExpired(DateTime now)
        {
            bool expired = true;

            if (_expireTime.HasValue && _expireTime.Value > now)
                expired = false;

            return expired;
        }

        private bool AccessTokenIsNullOrWhiteSpace()
        {
            bool isNullOrWhiteSpace = false;

            if (string.IsNullOrWhiteSpace(_accessToken))
                isNullOrWhiteSpace = true;

            return isNullOrWhiteSpace;
        }

        private async Task SetAccessTokenAsync(DateTime now)
        {
            //Asynchronously wait to enter the Semaphore. If no-one has been granted access to the Semaphore, code execution will proceed, otherwise this thread waits here until the semaphore is released 
            await _semaphoreSlim.WaitAsync();
            try
            {
                if (!AccessTokenIsValid(now) || AccessTokenIsInSecondHalfTime(now))
                {
                    TokenResponse tokenResponse = await GetOrganogramaAccessTokenAsync();

                    if (tokenResponse.IsError)
                        throw new Exception(tokenResponse.Error, tokenResponse.Exception);

                    //Tempo de expiração subtraído de 5 minutos para tolerância de relógios dessincronizados
                    _expireTime = DateTime.Now.AddSeconds((tokenResponse.ExpiresIn - (5 * 60)));
                    //Metade do tempo de expiração
                    _expireHalfTime = DateTime.Now.AddSeconds((tokenResponse.ExpiresIn / 2));
                    //AccessToken
                    _accessToken = tokenResponse.AccessToken;
                }
            }
            finally
            {
                //When the task is ready, release the semaphore. It is vital to ALWAYS release the semaphore when we are ready, or else we will end up with a Semaphore that is forever locked.
                //This is why it is important to do the Release within a try...finally clause; program execution may crash or take a different path, this way you are guaranteed execution
                _semaphoreSlim.Release();
            }
        }

        private async Task<TokenResponse> GetOrganogramaAccessTokenAsync()
        {
            AutenticacaoIdentityServer autenticacaoIdentityServer = _autenticacaoIdentityServerConfig.Value;

            TokenClient tokenClient = new TokenClient(autenticacaoIdentityServer.Authority + "/connect/token", _clientId, _secret);

            return await tokenClient.RequestClientCredentialsAsync("siarhes_admin");
        }
    }
}
