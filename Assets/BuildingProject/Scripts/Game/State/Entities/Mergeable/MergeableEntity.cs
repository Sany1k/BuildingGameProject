using R3;

public abstract class MergeableEntity : Entity
{
    public readonly ReactiveProperty<int> Level;

    protected MergeableEntity(MergeableEntityData data) : base(data)
    {
        Level = new ReactiveProperty<int>(data.Level);
        Level.Subscribe(newValue => data.Level = newValue);
    }
}
