using System;
using UnityEngine;

namespace MainProject.Scripts.Enemy
{
    [RequireComponent(typeof(Attack))]
    public class CheckAttackRange : MonoBehaviour
    {
        [SerializeField] private Attack attack;
        [SerializeField] private TriggerObserver triggerObserver;

        private void Start()
        {
            triggerObserver.TriggerEnter += TriggerEnter;
            triggerObserver.TriggerExit += TriggerExit;
            
            attack.DisableAttack();
        }

        private void TriggerExit(Collider obj)
        {
            attack.DisableAttack();
        }

        private void TriggerEnter(Collider obj)
        {
            attack.EnableAttack();
        }
    }
}