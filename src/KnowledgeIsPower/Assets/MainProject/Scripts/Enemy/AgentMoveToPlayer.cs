using MainProject.Scripts.Infrastructure.Factory;
using MainProject.Scripts.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AI;

namespace MainProject.Scripts.Enemy
{
    public class AgentMoveToPlayer : Follow
    {
        private const float MinimalDistance = 1;
        
        [SerializeField] private NavMeshAgent agent;

        private Transform _heroTransform;
        private IGameFactory _gameFactory;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (_gameFactory.HeroGameObject != null) 
                InitializeHeroTransform();
            else
                _gameFactory.HeroCreated += HeroCreated;
        }

        private void Update()
        {
            if (Initialized() && HeroNotReached()) 
                agent.destination = _heroTransform.position;
        }

        private bool HeroNotReached() => 
            Vector3.Distance(agent.transform.position, _heroTransform.position) >= MinimalDistance;

        private void InitializeHeroTransform() => 
            _heroTransform = _gameFactory.HeroGameObject.transform;

        private bool Initialized() => 
            _heroTransform!= null;

        private void HeroCreated() => 
            InitializeHeroTransform();
    }
}