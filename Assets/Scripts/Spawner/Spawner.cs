using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : Entity
{
    private int _countSpawn = 0;
    [SerializeField] protected T PrefabEntity;
    protected ObjectPool<T> EntityPool { get; private set; }
    
    public event Action<int> Spawned;
    public event Action<Vector3> Despawned;
    
    [field: SerializeField] public int MaxSpawn { get; private set; }
    public int CountInstance { get => EntityPool.CountAll; private set => CountInstance = value; }
    public int CountActive { get => EntityPool.CountActive; private set => CountActive = value; }
    
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

    protected virtual void ActionOnRelease(T entity)
    {
        Despawned?.Invoke(entity.transform.position);
        entity.DisableEntity();
    }

    protected virtual void ActionOnGet(T entity)
    {
        Spawned?.Invoke(++_countSpawn);
        entity.EnableEntity();
    }
    
    protected abstract T Instantiate();
}