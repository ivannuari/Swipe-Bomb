using UnityEngine;

public class Bomb : MonoBehaviour
{
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
        if(other.CompareTag("Ground"))
        {
            Instantiate(GameController.Instance.GetExplotionFx(), transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
