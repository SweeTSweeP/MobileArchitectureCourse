using UnityEngine;

namespace MainProject.Scripts.Enemy
{
    public class RotateToHero : Follow
    {
        [SerializeField] private float speed;

        private Transform _heroTransform;
        private Vector3 _positionToLook;
        
        public void Construct(Transform heroTransform) => 
            _heroTransform = heroTransform;

        private void Update()
        {
            if (Initialized())
                RotateTowardsHero();
        }

        private bool Initialized() => 
            _heroTransform != null;

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