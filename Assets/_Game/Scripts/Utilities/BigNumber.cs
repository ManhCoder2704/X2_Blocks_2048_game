using System.Numerics;
using TMPro;

public static class BigNumber
{
    private const int thousand = 1000;
    private const int tenThousand = 10000;
    private const int milion = 1000000;
    private const int billion = 1000000000;

    public static void FormatLargeNumberPowerOfTwo(this TMP_Text displayText, int number, string prefix = "")
    {
        BigInteger bigInteger = BigInteger.One << number;
        displayText.FormatBack(bigInteger);
        displayText.text = prefix + displayText.text;
    }
    public static void FormatBack(this TMP_Text displayText, BigInteger number)
    {
        displayText.text = FormatBack(number);
    }

    public static string FormatBack(BigInteger number)
    {
        if (BigInteger.Compare(number, tenThousand) < 0)
        {
            return number.ToString();
        }
        else if (BigInteger.Compare(number, milion) < 0)
        {
            return (number / thousand).ToString() + " K";
        }
        else if (BigInteger.Compare(number, billion) < 0)
        {
            return (number / milion).ToString() + " M";
        }
        else
        {
            char[] suffixes = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 'u', 'v', 'w', 'x', 'y', 'z' };
            int suffixIndex = 0;
            number /= billion;
            while (number >= milion && suffixIndex < suffixes.Length - 1)
            {
                number /= milion;
                suffixIndex++;
            }

            return number.ToString("N0") + " " + suffixes[suffixIndex];
        }
    }

    public static BigInteger String2BigInterger(this string number)
    {
        BigInteger bigInteger = BigInteger.Parse(number);
        return bigInteger;
    }

    public static void String2Point(this TMP_Text displayText, string numberText)
    {
        displayText.FormatBack(numberText.String2BigInterger());
    }

    public static string String2Point(this string numberText)
    {
        return FormatBack(numberText.String2BigInterger());
    }
}
