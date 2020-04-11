using System.Globalization;

namespace Owl.reCAPTCHA
{
    public class CultureInforeCAPTCHALanguageCodeProvider : IreCAPTCHALanguageCodeProvider
    {
        public string GetLanguageCode()
        {
            return CultureInfo.CurrentUICulture.ToString();
        }
    }
}