using System.Collections.Generic;
using UnityEngine;

public class WindowsContainer : MonoBehaviour
{
    [SerializeField] private Transform screensContainer;
    [SerializeField] private Transform popupsContainer;

    private readonly Dictionary<WindowViewModel, IWindowBinder> openedPopupBinders = new();
    private IWindowBinder openedScreenBinder;

    public void OpenPopup(WindowViewModel viewModel)
    {
        var prefabPath = GetPrefabPath(viewModel);
        var prefab = Resources.Load<GameObject>(prefabPath);
        var createdPopup = Instantiate(prefab, popupsContainer);
        var binder = createdPopup.GetComponent<IWindowBinder>();

        binder.Bind(viewModel);
        openedPopupBinders.Add(viewModel, binder);
    }

    public void ClosePopup(WindowViewModel viewModel)
    {
        var binder = openedPopupBinders[viewModel];
        binder?.Close();
        openedPopupBinders?.Remove(viewModel);
    }

    public void OpenScreen(WindowViewModel viewModel)
    {
        if (viewModel == null) return;

        openedScreenBinder?.Close();
        var prefabPath = GetPrefabPath(viewModel);
        var prefab = Resources.Load<GameObject>(prefabPath);
        var createdScreen = Instantiate(prefab, screensContainer);
        var binder = createdScreen.GetComponent<IWindowBinder>();

        binder.Bind(viewModel);
        openedScreenBinder = binder;
    }

    private string GetPrefabPath(WindowViewModel viewModel)
    {
        return $"Prefabs/UI/{viewModel.Id}";
    }
}
