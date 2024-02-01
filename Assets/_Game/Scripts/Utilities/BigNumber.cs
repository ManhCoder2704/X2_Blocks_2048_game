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
        if (BigInteger.Compare(bigInteger, thousand) < 0)
        {
            displayText.text = prefix + bigInteger.ToString();
        }
        else if (BigInteger.Compare(bigInteger, milion) < 0)
        {
            displayText.text = prefix + (bigInteger / thousand).ToString() + "K";
        }
        else if (BigInteger.Compare(bigInteger, billion) < 0)
        {
            displayText.text = prefix + (bigInteger / milion).ToString() + "M";
        }
        else
        {
            char[] suffixes = { 'B', 'T', 'Q', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 'u', 'v', 'w', 'x', 'y', 'z' };
            int suffixIndex = 0;

            while (bigInteger >= 1000L && suffixIndex < suffixes.Length - 1)
            {
                bigInteger /= 1000L;
                suffixIndex++;
            }

            displayText.text = prefix + bigInteger.ToString() + suffixes[suffixIndex];
        }
    }
    public static void FormatBack(this TMP_Text displayText, BigInteger number)
    {
        if (BigInteger.Compare(number, thousand) < 0)
        {
            displayText.text = number.ToString();
        }
        else if (BigInteger.Compare(number, milion) < 0)
        {
            displayText.text = (number / thousand).ToString() + "K";
        }
        else if (BigInteger.Compare(number, billion) < 0)
        {
            displayText.text = (number / milion).ToString() + "M";
        }
        else
        {
            char[] suffixes = { 'B', 'T', 'Q', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 'u', 'v', 'w', 'x', 'y', 'z' };
            int suffixIndex = 0;

            while (number >= 1000L && suffixIndex < suffixes.Length - 1)
            {
                number /= 1000L;
                suffixIndex++;
            }

            displayText.text = number.ToString() + suffixes[suffixIndex];
        }
    }

    public static string FormatBack(BigInteger number)
    {
        if (BigInteger.Compare(number, thousand) < 0)
        {
            return number.ToString();
        }
        else if (BigInteger.Compare(number, milion) < 0)
        {
            return (number / thousand).ToString() + "K";
        }
        else if (BigInteger.Compare(number, billion) < 0)
        {
            return (number / milion).ToString() + "M";
        }
        else
        {
            char[] suffixes = { 'B', 'T', 'Q', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 'u', 'v', 'w', 'x', 'y', 'z' };
            int suffixIndex = 0;

            while (number >= 1000L && suffixIndex < suffixes.Length - 1)
            {
                number /= 1000L;
                suffixIndex++;
            }

            return number.ToString() + suffixes[suffixIndex];
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
