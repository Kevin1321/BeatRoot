using UnityEngine;
using UnityEngine.SceneManagement;


namespace BeatRoot
{
    public class TutorialScreen : MonoBehaviour
    {
        [SerializeField] private SO_InputEvent Restart;

        private void OnEnable()
        {
            Restart.ControllerButtonPressed += OnRestartPressed;
        }

        private void OnDisable()
        {
            Restart.ControllerButtonPressed -= OnRestartPressed;
        }

        private void OnRestartPressed()
        {
            SceneManager.LoadScene("Level_1");
        }
    }
}