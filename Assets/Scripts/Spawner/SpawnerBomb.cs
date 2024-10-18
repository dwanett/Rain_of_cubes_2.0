using UnityEngine;

public class SpawnerBomb : Spawner<Bomb>
{
    public void Spawn(Vector3 position)
    {
        EntityPool.Get().transform.position = position;
    }
    
    protected override void ActionOnRelease(Bomb bomb)
    {
        bomb.DisabledEntity();
        bomb.Exploded -= DespawnBomb;
    }

    protected override void ActionOnGet(Bomb bomb)
    {
        base.ActionOnGet(bomb);
        bomb.EnabledEntity();
        bomb.ChangeColor();
        bomb.Exploded += DespawnBomb;
    }
    
    protected override Bomb Instantiate()
    {
        return Instantiate(PrefabEntity);
    }
    
    private void DespawnBomb(Bomb bomb)
    {
        EntityPool.Release(bomb);
    }
}