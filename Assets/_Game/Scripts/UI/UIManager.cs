
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Image _background;
    [SerializeField] private CanvasGroup _menu;

    [Header("UI Components")]
    [SerializeField] private List<UIBase> _uiComponents;
    [SerializeField] private float _fadeDuration = 0.2f;
    [SerializeField] private Transform _uiContainer;
    [SerializeField] private GameObject _loadingImage;


    private Dictionary<UIType, UIBase> _uiDict = new Dictionary<UIType, UIBase>();
    private UIBase _currentActivePage;
    private UIBase _currentActivePopup;
    private UIBase _currentActiveUI;
    private Stack<UIBase> _popupStack = new Stack<UIBase>();
    private Tween _loadingTween;
    public Button rankBtn;
    public Button shopBtn;
    void Awake()
    {
        FirstLoad();
    }

    private void FirstLoad()
    {
        _loadingTween = _loadingImage.transform.DORotate(new Vector3(0, 0, -360), 1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1);
        StartLoading();
        StartCoroutine(FirstLoadCo());
    }

    private IEnumerator FirstLoadCo()
    {
        yield return new WaitForSeconds(2f);
        StopLoading();
        OpenUIOrPopup(UIType.HomeUI);
    }

    private void StartLoading()
    {
        _loadingImage.SetActive(true);
        _loadingTween.Restart();
    }

    private void StopLoading()
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

    public void OpenUIOrPopup(UIType uiType)
    {

        UIBase ui = GetUIReference(uiType);

        if (_currentActiveUI == ui) return;
        if (uiType == UIType.PlayUI)
        {
            _menu.gameObject.SetActive(false);
            _background.gameObject.SetActive(false);
            GameplayManager.Instance.ChangeGameState(GameStateEnum.Playing);

        }
        else if (uiType == UIType.HomeUI)
        {
            _menu.gameObject.SetActive(true);
            _background.gameObject.SetActive(true);
            GameplayManager.Instance.ChangeGameState(GameStateEnum.Prepare);

        }
        else
        {
            GameplayManager.Instance.ChangeGameState(GameStateEnum.Pause);
        }
        if (ui.IsPopup)
        {
            Debug.Log("Open popup");
            _popupStack.Push(ui);
            _currentActiveUI.CanvasGroup.interactable = false;
            ui.CanvasGroup.interactable = true;
            ui.gameObject.SetActive(true);
            ui.CanvasGroup.DOFade(1f, _fadeDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                _currentActivePopup = ui;
                ui.CanvasGroup.interactable = true;
            });
        }
        else
        {
            ChangeUI(_currentActiveUI, ui).OnComplete(() =>
            {
                _currentActiveUI = ui;
                _currentActivePage = ui;
            });
        }
    }

    public void ClosePopup(UIBase currentPopup)
    {
        if (_popupStack.Count == 0) return;
        UIBase checkLastPopup = _popupStack.Pop();
        if (checkLastPopup != currentPopup)
        {
            Debug.LogError("Not this popup");
            _popupStack.Push(checkLastPopup);
            return;
        }
        if (_popupStack.Count == 0)
        {
            //if (_currentActivePage == )
            //{

            //}
            Debug.Log("Open last page");
            ChangeUI(currentPopup, _currentActivePage);
        }
        else
        {
            Debug.Log("Open last popup");
            UIBase lastPopup = _popupStack.Pop();
            ChangeUI(currentPopup, lastPopup);
            _currentActivePopup = lastPopup;
            _popupStack.Push(lastPopup);
        }
    }

    private Sequence ChangeUI(UIBase closeUI, UIBase openUI)
    {
        Sequence sequence = DOTween.Sequence();
        if (closeUI != null)
        {
            closeUI.CanvasGroup.interactable = false;
            sequence.Join(closeUI.CanvasGroup.DOFade(0f, _fadeDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    closeUI.gameObject.SetActive(false);
                }));
        }

        openUI.gameObject.SetActive(true);
        if (!openUI.IsPopup && _currentActivePopup != null)
        {
            _popupStack.Clear();
            _currentActivePopup.gameObject.SetActive(false);
            _currentActivePopup.CanvasGroup.interactable = false;
            _currentActivePopup.CanvasGroup.alpha = 0f;
            _currentActivePopup = null;
        }

        sequence.Join(openUI.CanvasGroup.DOFade(1f, _fadeDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                openUI.CanvasGroup.interactable = true;
            }));
        _currentActiveUI = openUI;
        return sequence;
    }
}
