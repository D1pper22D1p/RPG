
namespace RPG.Core;
internal static class FirstUpSymbolToUpper
{
    internal static string Apply(string applyString)
    {
        char[] chars = applyString.ToCharArray();
        if (chars.Length > 0)
        {
            chars[0] = char.ToUpper(chars[0]);
            for(int i = 1; i < chars.Length; i++)
            {
                chars[i] = chars[i - 1] == ' ' ? char.ToUpper(chars[i]) : char.ToLower(chars[i]);
            }
        }
        return new string(chars);
    }
}