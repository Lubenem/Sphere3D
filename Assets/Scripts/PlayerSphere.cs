using UnityEngine;
using System;

public class PlayerSphere : MonoBehaviour
{
    private BulletSphere _currBullet;
    private bool _isScaling;

    [SerializeField] private BulletSphere _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPos;
    [SerializeField] private float _scaleSpeed = 1f;
    [SerializeField] private Vector3 _minScale = new Vector3(0.12f, 0.12f, 0.12f);
    [SerializeField] private Transform _path;

    public bool inputBlock;

    private void Start()
    {
        GameManager.instance.playerSphere = this;
    }

    private void Update()
    {
        if (_isScaling)
        {
            transform.localScale = Vector3.Lerp(transform.localScale,
            Vector3.zero, _scaleSpeed * Time.deltaTime);

            UpdatePathScale();

            if (transform.localScale.magnitude < _minScale.magnitude)
            {
                Shoot();
                inputBlock = true;
            }

        }

    }

    private void UpdatePathScale()
    {
        _path.localScale = new Vector3(transform.localScale.x,
         _path.localScale.y, _path.localScale.z);
    }

    private void OnMouseDown()
    {
        if (inputBlock)
            return;

        _currBullet = Instantiate(_bulletPrefab, _bulletSpawnPos);
        _isScaling = true;
    }

    private void OnMouseUp()
    {
        if (inputBlock)
            return;

        Shoot();
    }

    private void Shoot()
    {
        _currBullet.Shoot();
        _isScaling = false;
        GameManager.instance.path.PathCheck();
    }

    public void KillCurrBullet()
    {
        if (_currBullet != null)
            Destroy(_currBullet.gameObject);
    }
}