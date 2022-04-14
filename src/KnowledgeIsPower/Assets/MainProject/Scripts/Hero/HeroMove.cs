using System;
using MainProject.Scripts.CameraLogic;
using MainProject.Scripts.Infrastructure;
using MainProject.Scripts.Infrastructure.Services;
using MainProject.Scripts.Services;
using UnityEngine;

namespace MainProject.Scripts.Hero
{
    public class HeroMove : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 10f;

        private CharacterController _characterController;
        private IInputService _inputService;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();

            _characterController = GetComponent<CharacterController>();
        }

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
            
            _characterController.Move(movementSpeed * movementVector * Time.deltaTime);
        }
    }
}
