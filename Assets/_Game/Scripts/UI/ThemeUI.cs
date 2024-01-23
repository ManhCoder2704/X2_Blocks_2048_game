using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeUI : MonoBehaviour
{
    [SerializeField] private Button _switchBtn;
    [SerializeField] private Transform _toggleBtn;

    private float _duration = 0.5f;
    void Start()
    {
        _switchBtn.onClick.AddListener(Switch);
    }
    private void Switch()
    {
        _switchBtn.interactable = false;
        _toggleBtn.DOLocalMoveX(-_toggleBtn.localPosition.x, _duration)
            .OnComplete(() =>
            {
                _switchBtn.interactable = true;
            });
    }
}
