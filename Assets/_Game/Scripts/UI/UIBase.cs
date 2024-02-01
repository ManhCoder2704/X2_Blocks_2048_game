using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIBase : MonoBehaviour
{
    [SerializeField] protected CanvasGroup _canvasGroup;
    [SerializeField] protected bool _isPopup;


    public CanvasGroup CanvasGroup => _canvasGroup;

    public bool IsPopup { get => _isPopup; set => _isPopup = value; }
}
