using System;
using Newtonsoft.Json;

namespace Owl.reCAPTCHA
{
    public class reCAPTCHASiteVerifyResponse
    {
        // whether this request was a valid reCAPTCHA token for your site
        public bool Success { get; set; }

        // timestamp of the challenge load (ISO format yyyy-MM-dd'T'HH:mm:ssZZ)
        [JsonProperty("challenge_ts")]
        public DateTime ChallengeTs { get; set; }

        // the hostname of the site where the reCAPTCHA was solved
        public string HostName { get; set; }

        [JsonProperty("error-codes")] 
        public string[] ErrorCodes { get; set; }
    }
}