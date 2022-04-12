using System;
using MainProject.Scripts.CameraLogic;
using MainProject.Scripts.Infrastructure;
using MainProject.Scripts.Services;
using UnityEngine;

namespace MainProject.Scripts.Hero
{
    public class HeroMove : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private float movementSpeed;
        
        private IInputService _inputService;
        private Camera _camera;

        private void Awake()
        {
            _inputService = Game.InputService;
        }

        private void Start()
        {
            _camera = Camera.main;
            CameraFollow();
        }

        private void Update()
        {
            var movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            
            characterController.Move(movementSpeed * movementVector * Time.deltaTime);
        }

        private void CameraFollow() => 
            _camera.GetComponent<CameraFollow>().Follow(gameObject);
    }
}
