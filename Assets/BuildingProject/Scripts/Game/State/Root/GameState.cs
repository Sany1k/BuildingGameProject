using System.Collections.Generic;

public class GameState
{
    public List<MapData> Maps { get; set; }
    public List<ResourceData> Resources { get; set; }
    public int GlobalEntityId { get; set; }
    public int CurrentMapId { get; set; }

    public int CreateEntityId()
    {
        return GlobalEntityId++;
    }
}
