using UnityEngine;

namespace MainProject.Scripts.Services
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis
        {
            get
            {
                var axis = SimpleInputAxis();

                if (axis == Vector2.zero)
                    axis = UnityAxis();
                    
                return axis;
            }
        }

        private static Vector2 UnityAxis() => 
            new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));
    }
}