using UnityEngine;
using UnityEngine.InputSystem;

public class GamePage : Page
{
    [Header("Swipe Settings (dalam cm)")]
    [SerializeField] private float minSwipeCm = 0.5f;
    [SerializeField] private float maxSwipeCm = 4.0f;

    [SerializeField] private SwipeData swipeData;

    private float _minSwipePx;
    private float _maxSwipePx;
    private Vector2 _startPos;
    private bool _isTouching;

    protected override void Start()
    {
        base.Start();

        float dpi = Screen.dpi > 0 ? Screen.dpi : 96f;
        float cmToInch = 0.3937f;

        _minSwipePx = minSwipeCm * cmToInch * dpi;
        _maxSwipePx = maxSwipeCm * cmToInch * dpi;

        Debug.Log($"[Swipe] DPI: {dpi} | Min: {_minSwipePx:F1}px | Max: {_maxSwipePx:F1}px");
    }

    private void Update()
    {
        if (Touchscreen.current != null)
        {
            var touch = Touchscreen.current.primaryTouch;

            if (touch.press.wasPressedThisFrame)
            {
                _startPos = touch.position.ReadValue();
                _isTouching = true;
            }

            if (_isTouching) 
            {
                float a = _startPos.x;
                float b = touch.position.ReadValue().x;
                GameController.Instance.MoveCannon(b - a);
            }

            if (touch.press.wasReleasedThisFrame && _isTouching)
            {
                _isTouching = false;
                ProcessSwipe(touch.position.ReadValue());
            }
        }

        if (Mouse.current != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                _startPos = Mouse.current.position.ReadValue();
                _isTouching = true;
            }

            if (Mouse.current.leftButton.wasReleasedThisFrame && _isTouching)
            {
                _isTouching = false;
                ProcessSwipe(Mouse.current.position.ReadValue());
            }
        }
    }

    private void ProcessSwipe(Vector2 endPos)
    {
        Vector2 delta = endPos - _startPos;

        float absX = Mathf.Abs(delta.x);

        // Abaikan swipe terlalu pendek
        if (absX < _minSwipePx) return;


        // Normalisasi arah horizontal: -1.0 (kiri) hingga +1.0 (kanan)
        float normX = Mathf.Clamp(delta.x / _maxSwipePx, -1f, 1f);

        swipeData.startPos = _startPos;
        swipeData.endPos = endPos;
        swipeData.distance = Vector2.Distance(_startPos, endPos);
        swipeData.normalizedX = normX;


        GameController.Instance.Swipe(swipeData);
    }
}