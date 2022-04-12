using UnityEngine;

namespace MainProject.Scripts.Services
{
    public interface IInputService
    {
        Vector2 Axis { get; }
        bool IsAttackButtonUp();
    }
}