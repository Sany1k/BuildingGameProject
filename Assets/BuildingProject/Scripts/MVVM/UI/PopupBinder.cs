using UnityEngine;
using UnityEngine.UI;

public abstract class PopupBinder<T> : WindowBinder<T> where T : WindowViewModel
{
    [SerializeField] private Button btnClose;
    [SerializeField] private Button btnCloseAlt;

    protected virtual void Start()
    {
        btnClose?.onClick?.AddListener(OnCloseButtonClick);
        btnCloseAlt?.onClick?.AddListener(OnCloseButtonClick);
    }

    protected virtual void OnDestroy()
    {
        btnClose?.onClick?.RemoveListener(OnCloseButtonClick);
        btnCloseAlt?.onClick?.RemoveListener(OnCloseButtonClick);
    }

    protected virtual void OnCloseButtonClick()
    {
        ViewModel.RequestClose();
    }
}