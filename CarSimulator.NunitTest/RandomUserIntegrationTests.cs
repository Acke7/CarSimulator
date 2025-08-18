using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarSimulator.NunitTest
{
    [TestFixture]
    public class RandomUserIntegrationTests
    {
        [Test]
        public async Task RandomUser_Returns_First_And_Last_Name()
        {
            using var http = new HttpClient { Timeout = System.TimeSpan.FromSeconds(8) };
            var url = "https://randomuser.me/api/?inc=name&noinfo=1";

            using var stream = await http.GetStreamAsync(url);
            using var doc = await JsonDocument.ParseAsync(stream);

            var name = doc.RootElement.GetProperty("results")[0].GetProperty("name");
            var first = name.GetProperty("first").GetString();
            var last = name.GetProperty("last").GetString();

            Assert.That(first, Is.Not.Null.And.Not.Empty);
            Assert.That(last, Is.Not.Null.And.Not.Empty);
        }
    }
}