using System;
using UnityEngine;


public class SelectionHandler : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private LayerMask _selectionMask;
    [SerializeField] private MiddleMenu middleMenu;

    

    private void Awake()
    {
        _selectionMask = LayerMask.GetMask("Obstacle") | 
                         LayerMask.GetMask("Tower") | 
                         LayerMask.GetMask("Zombie");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            HandleClick();
        }
    }

    private void HandleClick()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, Mathf.Infinity, _selectionMask)) return;
        // var item = hit.transform.gameObject.GetComponent<IInspectable>();
        if(!hit.transform.gameObject.TryGetComponent(out IInspectable item)) return;
        middleMenu.BuildInspectorMenu(item);
    }
}