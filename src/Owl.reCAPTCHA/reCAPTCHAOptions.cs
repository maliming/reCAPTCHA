namespace Owl.reCAPTCHA
{
    public class reCAPTCHAOptions
    {
        public string VerifyBaseUrl { get; set; } = "https://www.google.com/";

        public string SiteKey { get; set; }

        public string SiteSecret { get; set; }
    }
}