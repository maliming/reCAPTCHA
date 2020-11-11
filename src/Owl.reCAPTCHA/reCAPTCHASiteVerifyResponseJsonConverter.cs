using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Owl.reCAPTCHA.v3;

namespace Owl.reCAPTCHA
{
    public class reCAPTCHASiteVerifyResponseJsonConverter : JsonConverter<reCAPTCHASiteVerifyResponse>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(reCAPTCHASiteVerifyV3Response) ||
                   typeToConvert == typeof(reCAPTCHASiteVerifyResponse);
        }

        public override reCAPTCHASiteVerifyResponse Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var rootElement = JsonDocument.ParseValue(ref reader).RootElement;

            var success = rootElement.EnumerateObject().FirstOrDefault(x => x.Name.Equals("success", StringComparison.InvariantCultureIgnoreCase));
            var challengeTimestamp = rootElement.EnumerateObject().FirstOrDefault(x => x.Name.Equals("challenge_ts", StringComparison.InvariantCultureIgnoreCase));
            var hostname = rootElement.EnumerateObject().FirstOrDefault(x => x.Name.Equals("hostname", StringComparison.InvariantCultureIgnoreCase));
            var errorCodes = rootElement.EnumerateObject().FirstOrDefault(x => x.Name.Equals("error-codes", StringComparison.InvariantCultureIgnoreCase));

            if (typeToConvert ==  typeof(reCAPTCHASiteVerifyResponse))
            {
                return new reCAPTCHASiteVerifyResponse
                {
                    Success = success.Value.ValueKind == JsonValueKind.True,
                    ChallengeTs = challengeTimestamp.Value.ValueKind == JsonValueKind.String ? challengeTimestamp.Value.GetDateTime() : DateTime.MinValue,
                    HostName = hostname.Value.ValueKind == JsonValueKind.String ? hostname.Value.GetString() : null,
                    ErrorCodes = errorCodes.Value.ValueKind == JsonValueKind.Array ? JsonSerializer.Deserialize<string[]>(errorCodes.Value.GetRawText()) : null
                };
            }

            if (typeToConvert ==  typeof(reCAPTCHASiteVerifyV3Response))
            {
                var score = rootElement.EnumerateObject().FirstOrDefault(x => x.Name.Equals("score", StringComparison.InvariantCultureIgnoreCase));
                var action = rootElement.EnumerateObject().FirstOrDefault(x => x.Name.Equals("action", StringComparison.InvariantCultureIgnoreCase));
                return new reCAPTCHASiteVerifyV3Response
                {
                    Success = success.Value.ValueKind == JsonValueKind.True,
                    ChallengeTs = challengeTimestamp.Value.ValueKind == JsonValueKind.String ? challengeTimestamp.Value.GetDateTime() : DateTime.MinValue,
                    HostName = hostname.Value.ValueKind == JsonValueKind.String ? hostname.Value.GetString() : null,
                    ErrorCodes = errorCodes.Value.ValueKind == JsonValueKind.Array ? JsonSerializer.Deserialize<string[]>(errorCodes.Value.GetRawText()) : null,
                    Score = score.Value.ValueKind == JsonValueKind.Number ? score.Value.GetSingle() : 0,
                    Action = action.Value.ValueKind == JsonValueKind.String ? action.Value.GetString() : null
                };
            }

            throw new JsonException($"Incorrect type: {typeToConvert.FullName}");
        }

        public override void Write(Utf8JsonWriter writer, reCAPTCHASiteVerifyResponse value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value);
        }
    }
}
