using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform nozzle;
    [SerializeField] private Transform cannonObj;

    [Header("Force Settings")]
    [SerializeField] private float forwardForce = 15f;   // kekuatan maju (Z)
    [SerializeField] private float lateralForce = 8f;    // kekuatan samping (X)
    [SerializeField] private float upwardForce = 4f;    // kekuatan ke atas (Y)

    private void Start()
    {
        GameController.Instance.OnSwiped += Instance_OnSwiped;
        GameController.Instance.OnCannonMoved += Instance_OnCannonMoved;
    }

    private void OnDestroy()
    {
        GameController.Instance.OnSwiped -= Instance_OnSwiped;
        GameController.Instance.OnCannonMoved -= Instance_OnCannonMoved;
    }

    private void Instance_OnCannonMoved(float deltaX)
    {
        cannonObj.rotation = Quaternion.Euler(0f, deltaX / 10f, 0f);
    }

    private void Instance_OnSwiped(SwipeData _data)
    {
        var _prefab = GameController.Instance.bombPrefab;
        var bomb = Instantiate(_prefab, nozzle.position, nozzle.rotation);

        float x = _data.normalizedX;
        float y = 10f;
        float z = 10f;
        Vector3 force = new Vector3(x,y,z);
        bomb.Shoot(force);
    }
}