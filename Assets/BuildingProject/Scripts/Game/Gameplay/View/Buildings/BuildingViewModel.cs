using R3;
using System.Collections.Generic;
using UnityEngine;

public class BuildingViewModel
{
    private readonly BuildingEntity buildingEntity;
    private readonly BuildingSettings buildingSettings;
    private readonly BuildingsService buildingsService;
    private readonly Dictionary<int, BuildingLevelSettings> levelSettingsMap = new();

    public readonly int BuildingEntityId;
    public readonly string ConfigId;

    public ReadOnlyReactiveProperty<Vector2Int> Position { get; }
    public ReadOnlyReactiveProperty<int> Level { get; }

    public BuildingViewModel(BuildingEntity buildingEntity, BuildingSettings buildingSettings, BuildingsService buildingsService)
    {
        ConfigId = buildingEntity.ConfigId;
        BuildingEntityId = buildingEntity.UniqueId;
        Level = buildingEntity.Level;

        this.buildingEntity = buildingEntity;
        this.buildingSettings = buildingSettings;
        this.buildingsService = buildingsService;

        foreach (var buildingLevelSettings in buildingSettings.Levels)
        {
            levelSettingsMap[buildingLevelSettings.Level] = buildingLevelSettings;
        }

        Position = buildingEntity.Position;
    }

    public BuildingLevelSettings GetLevelSettings(int level)
    {
        return levelSettingsMap[level];
    }
}
