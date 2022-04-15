using UnityEngine;

namespace MainProject.Scripts.Infrastructure.Services.Inputs
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => SimpleInputAxis();
    }
}