using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Owl.reCAPTCHA;
using Owl.reCAPTCHA.v3;

namespace reCAPTCHA.Demo.Pages
{
    public class V3Model : PageModel
    {
        private readonly IreCAPTCHASiteVerifyV3 _siteVerify;

        public string Result { get; set; }

        public V3Model(IreCAPTCHASiteVerifyV3 siteVerify)
        {
            _siteVerify = siteVerify;
        }

        public async Task OnPostAsync(string token)
        {
            var response = await _siteVerify.Verify(new reCAPTCHASiteVerifyRequest
            {
                Response = token,
                RemoteIp = HttpContext.Connection.RemoteIpAddress.ToString()
            });

            Result = JsonConvert.SerializeObject(response, Formatting.Indented);
        }
    }
}