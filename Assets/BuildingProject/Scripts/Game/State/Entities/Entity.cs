using R3;
using UnityEngine;

public abstract class Entity
{
    public EntityData Origin { get; }
    public EntityType Type => Origin.Type;
    public int UniqueId => Origin.UniqueId;
    public string ConfigId => Origin.ConfigId;

    public readonly ReactiveProperty<Vector2Int> Position;

    protected Entity(EntityData data)
    {
        Origin = data;
        Position = new ReactiveProperty<Vector2Int>(data.Position);
        Position.Subscribe(newPosition => data.Position = newPosition);
    }
}
