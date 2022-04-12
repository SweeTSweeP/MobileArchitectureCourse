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

        private void Awake() => _inputService = Game.InputService;

        private void Update()
        {
            var movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = Camera.main.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            
            characterController.Move(movementSpeed * movementVector * Time.deltaTime);
        }
    }
}
