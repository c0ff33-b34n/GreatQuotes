using System;

namespace GreatQuotes.Data
{
    public class QuoteLoaderFactory
    {
        public static Func<IQuoteLoader> Create { get; set; }
    }
}
