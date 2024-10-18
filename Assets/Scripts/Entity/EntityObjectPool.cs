using UnityEngine;

public abstract class EntityObjectPool : MonoBehaviour
{
    [SerializeField] protected Renderer Renderer;
    [SerializeField] protected float MinTimeLive;
    [SerializeField] protected float MaxTimeLive;
    
    protected Color DefaultColor;
    
    public float TimeLive { get; protected set; }
    
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
    
    public void EnabledEntity()
    {
        RestoreDefaultColor();
        TimeLive = Random.Range(MinTimeLive, MaxTimeLive);
        gameObject.SetActive(true);
    }
    
    public void DisabledEntity()
    {
        gameObject.SetActive(false);
    }
    
    private void RestoreDefaultColor()
    {
        Renderer.material.color = DefaultColor;
    }
}