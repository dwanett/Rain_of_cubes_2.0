using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
public class Cube : Entity
{
    public event Action<Cube> Died;
    
    private void OnCollisionEnter(Collision other)
    {
        Floor cube = other.gameObject.GetComponent<Floor>();

        if (cube != null && TryChangeColor(new Color(Random.value, Random.value, Random.value)))
            StartCoroutine(DespawnCube());
    }
    
    public bool TryChangeColor(Color color)
    {
        bool isChanged = Renderer.material.color == DefaultColor;
        
        if (isChanged)
            Renderer.material.color = color;

        return isChanged;
    }
    
    private IEnumerator DespawnCube()
    {
        yield return new WaitForSeconds(TimeLive);
        Died?.Invoke(this);
    }
}
