using UnityEngine;

public abstract class EntityObjectPool : MonoBehaviour
{
    [SerializeField] protected Renderer _renderer;
    [SerializeField] protected float _minTimeLive;
    [SerializeField] protected float _maxTimeLive;

    public float TimeLive { get; protected set; }
    
    protected Color _defaultColor;
    
    private void Awake()
    {
        _defaultColor = _renderer.material.color;
        TimeLive = Random.Range(_minTimeLive, _maxTimeLive);
    }
    
    private void OnValidate()
    {
        if (_minTimeLive > _maxTimeLive)
        {
            _minTimeLive = _maxTimeLive - 1;
        }
    }
    
    public void EnabledEntity()
    {
        RestoreDefaultColor();
        TimeLive = Random.Range(_minTimeLive, _maxTimeLive);
        gameObject.SetActive(true);
    }
    
    public void DisabledEntity()
    {
        gameObject.SetActive(false);
    }
    
    private void RestoreDefaultColor()
    {
        _renderer.material.color = _defaultColor;
    }
}