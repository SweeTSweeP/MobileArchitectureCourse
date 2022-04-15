using UnityEngine;

namespace MainProject.Scripts.Infrastructure.Services.Inputs
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        bool IsAttackButtonUp();
    }
}