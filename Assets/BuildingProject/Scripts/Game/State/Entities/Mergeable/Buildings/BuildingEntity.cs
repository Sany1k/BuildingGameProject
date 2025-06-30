using R3;

public class BuildingEntity : MergeableEntity
{
    public readonly ReactiveProperty<double> LastClickedTimeMS;
    public readonly ReactiveProperty<bool> IsAutoCollectionEnable;

    public BuildingEntity(BuildingEntityData data) : base(data)
    {
        LastClickedTimeMS = new ReactiveProperty<double>(data.LastClickedTimeMS);
        LastClickedTimeMS.Subscribe(newLastClickedTimeMS => data.LastClickedTimeMS = newLastClickedTimeMS);
        
        IsAutoCollectionEnable = new ReactiveProperty<bool>(data.IsAutoCollectionEnabled);
        IsAutoCollectionEnable.Subscribe(newValue => data.IsAutoCollectionEnabled = newValue);
    }
}
