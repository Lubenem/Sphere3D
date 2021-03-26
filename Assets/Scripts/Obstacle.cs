using UnityEngine;
using System;

public class Obstacle : MonoBehaviour
{
    private MeshRenderer _mR;
    private Collider _collider;
    [SerializeField] private Color _killedColor;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        _mR = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _mR.material.color = _killedColor;
        _collider.enabled = false;
        Destroy(this.gameObject);
    }
}