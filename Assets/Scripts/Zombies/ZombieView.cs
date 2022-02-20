using UnityEngine;

namespace RabidRush.Zombies
{
    public class ZombieView : MonoBehaviour
    {
        [SerializeField] private ZombieModel model;
        [SerializeField] private ZombieController controller;
        private Animator _anim;
        private ParticleSystem _particles;
        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int Alive = Animator.StringToHash("alive");
        private static readonly int Attack = Animator.StringToHash("attack");

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _particles = GetComponent<ParticleSystem>();

            model.OnDeath += OnDeathHandler;
            controller.OnDamageTaken += OnDamageTakenHandler;
            controller.OnChemicalDamageTaken += OnChemicalDamageTakenHandler;
            controller.OnAttack += OnAttackHandler;
        }

        private void OnChemicalDamageTakenHandler()
        {
            //...
        }

        private void OnAttackHandler()
        {
            _anim.SetTrigger(Attack);
        }

        private void OnDeathHandler()
        {
            _anim.SetBool(Alive, false);
        }

        private void OnDamageTakenHandler()
        {
            _anim.SetTrigger(Hit);
            _particles.Play();
            StartCoroutine(model.StopNavMesh(2f));
        }
    }
}