using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Owl.reCAPTCHA;
using Owl.reCAPTCHA.v2;

namespace reCAPTCHA.Demo.Pages;

public class V2_InvisibleModel : PageModel
{
    private readonly IreCAPTCHASiteVerifyV2 _siteVerify;

    public string Result { get; set; }

    public V2_InvisibleModel (IreCAPTCHASiteVerifyV2 siteVerify)
    {
        _siteVerify = siteVerify;
    }

    public async Task OnPostAsync(string token)
    {
        var response = await _siteVerify.Verify(new reCAPTCHASiteVerifyRequest
        {
            Response = token,
            RemoteIp = HttpContext.Connection.RemoteIpAddress?.ToString()
        });

        Result = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            WriteIndented = true
        });
    }
}