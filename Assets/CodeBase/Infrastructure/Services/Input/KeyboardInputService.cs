using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public class KeyboardInputService : InputService
    {
        public override bool RightTop => UnityEngine.Input.GetKeyDown(KeyCode.E);
        public override bool RightDown => UnityEngine.Input.GetKeyDown(KeyCode.D);
        public override bool LeftTop => UnityEngine.Input.GetKeyDown(KeyCode.Q);
        public override bool LeftDown => UnityEngine.Input.GetKeyDown(KeyCode.A);
       
        public override bool Top => UnityEngine.Input.GetKeyDown(KeyCode.UpArrow);
        public override bool Down => UnityEngine.Input.GetKeyDown(KeyCode.DownArrow);
        public override bool Left => UnityEngine.Input.GetKeyDown(KeyCode.LeftArrow);
        public override bool Right => UnityEngine.Input.GetKeyDown(KeyCode.RightArrow);
    }
}