using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using RabidRush.ScriptableObjects;

public abstract class SideMenu : MonoBehaviour
{
    protected VisualElement Root;

    private void OnEnable()
    {
        Root = GetComponentInParent<UIDocument>().rootVisualElement;
    }
}