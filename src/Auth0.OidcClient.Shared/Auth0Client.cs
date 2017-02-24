﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using IdentityModel.OidcClient;

namespace Auth0.OidcClient
{
    public class Auth0Client
    {
        private readonly string _domain;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public Auth0Client(string domain, string clientId)
            : this(domain, clientId, null)
        {
        }

        public Auth0Client(string domain, string clientId, string clientSecret)
        {
            _domain = domain;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public Task<LoginResult> LoginAsync(object extraParameters = null)
        {
            var authority = $"https://{_domain}";

            var options = new OidcClientOptions
            {
                Authority = authority,
                ClientId = _clientId,
                ClientSecret = _clientSecret,
                Scope = "openid profile",
                RedirectUri = $"https://{_domain}/mobile",
                Browser = new PlatformWebView(),
                Flow = OidcClientOptions.AuthenticationFlow.AuthorizationCode,
                ResponseMode = OidcClientOptions.AuthorizeResponseMode.Redirect,
                Policy =
                {
                    RequireAuthorizationCodeHash = false,
                    RequireAccessTokenHash = false
                }
            };

            var oidcClient = new IdentityModel.OidcClient.OidcClient(options);

            return oidcClient.LoginAsync(extraParameters: extraParameters);
        }
    }
}