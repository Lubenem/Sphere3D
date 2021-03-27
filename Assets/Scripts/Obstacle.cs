using UnityEngine;
using System;
using System.Collections;

public class Obstacle : MonoBehaviour
{
    private MeshRenderer _mR;
    private bool _isDying;

    [SerializeField] private Color _killedColor;
    [SerializeField] private ParticleSystem _explosionPS;
    [SerializeField] private AudioClip _explosionSound;

    public Collider obstacleCollider;

    private void Start()
    {
        obstacleCollider = GetComponent<Collider>();
        _mR = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isDying)
            StartCoroutine(KillRoutine());
    }

    private IEnumerator KillRoutine()
    {
        _isDying = true;
        _mR.material.color = _killedColor;
        yield return new WaitForSeconds(0.2f);
        ParticleSystem explosion = Instantiate(_explosionPS, transform);
        explosion.transform.parent = null;
        SoundManager.instance.Play(_explosionSound, 0.1f);
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
        GameManager.instance.path.PathCheck();
    }
}