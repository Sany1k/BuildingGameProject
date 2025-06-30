﻿using R3;

public class ResourceViewModel
{
    public readonly ResourceType ResourceType;
    public readonly ReadOnlyReactiveProperty<int> Amount;

    public ResourceViewModel(Resource resource)
    {
        ResourceType = resource.ResourceType;
        Amount = resource.Amount;
    }
}
