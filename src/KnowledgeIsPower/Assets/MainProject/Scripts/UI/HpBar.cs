﻿using UnityEngine;
using UnityEngine.UI;

namespace MainProject.Scripts.UI
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image imageCurrent;

        public void SetValue(float current, float max) =>
            imageCurrent.fillAmount = current / max;
    }
}