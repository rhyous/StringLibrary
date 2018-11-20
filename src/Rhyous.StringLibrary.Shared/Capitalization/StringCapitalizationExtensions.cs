namespace Rhyous.StringLibrary
{
    public static class StringCapitalizationExtensions
    {
        public static string CapitalizeFirstLetter(this string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return word;
            char[] charArray = word.ToCharArray();
            charArray[0] = char.ToUpper(charArray[0]);
            return new string(charArray);
        }
    }
}