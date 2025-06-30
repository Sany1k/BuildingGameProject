using UnityEngine;

public class UIGameplayRootBinder : UIRootBinder
{
    [SerializeField] private CheatPanelBinder binderCheatsPanel;

    protected override void OnBind(UIRootViewModel rootViewModel)
    {
        base.OnBind(rootViewModel);

        var viewModel = rootViewModel as UIGameplayRootViewModel;
        binderCheatsPanel.Bind(viewModel.cheatPanelViewModel);
    }
}
