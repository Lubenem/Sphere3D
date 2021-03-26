using UnityEngine;

public class BulletSphere : MonoBehaviour
{
    Rigidbody _rb;

    [SerializeField] private Vector3 _velocity;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = _velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}