using UnityEngine;

namespace MainProject.Scripts.Services
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => SimpleInputAxis();
    }
}