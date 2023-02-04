using PlayerActions;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;


namespace BeatRoot
{
    public class InputBridge : MonoBehaviour
    {

        [SerializeField] private SO_InputEvent Jump = null;
        [SerializeField] private SO_InputEvent Dash = null;
        [SerializeField] private SO_InputEvent StartPause = null;

        private BeatRootActions beatRootActions;

        private void Awake()
        {
            beatRootActions = new BeatRootActions();
            RegisterButtonCallbacks();
        }

        private void OnEnable()
        {
            beatRootActions.Enable();
        }

        private void OnDisable()
        {
            beatRootActions.Disable();
        }

        private void RegisterButtonCallbacks()
        {
            beatRootActions.Player.Jump.performed += OnJumpPressed;
            beatRootActions.Player.Jump.canceled += OnJumpCanceled;

            beatRootActions.Player.Dash.performed += OnDashPressed;
            beatRootActions.Player.Dash.canceled += OnDashCanceled;

            beatRootActions.Player.StartPause.performed += OnStartPausePressed;
            beatRootActions.Player.StartPause.canceled += OnStartPauseCanceled;
        }



        private void OnJumpPressed(CallbackContext callbackContext)
        {
            Jump.InvokeButtonPress();
        }

        private void OnJumpCanceled(CallbackContext callbackContext)
        {
            Jump.InvokeButtonRelease();
        }

        private void OnDashPressed(CallbackContext callbackContext)
        {
            Dash.InvokeButtonPress();
        }

        private void OnDashCanceled(CallbackContext callbackContext)
        {
            Dash.InvokeButtonRelease();
        }

        private void OnStartPausePressed(CallbackContext callbackContext)
        {
            StartPause.InvokeButtonPress();
        }

        private void OnStartPauseCanceled(CallbackContext callbackContext)
        {
            StartPause.InvokeButtonRelease();
        }
    }
}