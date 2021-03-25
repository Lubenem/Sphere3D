using UnityEngine;
using System;

public class PlayerSphere : MonoBehaviour
{
    [SerializeField] private BulletSphere _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPos;

    private void OnMouseDown()
    {
        BulletSphere bullet = Instantiate(_bulletPrefab, _bulletSpawnPos);
    }
}