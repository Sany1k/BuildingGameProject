using UnityEngine;

[CreateAssetMenu(fileName = "BuildingLevelSettings", menuName = "Game Settings/Entities/Buildings/New Building Level Settings")]
public class BuildingLevelSettings : EntityLevelSettings
{
    [field: SerializeField] public double BaseIncome { get; private set; }
}
