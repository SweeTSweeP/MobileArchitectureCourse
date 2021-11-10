using UnityEngine;

namespace MainProject.Scripts.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        public float RotationAngleX;
        public float Distance;
        public float OffsetY;
        
        [SerializeField] private Transform _following;

        private void LateUpdate()
        {
            if (_following == null)
                return;

            var rotation = Quaternion.Euler(RotationAngleX, 0, 0);
            var position = rotation * new Vector3(0, 0, -Distance) + FollowingPointPosition();

            transform.rotation = rotation;
            transform.position = position;
        }

        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = _following.position;
            followingPosition.y += OffsetY;
            
            return followingPosition;
        }

        public void Follow(GameObject following) => _following = following.transform;
    }
}