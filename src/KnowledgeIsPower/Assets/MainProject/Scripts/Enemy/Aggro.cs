using System.Collections;
using UnityEngine;

namespace MainProject.Scripts.Enemy
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver triggerObserver;
        [SerializeField] private AgentMoveToPlayer follow;

        [SerializeField] private float cooldown;
        
        private Coroutine _aggroCoroutine;
        private bool _hasAggroTarget;

        private void Start()
        {
            triggerObserver.TriggerEnter += TriggerEnter;
            triggerObserver.TriggerExit += TriggerExit;

            SwitchFollowOff();
        }

        private void TriggerEnter(Collider obj)
        {
            if (!_hasAggroTarget)
            {
                _hasAggroTarget = true;

                StopAggroCoroutine();

                SwitchFollowOn();
            }
        }

        private void TriggerExit(Collider obj)
        {
            if (_hasAggroTarget)
            {
                _hasAggroTarget = false;
                
                _aggroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
            }
        }

        private void StopAggroCoroutine()
        {
            if (_aggroCoroutine != null)
            {
                StopCoroutine(_aggroCoroutine);
                _aggroCoroutine = null;
            }
        }

        private IEnumerator SwitchFollowOffAfterCooldown()
        {
            yield return new WaitForSeconds(cooldown);
            
            SwitchFollowOff();
        }

        private void SwitchFollowOff() => 
            follow.enabled = false;

        private void SwitchFollowOn() => 
            follow.enabled = true;
    }
}