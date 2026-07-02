using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform nozzle;
    [SerializeField] private Transform turretTransform;
    [SerializeField] private Transform barrelTransform;

    [Header("Force Settings")]
    [SerializeField] private float forwardForce = 15f;   // kekuatan maju (Z)
    [SerializeField] private float upwardForce = 4f;    // kekuatan ke atas (Y)

    private TrajectoryPredictor _trajectory;

    private void Start()
    {
        _trajectory = GetComponentInChildren<TrajectoryPredictor>();
        GameController.Instance.OnSwiped += Instance_OnSwiped;
        GameController.Instance.OnCannonMoved += Instance_OnCannonMoved;
    }

    private void OnDestroy()
    {
        GameController.Instance.OnSwiped -= Instance_OnSwiped;
        GameController.Instance.OnCannonMoved -= Instance_OnCannonMoved;
    }

    private void Instance_OnSwiped(SwipeData _data)
    {
        var _prefab = GameController.Instance.bombPrefab;
        var bomb = Instantiate(_prefab, nozzle.position, nozzle.rotation);

        Vector3 force = CalculateForce();
        bomb.Shoot(force);

        _trajectory.HideTrajectory();
    }

    private void Instance_OnCannonMoved(Vector2 cannonForce)
    {
        turretTransform.rotation = Quaternion.Euler(0f, cannonForce.x / 10f, 0f);

        Quaternion pitch = Quaternion.Euler(-cannonForce.y / 40f, 0f, 0f);
        barrelTransform.rotation = turretTransform.rotation * pitch;

        Vector3 force = CalculateForce();
        _trajectory.ShowTrajectory(force);
    }

    private Vector3 CalculateForce()
    {
        Vector3 dir = nozzle.forward * forwardForce
                    + nozzle.up * upwardForce;

        return dir;
    }
}