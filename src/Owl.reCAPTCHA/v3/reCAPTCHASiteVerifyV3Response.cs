namespace Owl.reCAPTCHA.v3
{
    public class reCAPTCHASiteVerifyV3Response : reCAPTCHASiteVerifyResponse
    {
        // the score for this request (0.0 - 1.0)
        public float Score { get; set; }

        // the action name for this request (important to verify)
        public string Action { get; set; }
    }
}