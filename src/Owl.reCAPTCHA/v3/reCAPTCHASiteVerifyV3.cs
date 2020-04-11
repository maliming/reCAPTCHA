using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Owl.reCAPTCHA.v3
{
    public class reCAPTCHASiteVerifyV3 : IreCAPTCHASiteVerifyV3
    {
        private readonly HttpClient _client;
        private readonly reCAPTCHAOptions _options;

        public reCAPTCHASiteVerifyV3(IOptionsSnapshot<reCAPTCHAOptions> optionsAccessor, IHttpClientFactory clientFactory)
        {
            _options = optionsAccessor.Get(reCAPTCHAConsts.V3);
            _client = clientFactory.CreateClient(reCAPTCHAConsts.V3);
            _client.BaseAddress = new Uri(_options.VerifyBaseUrl);
        }

        public async Task<reCAPTCHASiteVerifyV3Response> Verify(reCAPTCHASiteVerifyRequest request)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("secret", _options.SiteSecret),
                new KeyValuePair<string, string>("response", request.Response),
                new KeyValuePair<string, string>("remoteip", request.RemoteIp)
            });

            var v3Response = await _client.PostAsync("recaptcha/api/siteverify", content);
            if (v3Response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<reCAPTCHASiteVerifyV3Response>(
                    await v3Response.Content.ReadAsStringAsync());
            }

            return new reCAPTCHASiteVerifyV3Response
            {
                Success = false,
                ErrorCodes = new[]
                {
                    "http-status-error-" + v3Response.StatusCode
                }
            };
        }
    }
}