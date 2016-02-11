using System.Text.RegularExpressions;

namespace LangDetector.Models
{
    public class LangDetector
    {
        public ComparableGridModel Detect(string word, Language language)
        {
            string lettersPattern = @"\b[" + language.GetAlphabet() + "]";
            int matches = 0;
            int lettersLength = word.Length;


            foreach (char c in word)
            {
                if (Regex.IsMatch(c.ToString(), lettersPattern))
                {
                    matches++;
                }
                else if (Regex.IsMatch(c.ToString(), @"\B|\d"))
                {
                    lettersLength--;
                    if (lettersLength == 0)
                    {
                        return new ComparableGridModel(language.GetName(), 0);
                    }
                }
            }

            return new ComparableGridModel(language.GetName(), matches * 100 / lettersLength);
        }
    }
    public class ComparableGridModel
    {
        public string Language { get; set; }
        public int ChanceOfLanguage { get; set; }


        public ComparableGridModel(string language, int chanceOfLanguage)
        {
            Language = language;
            ChanceOfLanguage = chanceOfLanguage;
        } 
    }
}