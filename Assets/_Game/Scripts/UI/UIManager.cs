
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject _board;
    [SerializeField] private CanvasGroup _menu;

    [Header("UI Components")]
    [SerializeField] private List<UIBase> _uiComponents;
    [SerializeField] private float _fadeDuration = 0.2f;
    [SerializeField] private Transform _uiContainer;
    [SerializeField] private GameObject _loadingImage;


    private Dictionary<UIType, UIBase> _uiDict = new Dictionary<UIType, UIBase>();
    private UIBase _currentActiveUI;
    private Stack<UIBase> _popupStack = new Stack<UIBase>();
    private Tween _loadingTween;
    public Button rankBtn;
    public Button shopBtn;
    void Awake()
    {
        _loadingTween = _loadingImage.transform.DORotate(new Vector3(0, 0, -360), 1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1);
        StartLoading();
    }

    public void FirstLoadUI()
    {
        StopLoading();
        OpenUI(UIType.HomeUI);
    }

    public void StartLoading()
    {
        _loadingImage.SetActive(true);
        _loadingTween.Restart();
    }

    public void StopLoading()
    {
        _loadingTween.Kill();
        _loadingImage.SetActive(false);
    }

    private UIBase GetUIReference(UIType uiType)
    {
        UIBase ui;
        if (!_uiDict.TryGetValue(uiType, out ui))
        {
            ui = Instantiate(_uiComponents[(int)uiType], _uiContainer);
            _uiDict.Add(uiType, ui);
        }
        return ui;
    }

    public void OpenUI(UIType uiType)
    {

        UIBase ui = GetUIReference(uiType);

        if (_currentActiveUI == ui) return;
        if (uiType == UIType.PlayUI)
        {
            _menu.gameObject.SetActive(false);
            _board.gameObject.SetActive(true);
            GameplayManager.Instance.ChangeGameState(GameStateEnum.Playing);

        }
        else if (uiType == UIType.HomeUI)
        {
            _menu.gameObject.SetActive(true);
            _board.gameObject.SetActive(false);
            GameplayManager.Instance.ChangeGameState(GameStateEnum.Prepare);

        }
        else
        {
            GameplayManager.Instance.ChangeGameState(GameStateEnum.Pause);
        }

        ChangeUI(_currentActiveUI, ui);
    }

    public void ClosePopup(UIBase currentPopup)
    {
        if (_popupStack.Count == 0) return;
        _currentActiveUI = null;
        currentPopup.CanvasGroup.interactable = false;
        currentPopup.CanvasGroup.DOFade(0f, _fadeDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                currentPopup.gameObject.SetActive(false);
            });
        UIBase openUI = _popupStack.Pop();
        UIBase checkUI = GetUIReference(UIType.PlayUI);
        ChangeUI(null, openUI);

        if (GetUIReference(UIType.PlayUI) == _currentActiveUI)
        {
            _menu.gameObject.SetActive(false);
            _board.gameObject.SetActive(true);
            GameplayManager.Instance.ChangeGameState(GameStateEnum.Playing);

        }
        else if (GetUIReference(UIType.HomeUI) == _currentActiveUI)
        {
            _menu.gameObject.SetActive(true);
            _board.gameObject.SetActive(false);
            GameplayManager.Instance.ChangeGameState(GameStateEnum.Prepare);
        }
        else
        {
            GameplayManager.Instance.ChangeGameState(GameStateEnum.Pause);
        }
    }

    private Sequence ChangeUI(UIBase closeUI, UIBase openUI)
    {
        _currentActiveUI = openUI;
        Sequence sequence = DOTween.Sequence();
        if (closeUI != null)
        {
            closeUI.CanvasGroup.interactable = false;
            if (openUI.IsPopup)
            {
                _popupStack.Push(closeUI);
            }
            else
            {
                sequence.Join(closeUI.CanvasGroup.DOFade(0f, _fadeDuration)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        closeUI.gameObject.SetActive(false);
                    }));
                ClearStackUI();
            }
        }

        openUI.gameObject.SetActive(true);

        sequence.Join(openUI.CanvasGroup.DOFade(1f, _fadeDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                openUI.CanvasGroup.interactable = true;
            }));
        return sequence;
    }

    private void ClearStackUI()
    {
        while (_popupStack.Count > 0)
        {
            UIBase temp = _popupStack.Pop();
            temp.gameObject.SetActive(false);
            temp.CanvasGroup.interactable = false;
            temp.CanvasGroup.alpha = 0f;
        }
    }
}
