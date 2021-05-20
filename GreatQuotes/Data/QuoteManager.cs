using GreatQuotes.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GreatQuotes.Data
{
    public class QuoteManager
    {
        static readonly Lazy<QuoteManager> instance = new Lazy<QuoteManager>(() => new QuoteManager());

        private IQuoteLoader loader;

        public IList<GreatQuoteViewModel> Quotes { get; set; }

        public static QuoteManager Instance { get => instance.Value; }

        private QuoteManager()
        {
            loader = QuoteLoaderFactory.Create();
            Quotes = new ObservableCollection<GreatQuoteViewModel>(loader.Load());
        }

        public void Save()
        {
            loader.Save(Quotes);
        }
    }
}
