using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSimulator.RandomUser
{
    public interface IRandomUserClient
    {
        Task<string> GetRandomFullNameAsync(CancellationToken ct = default);
    }
}
