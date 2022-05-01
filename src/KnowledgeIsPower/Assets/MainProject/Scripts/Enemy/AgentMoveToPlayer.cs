using MainProject.Scripts.Infrastructure.Factory;
using UnityEngine;
using UnityEngine.AI;

namespace MainProject.Scripts.Enemy
{
    public class AgentMoveToPlayer : Follow
    {
        [SerializeField] private NavMeshAgent agent;

        private Transform _heroTransform;
        private IGameFactory _gameFactory;

        public void Construct(Transform heroTransform) =>
            _heroTransform = heroTransform;

        private void Update() => 
            SetDestinationForAgent();

        private void SetDestinationForAgent()
        {
            if (_heroTransform)
                agent.destination = _heroTransform.position;
        }
    }
}