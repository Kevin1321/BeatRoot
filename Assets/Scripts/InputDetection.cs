using System.Security.Cryptography;
using UnityEngine;


namespace BeatRoot
{
    public class InputDetection : MonoBehaviour
    {
        public static InputDetection Instance;
        public bool IsControllerAttached { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
            
            DontDestroyOnLoad(gameObject);
            IsControllerAttached = Input.GetJoystickNames().Length > 0;
            Debug.Log(Input.GetJoystickNames().Length);
            var joystickNames = Input.GetJoystickNames();
            for (int i = 0; i < joystickNames.Length; i++)
            {
                Debug.Log($"Joystick: {joystickNames[i]}");
            }
            Debug.Log(IsControllerAttached ? "Controller is attached" : "Keyboard is attached");
        }
    }
}