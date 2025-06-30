using System;
using UnityEngine;

public static class EntitiesDataFactory
{
    public static EntityData CreateEntity(EntityInitialStateSettings initialSettings)
    {
        switch (initialSettings.EntityType)
        {
            case EntityType.Building:
                return CreateEntity<BuildingEntityData>(initialSettings);
            default:
                throw new System.Exception($"Not implemented entity creation: {initialSettings.EntityType}");
        }
    }

    private static T CreateEntity<T>(EntityInitialStateSettings initialSettings) where T : EntityData, new()
    {
        return CreateEntity<T>(
            initialSettings.EntityType,
            initialSettings.ConfigId,
            initialSettings.Level,
            initialSettings.InitialPosition);
    }

    public static T CreateEntity<T>(EntityType type, string configId, int level, Vector2Int position) where T : EntityData, new()
    {
        var entity = new T
        {
            Type = type,
            ConfigId = configId,
            Level = level,
            Position = position
        };

        switch (entity)
        {
            case BuildingEntityData buildingEntityData:
                UpdateBuildingEntity(buildingEntityData);
                break;
            default:
                throw new System.Exception($"Not implemented entity creation: {type}");
        }

        return entity;
    }

    private static void UpdateBuildingEntity(BuildingEntityData buildingEntity)
    {
        buildingEntity.LastClickedTimeMS = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        buildingEntity.IsAutoCollectionEnabled = false;
    }
}
