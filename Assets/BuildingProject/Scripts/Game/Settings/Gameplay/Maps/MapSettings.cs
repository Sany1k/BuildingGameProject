﻿using UnityEngine;

[CreateAssetMenu(fileName = "MapSettings", menuName = "Game Settings/Maps/New Map Settings")]
public class MapSettings : ScriptableObject
{
    public int MapId;
    public MapInitialStateSettings InitialStateSettings;
}
