using System.Collections;
using System.Linq;
using CodeBase.Enemy;
using MainProject.Scripts.Infrastructure.Factory;
using MainProject.Scripts.Logic;
using UnityEngine;

namespace MainProject.Scripts.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator animator;

        [SerializeField] private float attackCooldown = 3f;
        [SerializeField] private float cleavage = 0.5f;
        [SerializeField] private float effectiveDistance = 0.5f;
        [SerializeField] private float damage = 10f;

        public float EffectiveDistance
        {
            get => effectiveDistance;
            set => effectiveDistance = value;
        }

        public float Cleavage
        {
            get => cleavage;
            set => cleavage = value;
        }

        public float Damage
        {
            get => damage;
            set => damage = value;
        }

        private IGameFactory _gameFactory;
        private Transform _heroTransform;
        private float _attackCooldown;
        [SerializeField] private bool _isAttacking;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        [SerializeField] private bool _attackIsActive;

        public void Construct(Transform heroTransform) => 
            _heroTransform = heroTransform;

        private void Awake() => 
            _layerMask = 1 << LayerMask.NameToLayer("Player");

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack()) 
                StartAttack();
        }

        private void OnAttack()
        {
            if (!Hit(out var hit)) return;
            
            PhysicsDebug.DrawDebug(StartPoint(), cleavage, 1);
            hit.transform.GetComponent<IHealth>().TakeDamage(damage);

            if (!animator.gameObject.name.Contains("Lich")) StartCoroutine(InvalidOnAttackEnded());
        }

        private IEnumerator InvalidOnAttackEnded()
        {
            yield return new WaitForSeconds(1 + 1 / 65);
            
            OnAttackEnded();
        }

        private void OnAttackEnded()
        {
            _attackCooldown = attackCooldown;
            _isAttacking = false;
        }

        public void EnableAttack() => 
            _attackIsActive = true;

        public void DisableAttack() => 
            _attackIsActive = false;

        private bool Hit(out Collider hit)
        {
            var hitsCount = Physics.OverlapSphereNonAlloc(StartPoint(), cleavage, _hits, _layerMask);

            hit = _hits.FirstOrDefault();
            
            return hitsCount > 0;
        }

        private Vector3 StartPoint() => 
            new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward * effectiveDistance;

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            animator.PlayAttack();

            _isAttacking = true;
        }

        private bool CanAttack() => 
            _attackIsActive && !_isAttacking && CooldownIsUp();

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _attackCooldown -= Time.deltaTime;
        }

        private bool CooldownIsUp()
        {
            return _attackCooldown <= 0;
        }
    }
}