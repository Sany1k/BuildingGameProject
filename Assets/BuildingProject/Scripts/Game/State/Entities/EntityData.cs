using UnityEngine;

public class EntityData
{
    public EntityType Type { get; set; }
    public Vector2Int Position { get; set; }
    public int UniqueId { get; set; }
    public int Level { get; set; }
    public string ConfigId { get; set; }
}
