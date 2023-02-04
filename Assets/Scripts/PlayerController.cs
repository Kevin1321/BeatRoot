using UnityEngine;


namespace BeatRoot
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float Speed = 1.0f;
        [SerializeField] private float Gravity = 2.0f;
        [SerializeField] private SO_InputEvent Jump;
        [SerializeField] private SO_InputEvent Dash;

        public bool isAlive = true;

        private bool isInJumpField = false;
        private bool isInDashField = false;
        private bool isGrounded = false;

        private void OnEnable()
        {
            Jump.ControllerButtonPressed += OnJumpPressed;
            Dash.ControllerButtonPressed += OnDashPressed;
        }

        private void Update()
        {
            if (!isAlive) return;

            Vector2 postion = transform.position;
            postion += (new Vector2(Speed, 0) * Time.deltaTime);

            transform.position = postion;
        }

        private void OnDisable()
        {
            Dash.ControllerButtonPressed -= OnJumpPressed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.GetComponent<JumpInstrument>()!= null) isInJumpField = true;
            if (collision.GetComponent<DashInstrument>() != null) isInDashField = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<JumpInstrument>() != null) isInJumpField = false;
            if (collision.GetComponent<DashInstrument>() != null) isInDashField = false;
        }

        private void OnJumpPressed()
        {
            Debug.Log("Pressed");
        }

        private void OnDashPressed()
        {
            Debug.Log("Pressed");
        }
    }
}