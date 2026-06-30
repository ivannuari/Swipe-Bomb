using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 2f;

    private NavMeshAgent _agent;
    private Animator _animator;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = enemySpeed;

        _animator = GetComponentInChildren<Animator>();

        _agent.SetDestination(Vector3.zero);
    }

    private void Update()
    {
        _animator.SetFloat("Move", _agent.velocity.magnitude);
    }
}
