using System;
using CodeBase.Enemy;
using MainProject.Scripts.Logic;
using UnityEngine;

namespace MainProject.Scripts.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyHealth: MonoBehaviour, IHealth
    {
        [SerializeField] private EnemyAnimator animator;

        [SerializeField] private float current;
        [SerializeField] private float max;

        public event Action HealthChanged;

        public float Current
        {
            get => current;
            set => current = value;
        }

        public float Max
        {
            get => max;
            set => max = value;
        }

        public void TakeDamage(float damage)
        {
            Current -= damage;
            
            animator.PlayHit();
            
            HealthChanged?.Invoke();
        }
    }
}