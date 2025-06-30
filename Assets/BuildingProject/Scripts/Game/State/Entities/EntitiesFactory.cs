using System;

public class EntitiesFactory
{
    public static Entity CreateEntity(EntityData entityData)
    {
        switch (entityData.Type)
        {
            case EntityType.Building: 
                return new BuildingEntity(entityData as BuildingEntityData);
            case EntityType.Resource:
                return new ResourceEntity(entityData as ResourceEntityData);

            default:
                throw new Exception("Unsupported entity type: " + entityData.Type);
        }
    }
}
