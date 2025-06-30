using System.Linq;

public class CmdResourcesAddHandler : ICommandHandler<CmdResourcesAdd>
{
    private readonly GameStateProxy gameState;

    public CmdResourcesAddHandler(GameStateProxy gameState)
    {
        this.gameState = gameState;
    }

    public bool Handle(CmdResourcesAdd command)
    {
        var requiredResourceType = command.ResourceType;
        var requiredResource = gameState.Resources.FirstOrDefault(r => r.ResourceType == requiredResourceType);

        if (requiredResource == null)
        {
            requiredResource = CreateNewResource(requiredResourceType);
        }

        requiredResource.Amount.Value += command.Amount;

        return true;
    }

    private Resource CreateNewResource(ResourceType resourceType)
    {
        var newResourceData = new ResourceData
        {
            ResourceType = resourceType,
            Amount = 0
        };

        var newResource = new Resource(newResourceData);
        gameState.Resources.Add(newResource);

        return newResource;
    }
}
