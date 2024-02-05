using UnityEngine;
using UnityEngine.UI;

public class IconController : MonoBehaviour
{
    [SerializeField] private Image _thisImage;
    [SerializeField] private Sprite _onSprite;
    [SerializeField] private Sprite _offSprite;


    public void Swap(bool status)
    {
        if (status) _thisImage.sprite = _onSprite;
        else _thisImage.sprite = _offSprite;
    }
}
