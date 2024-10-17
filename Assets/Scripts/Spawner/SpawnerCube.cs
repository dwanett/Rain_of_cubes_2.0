using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Collider))]
public class SpawnerCube : Spawner<Cube>
{
    [SerializeField] Collider _colliderSpawner;
    [SerializeField] SpawnerBomb _spawnerBomb;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 0, 0.0001f);
    }

    protected override void ActionOnRelease(Cube cube)
    {
        cube.DisabledEntity();
        cube.Died -= EntityPool.Release;
        _spawnerBomb.Spawn(cube.transform.position);
    }

    protected override void ActionOnGet(Cube cube)
    {
        base.ActionOnGet(cube);
        cube.EnabledEntity();
        cube.Died += EntityPool.Release;
        cube.transform.position = RandomSpawnPoint();
    }

    protected override Cube Instantiate()
    {
        return Instantiate(_prefabEntity, RandomSpawnPoint(), Quaternion.identity);
    }

    private void Spawn()
    {
        if (EntityPool.CountActive < MaxSpawn)
            EntityPool.Get();
    }

    private Vector3 RandomSpawnPoint()
    {
        return new Vector3(
            Random.Range(_colliderSpawner.bounds.min.x, _colliderSpawner.bounds.max.x),
            Random.Range(_colliderSpawner.bounds.min.y, _colliderSpawner.bounds.max.y),
            Random.Range(_colliderSpawner.bounds.min.z, _colliderSpawner.bounds.max.z));
    }
}