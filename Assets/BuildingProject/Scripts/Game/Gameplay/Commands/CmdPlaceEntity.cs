using UnityEngine;

public class CmdPlaceEntity : ICommand
{
    public readonly EntityType EntityType;
    public readonly Vector2Int Position;
    public readonly string EntityConfigId;
    public readonly int Level;

    public CmdPlaceEntity(EntityType entityType, Vector2Int position, string entityConfigId, int level)
    {
        EntityType = entityType;
        Position = position;
        EntityConfigId = entityConfigId;
        Level = level;
    }
}
