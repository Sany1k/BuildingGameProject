using System.Linq;
using UnityEngine;

public class CmdPlaceEntityHandler : ICommandHandler<CmdPlaceEntity>
{
    private readonly GameStateProxy gameState;

    public CmdPlaceEntityHandler(GameStateProxy gameState)
    {
        this.gameState = gameState;
    }

    public bool Handle(CmdPlaceEntity command)
    {
        var currentMap = gameState.Maps.FirstOrDefault(m => m.Id == gameState.CurrentMapId.CurrentValue);
        if (currentMap == null)
        {
            Debug.LogError($"Couldn't find MapState for if: {gameState.CurrentMapId.CurrentValue}");
            return false;
        }

        var entityConfigId = command.EntityConfigId;
        var entityType = command.EntityType;
        var entityLevel = command.Level;
        var entityPosition = command.Position;
        var entityId = gameState.CreateEntityId();

        var createdEntityData = entityType switch
        {
            EntityType.Building => EntitiesDataFactory.CreateEntity<BuildingEntityData>(entityType, entityConfigId, entityLevel, entityPosition),
            _ => throw new System.NotImplementedException()
        };
        var createEntity = EntitiesFactory.CreateEntity(createdEntityData);
        
        currentMap.Entities.Add(createEntity);

        return true;
    }
}
