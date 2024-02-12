using System;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardNavigator : MonoBehaviour
{
    [SerializeField] private HorizontalLayoutGroup _layoutGroup;
    [SerializeField] private Button _escapeButton;
    [SerializeField] private Button _localBoardButton;
    [SerializeField] private Button _globalBoardButton;
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _unselectedColor;
    [SerializeField] private GameObject _localSelectedLight;
    [SerializeField] private GameObject _globalSelectedLight;

    private bool _isLocalBoard = true;
    private Action _escapeOnclick;
    private Action _localOnclick;
    private Action _globalOnclick;

    private void Awake()
    {
        _escapeButton.onClick.AddListener(EscapeButtonOnclick);
        _localBoardButton.onClick.AddListener(LocalBoardButtonOnclick);
        _globalBoardButton.onClick.AddListener(GlobalBoardButtonOnclick);
    }
    public void Init(Action escapeCallback, Action localCallback, Action globalCallbackk)
    {
        _escapeOnclick = escapeCallback;
        if (escapeCallback == null)
        {
            _escapeButton.gameObject.SetActive(false);
            _layoutGroup.childAlignment = TextAnchor.MiddleCenter;
        }
        else
        {
            _escapeButton.gameObject.SetActive(true);
            _layoutGroup.childAlignment = TextAnchor.MiddleLeft;
        }
        _localOnclick = localCallback;
        _globalOnclick = globalCallbackk;
    }

    private void EscapeButtonOnclick()
    {
        _escapeOnclick?.Invoke();
    }

    private void LocalBoardButtonOnclick()
    {
        if (_isLocalBoard) return;
        _localBoardButton.image.color = _selectedColor;
        _globalBoardButton.image.color = _unselectedColor;
        _localSelectedLight.SetActive(true);
        _globalSelectedLight.SetActive(false);
        _isLocalBoard = true;
        _localOnclick?.Invoke();
    }

    private void GlobalBoardButtonOnclick()
    {
        if (!_isLocalBoard) return;
        _localBoardButton.image.color = _unselectedColor;
        _globalBoardButton.image.color = _selectedColor;
        _localSelectedLight.SetActive(false);
        _globalSelectedLight.SetActive(true);
        _isLocalBoard = false;
        _globalOnclick?.Invoke();
    }
}
