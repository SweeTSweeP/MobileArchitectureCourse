using MainProject.Scripts.Infrastructure.Services;
using UnityEngine;

namespace MainProject.Scripts.Services
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        bool IsAttackButtonUp();
    }
}