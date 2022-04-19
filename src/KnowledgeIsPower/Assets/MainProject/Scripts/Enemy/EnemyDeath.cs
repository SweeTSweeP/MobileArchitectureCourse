using System;
using System.Collections;
using CodeBase.Enemy;
using UnityEngine;

namespace MainProject.Scripts.Enemy
{
    [RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth health;
        [SerializeField] private EnemyAnimator animator;

        [SerializeField] private GameObject deathFx;

        public event Action Happened;

        private void Start() => 
            health.HealthChanged += HealthChanged;

        private void OnDestroy() => 
            health.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (health.Current <= 0)
                Die();
        }

        private void Die()
        {
            health.HealthChanged -= HealthChanged;
            
            animator.PlayDeath();

            SpawnDeathFx();
            StartCoroutine(DestroyTimer());
            
            Happened?.Invoke();
        }

        private void SpawnDeathFx() => 
            Instantiate(deathFx, transform.position, Quaternion.identity);

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }
    }
}