using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public EnemySO enemyData;

    [SerializeField] private GameObject hitFx; // fx kena damage (opsional)
    private float _currentHealth;
    private bool _isDead;

    private NavMeshAgent _agent;
    private Animator _animator;
    private AnimationListener _listener;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _listener = GetComponentInChildren<AnimationListener>();

        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = enemyData.enemySpeed;

        _agent.SetDestination(Vector3.zero);

        _listener.OnAnimationPlayed += _listener_OnAnimationPlayed;

        _currentHealth = enemyData.enemyHp;
    }

    private void OnDestroy()
    {
        _listener.OnAnimationPlayed -= _listener_OnAnimationPlayed;
    }

    private void _listener_OnAnimationPlayed(string key)
    {
        if(key == "Die")
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        _animator.SetFloat("Move", _agent.velocity.magnitude);
    }

    public async void TakeDamage(float damage, Vector3 explosionPosition)
    {
        if (_isDead) return;

        _currentHealth -= damage;
        _animator.Play("Hit");

        if (hitFx != null)
            Instantiate(hitFx, transform.position, Quaternion.identity);

        ApplyKnockback(explosionPosition);

        if (_currentHealth <= 0f)
        {
            Die();
        }
        else
        {
            await Awaitable.WaitForSecondsAsync(enemyData.enemyReactionTime);
            if (_agent != null) { _agent.SetDestination(Vector3.zero); }
        }
    }

    private void ApplyKnockback(Vector3 explosionPosition)
    {
        Vector3 dir = transform.position - explosionPosition;
        dir.y = 0f;
        dir.Normalize();

        float knockbackDistance = 1.5f;
        Vector3 targetPos = transform.position + dir * knockbackDistance;

        if (NavMesh.SamplePosition(targetPos, out NavMeshHit navHit, knockbackDistance, NavMesh.AllAreas))
        {
            _agent.Warp(navHit.position);
        }
    }

    private void Die()
    {
        _isDead = true;
        _agent.isStopped = true;
        _animator.Play("Die");

        GameController.Instance.EnemyDie();
    }
}