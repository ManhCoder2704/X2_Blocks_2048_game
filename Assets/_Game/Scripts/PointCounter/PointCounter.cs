using DG.Tweening;
using System;
using UnityEngine;

public class PointCounter : MonoBehaviour, IPoolable<PointCounter>
{
    [SerializeField] private TMPro.TMP_Text _pointTxt;

    private Action<PointCounter> _returnAction;
    public void Initialize(Action<PointCounter> returnAction)
    {
        _returnAction = returnAction;
    }

    public void ReturnToPool()
    {
        _returnAction?.Invoke(this);
    }

    public Tween ShowPoint(int point, Vector3 position)
    {
        transform.position = position;
        BigNumber.FormatLargeNumberPowerOfTwo(_pointTxt, point, "+");
        _pointTxt.alpha = 1;

        return transform.DOMoveY(position.y - 0.75f, 1f)
            .SetEase(Ease.InOutSine)
            .OnStart(() =>
            {
                this.gameObject.SetActive(true);
                _pointTxt.DOFade(0, 1f)
                    .SetEase(Ease.Linear);
            })
            .OnComplete(() => ReturnToPool());
    }

}
