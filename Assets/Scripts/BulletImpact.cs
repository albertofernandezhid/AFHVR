using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    public float force = 10f;
    public float destroyDelayNoHit = 5f;

    private Rigidbody rb;
    private bool hasCollided = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, destroyDelayNoHit);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasCollided) return;
        hasCollided = true;

        Rigidbody targetRb = collision.rigidbody;
        if (targetRb != null)
        {
            targetRb.WakeUp();
            targetRb.AddForceAtPosition(transform.forward * force, collision.contacts[0].point, ForceMode.Impulse);
        }

        ImmediateDestroy();
    }

    private void ImmediateDestroy()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}