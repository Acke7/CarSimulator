using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CarSimulator.RandomUser
{
    public sealed class RandomUserClient : IRandomUserClient
    {
        private static readonly HttpClient http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(8)
        };

        private const string Url = "https://randomuser.me/api/?inc=name&noinfo=1";

        public RandomUserClient()
        {
        }

        public async Task<string> GetRandomFullNameAsync(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                using (var stream = await http.GetStreamAsync(Url, ct))
                {
                    using (var doc = await JsonDocument.ParseAsync(stream, cancellationToken: ct))
                    {
                        JsonElement name = doc.RootElement
                                              .GetProperty("results")[0]
                                              .GetProperty("name");

                        string first = name.GetProperty("first").GetString() ?? "Unknown";
                        string last = name.GetProperty("last").GetString() ?? "Driver";

                        return Capitalize(first) + " " + Capitalize(last);
                    }
                }
            }
            catch
            {
                // Keep the app running even if the API call fails
                return "Unknown Driver";
            }
        }

        private static string Capitalize(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return s;
            }

            char firstChar = char.ToUpperInvariant(s[0]);
            if (s.Length == 1)
            {
                return new string(firstChar, 1);
            }

            return firstChar + s.Substring(1);
        }
    }
}
