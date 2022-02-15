using System;
using RabidRush.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;


namespace UI
{
    public class BarricadeButton : SideMenu
    {
        private Button _barricadeButton;

        [SerializeField] private BarricadeData bd;
        [SerializeField] private Transform specialAttacksContainer;

        private void Start()
        {
            _barricadeButton = Root.Q<Button>("attack-0");

            _barricadeButton.RegisterCallback<ClickEvent>(ev => PlaceBarricade());
        }

        private void PlaceBarricade()
        {
            Instantiate(bd.Prefab, specialAttacksContainer);
        }
    }
}