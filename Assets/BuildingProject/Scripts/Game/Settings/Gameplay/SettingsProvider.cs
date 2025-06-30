using System.Threading.Tasks;
using UnityEngine;

public class SettingsProvider : ISettingsProvider
{
    public GameSettings GameSettings => gameSettings;

    public ApplicationSettings ApplicationSettings { get; }

    private GameSettings gameSettings;

    public SettingsProvider()
    {
        ApplicationSettings = Resources.Load<ApplicationSettings>("ApplicationSettings");
    }

    public Task<GameSettings> LoadGameSettings()
    {
        gameSettings = Resources.Load<GameSettings>("GameSettings");

        return Task.FromResult(gameSettings);
    }
}
