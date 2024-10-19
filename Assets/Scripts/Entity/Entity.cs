using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected Renderer Renderer;
    [SerializeField] protected float MinTimeLive;
    [SerializeField] protected float MaxTimeLive;
    
    protected Color DefaultColor;
    
    protected float TimeLive { get; private set; }
    
    private void Awake()
    {
        DefaultColor = Renderer.material.color;
        TimeLive = Random.Range(MinTimeLive, MaxTimeLive);
    }
    
    private void OnValidate()
    {
        if (MinTimeLive > MaxTimeLive)
        {
            MinTimeLive = MaxTimeLive - 1;
        }
    }
    
    public void EnableEntity()
    {
        RestoreDefaultColor();
        TimeLive = Random.Range(MinTimeLive, MaxTimeLive);
        gameObject.SetActive(true);
    }
    
    public void DisableEntity()
    {
        gameObject.SetActive(false);
    }
    
    private void RestoreDefaultColor()
    {
        Renderer.material.color = DefaultColor;
    }
}