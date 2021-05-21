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
        private ITextToSpeech tts;

        public IList<GreatQuoteViewModel> Quotes { get; set; }

        public QuoteManager(IQuoteLoader loader, ITextToSpeech tts)
        {
            if (Instance != null)
            {
                throw new Exception("Can only create a single QuoteManager.");
            }
            Instance = this;
            this.loader = loader;
            this.tts = tts;
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
