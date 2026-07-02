using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private LayerMask enemyLayer; // set layer "Enemy" di inspector

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Shoot(Vector3 force)
    {
        rb.AddForce(force, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        Instantiate(GameController.Instance.GetExplotionFx(), transform.position, Quaternion.identity);

        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, explosionRadius, enemyLayer);
        foreach (Collider hit in hitEnemies)
        {
            Debug.Log(hit.gameObject.name);
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage, transform.position);
            }
        }

        Destroy(gameObject);
    }

    // Biar radius kelihatan di Scene view saat bomb prefab dipilih
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}