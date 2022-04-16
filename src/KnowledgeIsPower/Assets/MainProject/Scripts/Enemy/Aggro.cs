﻿using System;
using UnityEngine;

namespace MainProject.Scripts.Enemy
{
    [RequireComponent(typeof(Collider))]
    public class Aggro : MonoBehaviour
    {
        public event Action<Collider> TriggerEnter;
        public event Action<Collider> TriggerExit;
        
        private void OnTriggerEnter(Collider other) => 
            TriggerEnter?.Invoke(other);

        private void OnTriggerExit(Collider other) => 
            TriggerExit?.Invoke(other);
    }
}