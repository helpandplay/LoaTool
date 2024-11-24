namespace LoaTool.Util;
public class TextValidationUtil
{
    public static bool IsOnlyNumber(string text)
    {
        return text.All(char.IsDigit);
    }
}
