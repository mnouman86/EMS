using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Interfaces.Services
{
    public interface IExchangeRateApiService
    {
        Task<Dictionary<string, decimal>> GetCurrencyRates(string baseCurrency);
    }
}
