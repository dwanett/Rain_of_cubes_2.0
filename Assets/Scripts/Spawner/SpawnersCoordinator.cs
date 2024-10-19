using UnityEngine;

public class SpawnersCoordinator : MonoBehaviour
{
    [SerializeField] private SpawnerBomb _spawnerBomb;
    [SerializeField] private SpawnerCube _spawnerCube;

    private void OnEnable()
    {
        _spawnerCube.Despawned += _spawnerBomb.Spawn;
    }

    private void OnDisable()
    {
        _spawnerCube.Despawned -= _spawnerBomb.Spawn;
    }
}
