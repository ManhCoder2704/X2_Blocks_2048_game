using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using System;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject _board;
    [SerializeField] private CanvasGroup _menu;
    [SerializeField] private Image _background;
    [Header("UI Components")]
    [SerializeField] private List<UIBase> _uiComponents;
    [SerializeField] private float _fadeDuration = 0.2f;
    [SerializeField] private Transform _uiContainer;
    [SerializeField] private GameObject _loadingImage;
    [SerializeField] private ConfirmUI _confirmUI;
    [SerializeField] private NotificationUI _noticUI;


    private Dictionary<UIType, UIBase> _uiDict = new Dictionary<UIType, UIBase>();
    private UIBase _currentActiveUI;
    private Stack<UIBase> _popupStack = new Stack<UIBase>();
    private Tween _loadingTween;
    public Button rankBtn;
    public Button shopBtn;

    public Image Background { get => _background; set => _background = value; }
    public UIBase CurrentActiveUI { get => _currentActiveUI; set => _currentActiveUI = value; }

    void Awake()
    {
        _menu.interactable = false;
        _loadingTween = _loadingImage.transform.DORotate(new Vector3(0, 0, -360), 1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1);
        StartLoading();
    }
    public void OpenConfirmUI(Action agreeCallBack, Action disagreeCallBack, string content, Action onOpen)
    {
        onOpen?.Invoke();
        _confirmUI.gameObject.SetActive(true);
        _currentActiveUI.CanvasGroup.interactable = false;
        agreeCallBack += () => _currentActiveUI.CanvasGroup.interactable = true;
        disagreeCallBack += () => _currentActiveUI.CanvasGroup.interactable = true;
        _confirmUI.OnInit(agreeCallBack, disagreeCallBack, content);
    }
    public void OpenNoticUI(string content)
    {
        _noticUI.gameObject.SetActive(true);
        _noticUI.SetContent(content);
    }
    public void FirstLoadUI()
    {
        StopLoading();
        _menu.interactable = true;
        OpenUI(UIType.HomeUI);
    }

    public void StartLoading()
    {
        _loadingImage.SetActive(true);
        _loadingTween.Play();
    }

    public void StopLoading()
    {
        _loadingTween.Pause();
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
        StopLoading();
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
        StopLoading();
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
    public void ChangeBackground(Sprite bg, int id)
    {
        _background.sprite = bg;
        RuntimeDataManager.Instance.SettingData.ThemeIndex = id;
    }
}
