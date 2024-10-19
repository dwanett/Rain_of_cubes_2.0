using UnityEngine;

public class SpawnerBomb : Spawner<Bomb>
{
    public void Spawn(Vector3 position)
    {
        EntityPool.Get().transform.position = position;
    }
    
    protected override void ActionOnRelease(Bomb bomb)
    {
        base.ActionOnRelease(bomb);
        bomb.Exploded -= EntityPool.Release;
    }

    protected override void ActionOnGet(Bomb bomb)
    {
        base.ActionOnGet(bomb);
        bomb.Exploded += EntityPool.Release;
        bomb.ChangeColor();
    }
    
    protected override Bomb Instantiate()
    {
        return Instantiate(PrefabEntity);
    }
}