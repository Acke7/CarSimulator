using System;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CarSimulator.RandomUser
{
    public sealed class RandomUserClient : IRandomUserClient
    {
        private static readonly HttpClient http = new()
        {
            Timeout = TimeSpan.FromSeconds(8)
        };

        private const string Url = "https://randomuser.me/api/?inc=name&noinfo=1";

     
        public RandomUserClient()
        {
        }

        public async Task<string> GetRandomFullNameAsync(CancellationToken ct = default)
        {
            try
            {
                using var stream = await http.GetStreamAsync(Url, ct);
                using var doc = await JsonDocument.ParseAsync(stream, cancellationToken: ct);

                var name = doc.RootElement
                              .GetProperty("results")[0]
                              .GetProperty("name");

                var first = name.GetProperty("first").GetString() ?? "Unknown";
                var last = name.GetProperty("last").GetString() ?? "Driver";

                return $"{Cap(first)} {Cap(last)}";
            }
            catch
            {
                // Keep the app running even if the API call fails
                return "Unknown Driver";
            }

            static string Cap(string s) =>
                string.IsNullOrWhiteSpace(s) ? s : char.ToUpperInvariant(s[0]) + s[1..];
        }
    }
}
