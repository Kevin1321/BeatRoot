using UnityEngine;


namespace BeatRoot
{
    public class InputDetection : MonoBehaviour
    {
        [SerializeField] private GameObject ControllerTutorial;
        [SerializeField] private GameObject KeyboardTutorial;
        private bool isControllerAttached;
     
        
        private void Awake()
        {
            isControllerAttached = Input.GetJoystickNames().Length > 0;
           
            if(isControllerAttached) ShowControllerTutorial();
            else ShowKeyboardTutorial();
        }
        
        private void ShowControllerTutorial()
        {
            ControllerTutorial.SetActive(true);
        }

        private void ShowKeyboardTutorial()
        {
            KeyboardTutorial.SetActive(true);
        }
    }
}