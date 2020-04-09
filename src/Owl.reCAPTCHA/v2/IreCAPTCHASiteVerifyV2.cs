using System.Threading.Tasks;

namespace reCAPTCHA.v2
{
    public interface IreCAPTCHASiteVerifyV2
    {
        Task<reCAPTCHASiteVerifyResponse> Verify(reCAPTCHASiteVerifyRequest request);
    }
}
