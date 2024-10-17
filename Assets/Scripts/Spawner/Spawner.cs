using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : EntityObjectPool
{
    public event Action Spawned;
    
    [SerializeField] protected T _prefabEntity;
    [field: SerializeField] public int MaxSpawn { get; private set; }
    
    public ObjectPool<T> EntityPool { get; private set; }
    
    private void Awake()
    {
        EntityPool = new ObjectPool<T>(
            Instantiate,
            ActionOnGet, 
            ActionOnRelease, 
            Destroy, 
            false, 
            MaxSpawn, 
            MaxSpawn);
    }
    
    protected abstract void ActionOnRelease(T entity);

    protected virtual void ActionOnGet(T entity)
    {
        Spawned?.Invoke();
    }
    
    protected abstract T Instantiate();
}