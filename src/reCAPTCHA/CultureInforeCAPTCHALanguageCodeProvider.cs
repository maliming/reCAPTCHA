using System.Globalization;

namespace reCAPTCHA
{
    public class CultureInforeCAPTCHALanguageCodeProvider : IreCAPTCHALanguageCodeProvider
    {
        public string GetLanguageCode()
        {
            return CultureInfo.CurrentUICulture.ToString();
        }
    }
}