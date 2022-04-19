using MainProject.Scripts.Logic;
using UnityEngine;

namespace MainProject.Scripts.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar hpBar;

        private IHealth _health;

        private void Start()
        {
            var health = GetComponent<IHealth>();
            
            if (health != null)
                Construct(health);
        }

        private void OnDestroy()
        {
            if (_health != null)
                _health.HealthChanged -= UpdateHpBar;
        }

        public void Construct(IHealth health)
        {
            _health = health;

            _health.HealthChanged += UpdateHpBar;
        }

        private void UpdateHpBar() => 
            hpBar.SetValue(_health.Current, _health.Max);
    }
}