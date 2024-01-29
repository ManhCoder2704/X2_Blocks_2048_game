using DG.Tweening;
using TMPro;

public static class NumberLerp
{
    private const float lerpDuration = 1f;

    public static void LerpNumber(this TMP_Text numberText, int targetNumber)
    {
        // Get the current number from the text component
        int startNumber = int.Parse(numberText.text);
        if (startNumber == targetNumber) return;
        // Use DOTween to lerp between the start and target numbers
        DOTween.To(() => startNumber, x => numberText.text = x.ToString(), targetNumber, lerpDuration)
            .SetEase(Ease.OutQuad); // You can change the ease type as needed
    }
}
