using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : EntityObjectPool
{
    public event Action<Bomb> Exploded;
    
    [SerializeField] private float _forceExplode;
    [SerializeField] private float _radiusExplode;
    [SerializeField] private Color _targetColor;
    private Coroutine _coroutineChangeAlphaColor = null;
    
    private void Explode()
    {
        Vector3 explosionPos = transform.position;
        
        List<Collider> colliders = new List<Collider>(Physics.OverlapSphere(explosionPos, _radiusExplode));
        colliders.Remove(GetComponent<Collider>());
        
        foreach (Collider collider in colliders)
        {
            Rigidbody rigidbody = collider.attachedRigidbody;
            if (rigidbody != null)
                rigidbody.AddExplosionForce(_forceExplode, explosionPos, _radiusExplode);
        }
    }

    public void ChangeColor()
    {
        if (_coroutineChangeAlphaColor == null)
            _coroutineChangeAlphaColor = StartCoroutine(ChangeAlphaColor());
    }
    
    private IEnumerator ChangeAlphaColor()
    {
        for (float i = 0.0f; i < TimeLive; i += Time.deltaTime)
        {
            yield return null;
            _renderer.material.color = Color.Lerp(_defaultColor, _targetColor, i / TimeLive);
        }

        _coroutineChangeAlphaColor = null;
        Explode();
        Exploded.Invoke(this);
    }
}