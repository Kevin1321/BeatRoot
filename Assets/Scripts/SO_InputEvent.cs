using UnityEngine;


namespace BeatRoot
{
    [CreateAssetMenu]
    public class SO_InputEvent : ScriptableObject
    {
        public delegate void ControllerButtonPress();
        public event ControllerButtonPress ControllerButtonPressed;

        public delegate void ControllerButtonRelease();
        public event ControllerButtonRelease ControllerButtonReleased;

        public void InvokeButtonPress()
        {
            ControllerButtonPressed?.Invoke();
        }

        public void InvokeButtonRelease()
        {
            ControllerButtonReleased?.Invoke();
        }
    }
}