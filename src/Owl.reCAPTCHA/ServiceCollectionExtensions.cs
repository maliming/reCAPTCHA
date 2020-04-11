using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Owl.reCAPTCHA.v2;
using Owl.reCAPTCHA.v3;

namespace Owl.reCAPTCHA
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddreCAPTCHAV3(this IServiceCollection services,
            Action<reCAPTCHAOptions> setupAction, Action<HttpClient> configureClient = null)
        {
            if (setupAction != null)
            {
                services.Configure(reCAPTCHAConsts.V3, setupAction);
            }

            if (configureClient == null)
            {
                services.AddHttpClient(reCAPTCHAConsts.V3);
            }
            else
            {
                services.AddHttpClient(reCAPTCHAConsts.V3, configureClient);
            }
               
            services.AddTransient<IreCAPTCHALanguageCodeProvider, CultureInforeCAPTCHALanguageCodeProvider>();
            services.AddTransient<IreCAPTCHASiteVerifyV3, reCAPTCHASiteVerifyV3>();

            return services;
        }

        public static IServiceCollection AddreCAPTCHAV2(this IServiceCollection services,
            Action<reCAPTCHAOptions> setupAction, Action<HttpClient> configureClient = null)
        {
            if (setupAction != null)
            {
                services.Configure(reCAPTCHAConsts.V2, setupAction);
            }

            if (configureClient == null)
            {
                services.AddHttpClient(reCAPTCHAConsts.V2);
            }
            else
            {
                services.AddHttpClient(reCAPTCHAConsts.V2, configureClient);
            }

            services.AddTransient<IreCAPTCHALanguageCodeProvider, CultureInforeCAPTCHALanguageCodeProvider>();
            services.AddTransient<IreCAPTCHASiteVerifyV2, reCAPTCHASiteVerifyV2>();

            return services;
        }
    }
}