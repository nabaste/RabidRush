using UnityEngine;
using UnityEngine.UIElements;

public abstract class SideMenu : MonoBehaviour
{
    protected VisualElement Root;
    private void OnEnable()
    {
        Root = GetComponentInParent<UIDocument>().rootVisualElement;
        LevelManager.Instance.RootVisualElement = Root;
    }
}