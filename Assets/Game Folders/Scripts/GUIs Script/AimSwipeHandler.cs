using UnityEngine;
using UnityEngine.InputSystem;

public class AimSwipeHandler : MonoBehaviour
{
    [Header("Swipe Settings (dalam cm)")]
    [SerializeField] private float minSwipeCm = 0.5f;
    [SerializeField] private float maxSwipeCm = 4.0f;

    [SerializeField] private SwipeData swipeData;

    private float _minSwipePx;
    private float _maxSwipePx;
    private Vector2 _startPos;
    private bool _isTouching;

    private void Start()
    {
        float dpi = Screen.dpi > 0 ? Screen.dpi : 96f;
        float cmToInch = 0.3937f;

        _minSwipePx = minSwipeCm * cmToInch * dpi;
        _maxSwipePx = maxSwipeCm * cmToInch * dpi;
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
                float xa = _startPos.x;
                float xb = touch.position.ReadValue().x;

                float ya = _startPos.y;
                float yb = touch.position.ReadValue().y;

                float forceX = xb - xa;
                float forceY = yb - ya;

                GameController.Instance.MoveCannon(new Vector2(forceX, forceY));
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

        // Normalisasi arah horizontal: -1.0 (kiri) hingga +1.0 (kanan)
        float normX = Mathf.Clamp(delta.x / _maxSwipePx, -1f, 1f);

        swipeData.startPos = _startPos;
        swipeData.endPos = endPos;
        swipeData.distance = Vector2.Distance(_startPos, endPos);
        swipeData.normalizedX = normX;

        GameController.Instance.Swipe(swipeData);
    }
}
