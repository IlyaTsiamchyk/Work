namespace LangDetector.Models
{
    public abstract class Language
    {
        protected string Alphabet;

        public virtual string GetAlphabet()
        {
            return Alphabet;
        }

        public abstract string GetName();
    }

    public class EnglishLanguage : Language
    {
        public EnglishLanguage()
        {
            Alphabet = "ABCDEFGHIJKLMNOPQRSTUWXYZabcdefghijklmnopqrstuvwxyz";
        }

        public override string GetName()
        {
            return "English";
        }
    }

    public class RussianLanguage : Language
    {
        public RussianLanguage()
        {
            Alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        }

        public override string GetName()
        {
            return "Russian";
        }
    }

    public class PortugalLanguage : Language
    {
        public PortugalLanguage()
        {
            Alphabet = "asdfghjklçºqwertyuiopzxcvbnmASDFGHJKLÇQWERTYUIOPZXCVBNMÉéÝÚÍýúíÓóÁáÕÃÑõãñ";
        }

        public override string GetName()
        {
            return "Portugal";
        }
    }

    public class BulgarianLanguage : Language
    {
        public BulgarianLanguage()
        {
            Alphabet = "ьяаожгтнвмчюйъэфхпрлбуеишщксдзцЬЯАОЖГТНВМЧЮЙЪЭФХПРЛБУЕИШЩКСДЗЦ";
        }

        public override string GetName()
        {
            return "Bulgarian";
        }
    }

    public class SpanishLanguage : Language
    {
        public SpanishLanguage()
        {
            Alphabet = "asdfghjklzxcvbnmqwertyuiopçºªÇASDFGHJKLÑZXCVBNMQWERTYUIOPàèùìòÈÙÌÒÀñ";
        }

        public override string GetName()
        {
            return "Spanish";
        }
    }
}