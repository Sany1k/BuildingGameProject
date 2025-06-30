using ObservableCollections;
using R3;
using System.Linq;

public class Map
{
    public ObservableList<Entity> Entities { get; } = new();
    public int Id => Origin.Id;

    public MapData Origin { get; }

    public Map(MapData mapState)
    {
        Origin = mapState;
        mapState.Entities.ForEach(entityData => Entities.Add(EntitiesFactory.CreateEntity(entityData)));

        Entities.ObserveAdd().Subscribe(e =>
        {
            var addedEntity = e.Value;
            mapState.Entities.Add(addedEntity.Origin);
        });

        Entities.ObserveRemove().Subscribe(e =>
        {
            var removedEntity = e.Value;
            var removedEntityData = mapState.Entities.FirstOrDefault(entity => entity.UniqueId == removedEntity.UniqueId);
            mapState.Entities.Remove(removedEntityData);
        });
    }
}
