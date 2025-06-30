using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Game Settings/New Game Settings")]
public class GameSettings : ScriptableObject
{
    public EntitiesSettings BuildingsSettings;
    public MapsSettings MapsSettings;
}