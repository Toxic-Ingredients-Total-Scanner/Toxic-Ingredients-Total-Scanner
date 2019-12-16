using System;
using System.Collections.Generic;
using System.Text;
using GoogleTranslateFreeApi;

namespace TITS_API.Services.Services
{
    public class TranslateService
    {
        private readonly GoogleTranslator _translator;

        public TranslateService(GoogleTranslator googleTranslator)
        {
            _translator = googleTranslator;
        }

        
        public TranslationResult Translate(string text, Language from, Language to)
        {
            return _translator.TranslateAsync(text, from, to).GetAwaiter().GetResult();
        }

        public TranslationResult[] Translate(string[] texts, Language from, Language to)
        {
            TranslationResult[] results = new TranslationResult[texts.Length];

            for(int i = 0; i < texts.Length; i++)
            {
                results[i] = _translator.TranslateAsync(texts[i], from, to).GetAwaiter().GetResult();
            }

            return results;
        }
    }
}
