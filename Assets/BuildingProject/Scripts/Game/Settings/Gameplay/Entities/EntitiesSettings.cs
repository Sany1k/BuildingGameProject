using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingsSettings", menuName = "Game Settings/Entities/Buildings/New Buildings Settings")]
public class EntitiesSettings : ScriptableObject
{
    [field: SerializeField] public List<BuildingSettings> Buildings { get; private set; }
}
