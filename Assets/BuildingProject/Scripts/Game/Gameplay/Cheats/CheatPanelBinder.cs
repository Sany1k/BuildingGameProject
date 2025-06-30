using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheatPanelBinder : MonoBehaviour
{
    [SerializeField] private TMP_InputField cheatInputField;
    [SerializeField] private Button btnApply;

    private CheatPanelViewModel viewModel;

    public void Bind(CheatPanelViewModel viewModel)
    {
        this.viewModel = viewModel;
    }

    private void OnEnable()
    {
        btnApply.onClick.AddListener(OnApplyButtonClick);
    }

    private void OnDisable()
    {
        btnApply.onClick.RemoveListener(OnApplyButtonClick);
    }

    public void OnApplyButtonClick()
    {
        viewModel.HandleCheatApplying(cheatInputField.text);
    }
}
