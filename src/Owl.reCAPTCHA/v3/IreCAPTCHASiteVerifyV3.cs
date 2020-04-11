using System.Threading.Tasks;

namespace Owl.reCAPTCHA.v3
{
    public interface IreCAPTCHASiteVerifyV3
    {
        Task<reCAPTCHASiteVerifyV3Response> Verify(reCAPTCHASiteVerifyRequest request);
    }
}
