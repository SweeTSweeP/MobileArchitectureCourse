using System;
using UnityEngine;

namespace MainProject.Scripts.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float rotationAngleX;
        [SerializeField] private float distance;
        [SerializeField] private float offsetY;
        
        [SerializeField] private Transform _following;

        private void LateUpdate()
        {
            if (_following == null)
                return;

            var rotation = Quaternion.Euler(rotationAngleX, 0, 0);
            var position = rotation * new Vector3(0, 0, -distance) + FollowingPointPosition();

            transform.rotation = rotation;
            transform.position = position;
        }

        public void Follow(GameObject following) => 
            _following = following.transform;

        private Vector3 FollowingPointPosition()
        {
            var followingPosition = _following.position;
            followingPosition.y += offsetY;
            
            return followingPosition;
        }
    }
}