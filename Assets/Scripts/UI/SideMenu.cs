using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public abstract class SideMenu : MonoBehaviour
{
    protected VisualElement Root;

    private void OnEnable()
    {
        Root = GetComponentInParent<UIDocument>().rootVisualElement;
    }
}