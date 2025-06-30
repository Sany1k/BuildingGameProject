using ObservableCollections;
using R3;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsService
{
    private readonly ICommandProcessor cmd;
    private readonly ObservableList<BuildingViewModel> _allBuildings = new();
    private readonly Dictionary<int, BuildingViewModel> _buildingsMap = new();
    private readonly Dictionary<string, BuildingSettings> buildingSettingsMap = new();

    public IObservableCollection<BuildingViewModel> AllBuildings => _allBuildings;

    public BuildingsService(IObservableCollection<Entity> entities, EntitiesSettings buildingsSettings, ICommandProcessor cmd)
    {
        this.cmd = cmd;

        foreach (var buildingSettings in buildingsSettings.Buildings)
        {
            buildingSettingsMap[buildingSettings.ConfigId] = buildingSettings;
        }

        foreach (var entity in entities)
        {
            if (entity is BuildingEntity buildingEntity)
                CreateBuildingViewModel(buildingEntity);
        }

        entities.ObserveAdd().Subscribe(e =>
        {
            var entity = e.Value;

            if (entity is BuildingEntity buildingEntity)
                CreateBuildingViewModel(buildingEntity);
        });

        entities.ObserveRemove().Subscribe(e =>
        {
            var entity = e.Value;

            if (entity is BuildingEntity buildingEntity)
                RemoveBuildingViewModel(buildingEntity);
        });
    }

    public bool PlaceBuidling(string BuildingConfigId, Vector2Int position, int level)
    {
        var command = new CmdPlaceEntity(EntityType.Building, position, BuildingConfigId, level);
        var result = cmd.Process(command);

        return result;
    }

    public bool MoveBuilding(string buildingEntityId, Vector3Int position)
    {
        throw new NotImplementedException();
    }

    public bool DeleteBuilding(string buildingEntityId)
    {
        throw new NotImplementedException();
    }

    private void CreateBuildingViewModel(BuildingEntity buildingEntity)
    {
        var buildingSettings = buildingSettingsMap[buildingEntity.ConfigId];
        var buildingViewModel = new BuildingViewModel(buildingEntity, buildingSettings, this);

        _allBuildings.Add(buildingViewModel);
        _buildingsMap[buildingEntity.UniqueId] = buildingViewModel;
    }

    private void RemoveBuildingViewModel(BuildingEntity buildingEntity)
    {
        if (_buildingsMap.TryGetValue(buildingEntity.UniqueId, out var buildingViewModel))
        {
            _allBuildings.Remove(buildingViewModel);
            _buildingsMap.Remove(buildingEntity.UniqueId);
        }
    }
}
