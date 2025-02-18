using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurrencyConverter.Models
{
    public class CurrencyModel
    {
        public string SourceCurrency { get; set; }
        public string DestinationCurrency { get; set; }
        public decimal Amount { get; set; }
        public decimal ConvertedAmount { get; set; }
    }
}