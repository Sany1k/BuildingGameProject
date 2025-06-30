using UnityEngine;
using UnityEngine.UI;

public class ScreenGameplayBinder : WindowBinder<ScreenGameplayViewModel>
{
    [SerializeField] private Button btnPopupA;
    [SerializeField] private Button btnPopupB;
    [SerializeField] private Button btnGoToMenu;

    private void OnEnable()
    {
        btnPopupA.onClick.AddListener(OnPopupAButtonClicked);
        btnPopupB.onClick.AddListener(OnPopupBButtonClicked);
        btnGoToMenu.onClick.AddListener(OnGoToMainMenuButtonClicked);
    }

    private void OnDisable()
    {
        btnPopupA.onClick.RemoveListener(OnPopupAButtonClicked);
        btnPopupB.onClick.RemoveListener(OnPopupBButtonClicked);
        btnGoToMenu.onClick.RemoveListener(OnGoToMainMenuButtonClicked);
    }

    private void OnPopupAButtonClicked()
    {
        ViewModel.RequestOpenPopupA();
    }

    private void OnPopupBButtonClicked()
    {
        ViewModel.RequestOpenPopupB();
    }

    private void OnGoToMainMenuButtonClicked()
    {
        ViewModel.RequestGoToMainMenu();
    }
}
