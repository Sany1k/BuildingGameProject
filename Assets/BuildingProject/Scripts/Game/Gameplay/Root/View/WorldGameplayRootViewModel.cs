using System;
using ObservableCollections;
using R3;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldGameplayRootViewModel
{
    public readonly IObservableCollection<BuildingViewModel> AllBuildings;

    private readonly ResourcesService resourcesService;

    public WorldGameplayRootViewModel(BuildingsService buildingsService, ResourcesService resourcesService)
    {
        AllBuildings = buildingsService.AllBuildings;
        this.resourcesService = resourcesService;

        resourcesService.ObservableResource(ResourceType.SoftCurrency).Subscribe(newValue => Debug.Log($"SoftCurrency: {newValue}"));
        resourcesService.ObservableResource(ResourceType.HardCurrency).Subscribe(newValue => Debug.Log($"HardCurrency: {newValue}"));
        resourcesService.ObservableResource(ResourceType.Wood).Subscribe(newValue => Debug.Log($"Wood: {newValue}"));
    }

    public void HandleTestInput()
    {
        var rResourceType = (ResourceType)Random.Range(0, Enum.GetNames(typeof(ResourceType)).Length);
        var rValue = Random.Range(1, 1001);
        var rOperation = Random.Range(0, 2);

        if (rOperation == 0)
        {
            resourcesService.AddResources(rResourceType, rValue);
            return;
        }

        resourcesService.TrySpendResources(rResourceType, rValue);
    }
}
