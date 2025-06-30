using UnityEngine;

public class BuildingBinder : MonoBehaviour
{
    public void Bind(BuildingViewModel viewModel)
    {
        Vector2Int position2D = viewModel.Position.CurrentValue;
        transform.position = new Vector3(position2D.x, 0, position2D.y);
    }
}
