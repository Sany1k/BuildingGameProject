using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CmdCreateMapHandler : ICommandHandler<CmdCreateMap>
{
    private readonly GameStateProxy gameState;
    private readonly GameSettings gameSettings;

    public CmdCreateMapHandler(GameStateProxy gameState, GameSettings gameSettings)
    {
        this.gameState = gameState;
        this.gameSettings = gameSettings;
    }

    public bool Handle(CmdCreateMap command)
    {
        var isMapAlreadyExisted = gameState.Maps.Any(m => m.Id == command.MapId);

        if (isMapAlreadyExisted)
        {
            Debug.LogError($"Map with Id = {command.MapId} already exists");
            return false;
        }

        var newMapSettings = gameSettings.MapsSettings.Maps.First(m => m.MapId == command.MapId);
        var newMapInitialStateSettings = newMapSettings.InitialStateSettings;

        var initialEntities = new List<EntityData>();
        foreach (var entitySettings in newMapInitialStateSettings.Entities)
        {
            var initialEntityData = EntitiesDataFactory.CreateEntity(entitySettings);
            initialEntityData.UniqueId = gameState.CreateEntityId();


            initialEntities.Add(initialEntityData);
        }

        var newMapState = new MapData
        {
            Id = command.MapId,
            Entities = initialEntities
        };

        var newMapStateProxy = new Map(newMapState);
        gameState.Maps.Add(newMapStateProxy);

        return true;
    }
}
