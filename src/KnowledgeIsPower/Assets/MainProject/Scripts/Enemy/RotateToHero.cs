using MainProject.Scripts.Infrastructure.Factory;
using MainProject.Scripts.Infrastructure.Services;
using UnityEngine;

namespace MainProject.Scripts.Enemy
{
    public class RotateToHero : Follow
    {
        [SerializeField] private float speed;

        private Transform _heroTransform;
        private IGameFactory _gameFactory;
        private Vector3 _positionToLook;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (HeroExists())
                InitializeHeroTransform();
            else
                _gameFactory.HeroCreated += InitializeHeroTransform;
        }

        private void Update()
        {
            if (Initialized())
                RotateTowardsHero();
        }

        private bool Initialized() => 
            _heroTransform != null;

        private void InitializeHeroTransform() => 
            _heroTransform = _gameFactory.HeroGameObject.transform;

        private bool HeroExists() => 
            _gameFactory.HeroGameObject != null;

        private void RotateTowardsHero()
        {
            UpdatePositionToLookAt();

            transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
        }

        private void UpdatePositionToLookAt()
        {
            var positionDiff = _heroTransform.position - transform.position;
            _positionToLook = new Vector3(positionDiff.x, transform.position.y, positionDiff.z);
        }

        private Quaternion SmoothedRotation(Quaternion transformRotation, Vector3 positionToLook) => 
            Quaternion.Lerp(transformRotation, TargetRotation(positionToLook), SpeedFactor());

        private Quaternion TargetRotation(Vector3 positionToLook) => 
            Quaternion.LookRotation(positionToLook);

        private float SpeedFactor() => 
            speed * Time.deltaTime;
    }
}