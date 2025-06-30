using UnityEngine;

public class UIRootView : MonoBehaviour
{
    [SerializeField] private GameObject loadingSceen;
    [SerializeField] private Transform uiSceneContainer;

    private void Awake()
    {
        HideLoadingScreen();
    }

    public void ShowLoadingScreen()
    {
        loadingSceen.SetActive(true);
    }

    public void HideLoadingScreen()
    {
        loadingSceen.SetActive(false);
    }

    public void AttachSceneUI(GameObject sceneUI)
    {
        ClearSceneUI();

        sceneUI.transform.SetParent(uiSceneContainer, false);
    }

    private void ClearSceneUI()
    {
        var childCount = uiSceneContainer.childCount;
        for (var i = 0; i < childCount; i++)
        {
            Destroy(uiSceneContainer.GetChild(i).gameObject);
        }
    }
}
