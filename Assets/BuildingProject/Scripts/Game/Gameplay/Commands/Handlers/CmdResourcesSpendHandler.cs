using System.Linq;
using UnityEngine;

public class CmdResourcesSpendHandler : ICommandHandler<CmdResourcesSpend>
{
    private readonly GameStateProxy gameState;

    public CmdResourcesSpendHandler(GameStateProxy gameState)
    {
        this.gameState = gameState;
    }

    public bool Handle(CmdResourcesSpend command)
    {
        var requiredResourceType = command.ResourceType;
        var requiredResource = gameState.Resources.FirstOrDefault(r => r.ResourceType == requiredResourceType);

        if (requiredResource == null)
        {
            Debug.LogError("Trying to spend not existed resource.");
            return false;
        }

        if (requiredResource.Amount.Value < command.Amount)
        {
            Debug.LogError($"Trying to spend more resource than existed ({requiredResourceType}). Exists: {requiredResource.Amount.Value}.");
            return false;
        }

        requiredResource.Amount.Value -= command.Amount;

        return true;
    }
}
