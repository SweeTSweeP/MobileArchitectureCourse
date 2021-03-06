using UnityEngine;

namespace MainProject.Scripts.Infrastructure.Services.Inputs
{
    public abstract class InputService : IInputService
    {
        private const string Button = "Fire";
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";

        public abstract Vector2 Axis { get; }

        public bool IsAttackButtonUp() => 
            SimpleInput.GetButtonUp(Button);

        protected static Vector2 SimpleInputAxis() => 
            new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
}