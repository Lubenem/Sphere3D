using System.Collections;
using UnityEngine;

public class BulletSphere : MonoBehaviour
{
    private Rigidbody _rb;
    private bool _isScaling = true;
    private Vector3 _targetScale;

    [SerializeField] private Vector3 _velocity;
    [SerializeField] private float _scaleSpeed = 1f;
    [SerializeField] private Vector3 _minScale = new Vector3(0.12f, 0.12f, 0.12f);



    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _targetScale = transform.localScale;
        transform.localScale = _minScale;
    }

    private void Update()
    {
        if (_isScaling)
            transform.localScale = Vector3.Lerp(transform.localScale, _targetScale,
            _scaleSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle" || other.tag == "Target")
            Destroy(this.gameObject, 0.05f);
    }

    public void Shoot()
    {
        if (_rb == null)
            return;

        _isScaling = false;
        _rb.velocity = _velocity;
    }
}