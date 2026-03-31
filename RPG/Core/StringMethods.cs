
namespace RPG.Core.StringMethods
{
    internal class FirstUpSymbolToUpper
    {
        internal static string Apply(string String)
        {
            char[] chars = String.ToCharArray();
            if (chars.Length > 0)
            {
                chars[0] = char.ToUpper(chars[0]);
                for(int i = 1; i < chars.Length; i++)
                {
                    if (chars[i - 1] == ' ')
                    {
                        chars[i] = char.ToUpper(chars[i]);
                    }else
                        chars[i] = char.ToLower(chars[i]);
                }
            }
            return new string(chars);
        }
    }
}