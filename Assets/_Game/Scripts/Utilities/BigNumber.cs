using System.Numerics;
using TMPro;

public static class BigNumber
{
    private const int thousand = 1000;
    private const int tenThousand = 10000;
    private const int milion = 1000000;
    private const int billion = 1000000000;

    public static void FormatLargeNumberPowerOfTwo(this TMP_Text displayText, int number)
    {
        BigInteger bigInteger = BigInteger.Pow(2, number);
        if (bigInteger < tenThousand)
        {
            displayText.text = bigInteger.ToString();
        }
        else if (bigInteger < milion)
        {
            displayText.text = (bigInteger / thousand).ToString() + "K";
        }
        else if (bigInteger < billion)
        {
            displayText.text = (bigInteger / thousand).ToString() + "M";
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

            displayText.text = bigInteger.ToString() + suffixes[suffixIndex];
        }
    }
    public static void FormatBack(this TMP_Text displayText, BigInteger number)
    {
        if (number < tenThousand)
        {
            displayText.text = number.ToString();
        }
        else if (number < milion)
        {
            displayText.text = (number / thousand).ToString() + "K";
        }
        else if (number < billion)
        {
            displayText.text = (number / thousand).ToString() + "M";
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
}
