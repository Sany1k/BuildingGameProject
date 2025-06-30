using ObservableCollections;
using R3;
using System.Collections.Generic;

public class ResourcesService
{
    public readonly ObservableList<ResourceViewModel> Resources = new();

    private readonly Dictionary<ResourceType, ResourceViewModel> resourcesMap = new();
    private readonly ICommandProcessor cmd;

    public ResourcesService(ObservableList<Resource> resources, ICommandProcessor cmd)
    {
        this.cmd = cmd;
        resources.ForEach(CreateResourceViewModel);
        resources.ObserveAdd().Subscribe(e => CreateResourceViewModel(e.Value));
        resources.ObserveRemove().Subscribe(e => RemoveResourceViewModel(e.Value));
    }

    public bool AddResources(ResourceType resourceType, int amount)
    {
        var command = new CmdResourcesAdd(resourceType, amount);

        return cmd.Process(command);
    }

    public bool TrySpendResources(ResourceType resourceType, int amount)
    {
        var command = new CmdResourcesSpend(resourceType, amount);

        return cmd.Process(command);
    }

    public bool IsEnoughResources(ResourceType resourceType, int amount)
    {
        if (resourcesMap.TryGetValue(resourceType, out var resourceViewModel))
        {
            return resourceViewModel.Amount.CurrentValue >= amount;
        }

        return false;
    }

    public Observable<int> ObservableResource(ResourceType resourceType)
    {
        if (resourcesMap.TryGetValue(resourceType, out var resourceViewModel))
        {
            return resourceViewModel.Amount;
        }

        throw new System.Exception($"Resource of type {resourceType} doesn't exist.");
    }

    private void CreateResourceViewModel(Resource resource)
    {
        var resourceViewModel = new ResourceViewModel(resource);
        resourcesMap[resource.ResourceType] = resourceViewModel;

        Resources.Add(resourceViewModel);
    }

    private void RemoveResourceViewModel(Resource resource)
    {
        if (resourcesMap.TryGetValue(resource.ResourceType, out var resourceViewModel))
        {
            Resources.Remove(resourceViewModel);
            resourcesMap.Remove(resource.ResourceType);
        }
    }
}
