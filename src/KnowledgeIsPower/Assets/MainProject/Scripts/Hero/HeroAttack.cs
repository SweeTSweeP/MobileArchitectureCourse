using System;
using CodeBase.Hero;
using MainProject.Scripts.Data;
using MainProject.Scripts.Infrastructure.Services;
using MainProject.Scripts.Infrastructure.Services.Inputs;
using MainProject.Scripts.Infrastructure.Services.PersistentProgress;
using MainProject.Scripts.Logic;
using UnityEngine;

namespace MainProject.Scripts.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private HeroAnimator animator;
        [SerializeField] private CharacterController characterController;

        private IInputService _input;

        private static int _layerMask;
        private Collider[] _hits = new Collider[3];
        private Stats _stats;

        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();

            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Update()
        {
            if (_input.IsAttackButtonUp() && !animator.IsAttacking) animator.PlayAttack();
        }

        public void OnAttack()
        {
            for (var index = 0; index < Hit(); index++)
            {
                _hits[index].transform.parent.GetComponent<IHealth>().TakeDamage(_stats.Damage);
            }
        }

        public void LoadProgress(PlayerProgress playerProgress) => 
            _stats = playerProgress.HeroStats;

        private int Hit() => 
            Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _stats.DamageRadius, _hits, _layerMask);

        private Vector3 StartPoint() =>
            new Vector3(transform.position.x, characterController.center.y / 2, transform.position.z);
    }
}