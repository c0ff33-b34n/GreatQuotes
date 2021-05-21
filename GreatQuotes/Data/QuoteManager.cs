using GreatQuotes.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GreatQuotes.Data
{
    public class QuoteManager
    {
        public static QuoteManager Instance { get; private set; }

        private IQuoteLoader loader;

        public IList<GreatQuoteViewModel> Quotes { get; set; }

        public QuoteManager(IQuoteLoader loader)
        {
            if (Instance != null)
            {
                throw new Exception("Can only create a single QuoteManager.");
            }
            Instance = this;
            this.loader = loader;
            Quotes = new ObservableCollection<GreatQuoteViewModel>(loader.Load());
        }
        

        public void Save()
        {
            loader.Save(Quotes);
        }

        public void SayQuote(GreatQuoteViewModel quote)
        {
            if (quote == null)
                throw new ArgumentNullException("No quote set");

            ITextToSpeech tts = ServiceLocator.Instance.Resolve<ITextToSpeech>();

            if (tts != null)
            {
                var text = quote.QuoteText;

                if (!string.IsNullOrWhiteSpace(quote.Author))
                    text += $" by {quote.Author}";

                tts.Speak(text);
            }
        }
    }
}
