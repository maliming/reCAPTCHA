using System.Threading.Tasks;

namespace Owl.reCAPTCHA.v2
{
    public interface IreCAPTCHASiteVerifyV2
    {
        Task<reCAPTCHASiteVerifyResponse> Verify(reCAPTCHASiteVerifyRequest request);
    }
}
