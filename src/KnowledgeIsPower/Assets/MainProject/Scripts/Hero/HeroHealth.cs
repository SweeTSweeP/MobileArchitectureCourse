using System;
using CodeBase.Hero;
using MainProject.Scripts.Data;
using MainProject.Scripts.Infrastructure.Services.PersistentProgress;
using MainProject.Scripts.Logic;
using UnityEngine;

namespace MainProject.Scripts.Hero
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        [SerializeField] private HeroAnimator animator;

        public event Action HealthChanged; 
        
        private State _state;

        public float Max
        {
            get => _state.MaxHP;
            set => _state.MaxHP = value;
        }

        public float Current
        {
            get => _state.CurrentHP;
            set
            {
                if (_state.CurrentHP != value)
                {
                    _state.CurrentHP = value;
                    HealthChanged?.Invoke();
                }
            }
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            _state = playerProgress.HeroState;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            playerProgress.HeroState.CurrentHP = Current;
            playerProgress.HeroState.MaxHP = Max;
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0) return;

            Current -= damage;
            animator.PlayHit();
        }
    }
}