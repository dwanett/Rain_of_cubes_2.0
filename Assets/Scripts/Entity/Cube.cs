using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
public class Cube : EntityObjectPool
{
    public event Action<Cube> Died;
    
    private void OnCollisionEnter(Collision other)
    {
        Floor cube = other.gameObject.GetComponent<Floor>();

        if (cube != null && TryChangeColor(new Color(Random.value, Random.value, Random.value)))
            StartCoroutine(DespawnCube(this));
    }
    
    public bool TryChangeColor(Color color)
    {
        bool isChanged = _renderer.material.color == _defaultColor;
        
        if (isChanged)
            _renderer.material.color = color;

        return isChanged;
    }
    
    private IEnumerator DespawnCube(Cube cube)
    {
        yield return new WaitForSeconds(cube.TimeLive);
        Died?.Invoke(this);
    }
}
