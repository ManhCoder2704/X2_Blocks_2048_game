using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraViewHandle : Singleton<CameraViewHandle>
{
    private Camera _camera;
    private const float _defaultSize = 5f;
    private const float _defaultAspect = 0.5625f; // 1080 : 1920
    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Start()
    {
        CalculateOrthographicSize();
    }

#if UNITY_EDITOR
    private void Update()
    {
        CalculateOrthographicSize();
    }
#endif

    private void CalculateOrthographicSize()
    {
        _camera.orthographicSize = _defaultSize * _defaultAspect / ((float)Screen.width / Screen.height);
    }
}
