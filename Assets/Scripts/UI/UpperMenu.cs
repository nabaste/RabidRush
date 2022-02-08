using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class UpperMenu : MonoBehaviour
{
    // [SerializeField] private TimeFlowControl tfc;
    // [SerializeField] private KartCounter kc;
    // [SerializeField] private SelectorButtons sb;
    protected VisualElement Root;

    private void OnEnable()
    {
        Root = GetComponent<UIDocument>().rootVisualElement;
        // tfc.enabled = true;
        // kc.enabled = true;
        // sb.enabled = true;
    }
}