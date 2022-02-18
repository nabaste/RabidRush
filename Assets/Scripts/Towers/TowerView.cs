using UnityEngine;

namespace RabidRush.Towers
{
    public class TowerView : MonoBehaviour
    {
        [SerializeField] private TowerController controller;
        [SerializeField] private TowerModel model;
        private Animator _anim;
        private static readonly int Attack = Animator.StringToHash("attack");

        private void Awake()
        {
            _anim = GetComponent<Animator>();

            controller.OnShoot += OnShootHandler;
            model.OnUpgrade += OnUpgradeHandler;
        }

        private void OnShootHandler()
        {
            _anim.SetTrigger(Attack);
        }

        private void OnUpgradeHandler()
        {
            //Make the animator run an animation showing an upgrade
        }
    }
}