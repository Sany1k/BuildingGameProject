public class GameplayEnterParams : SceneEnterParams
{
    public int MapId { get; }

    public GameplayEnterParams(int mapId) : base(Scenes.GAMEPLAY)
    {
        MapId = mapId;
    }
}
