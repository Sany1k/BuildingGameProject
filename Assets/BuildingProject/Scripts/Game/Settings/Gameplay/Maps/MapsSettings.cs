using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapsSettings", menuName = "Game Settings/Maps/New Maps Settings")]
public class MapsSettings : ScriptableObject
{
    public List<MapSettings> Maps;
}