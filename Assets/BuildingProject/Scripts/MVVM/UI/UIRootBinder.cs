using ObservableCollections;
using R3;
using UnityEngine;

public class UIRootBinder : MonoBehaviour
{
    [SerializeField] private WindowsContainer windowsContainer;

    private readonly CompositeDisposable subscriptions = new();

    public void Bind(UIRootViewModel viewModel)
    {
        subscriptions.Add(viewModel.OpenedScreen.Subscribe(newScreenViewModel =>
        {
            windowsContainer.OpenScreen(newScreenViewModel);
        }));

        foreach (var openedPopup in viewModel.OpenedPopups)
        {
            windowsContainer.OpenPopup(openedPopup);
        }

        subscriptions.Add(viewModel.OpenedPopups.ObserveAdd().Subscribe(e =>
        {
            windowsContainer.OpenPopup(e.Value);
        }));
        subscriptions.Add(viewModel.OpenedPopups.ObserveRemove().Subscribe(e => 
        {
            windowsContainer.ClosePopup(e.Value);
        }));

        OnBind(viewModel);
    }

    protected virtual void OnBind(UIRootViewModel viewModel) { }

    private void OnDestroy()
    {
        subscriptions.Dispose();
    }
}
