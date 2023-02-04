using UnityEngine;


namespace BeatRoot
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private SO_InputEvent Jump;
        [SerializeField] private SO_InputEvent Dash;

   
        [SerializeField] private float JumpForce = 10;
        [SerializeField] private float Speed = 0;
        [SerializeField] private float Gravity = -2;


        public LayerMask GroundLayer;
        private float groundCheckRadius = 0.2f;
        private float groundCheckDistance = 0.1f;

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

            Vector2 velocity = transform.position;

            velocity += new Vector2(Speed * Time.deltaTime, 0);

            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, GroundLayer);

            if (!isGrounded) velocity += new Vector2(0, Gravity * Time.deltaTime);

            transform.position = velocity;
        }

        private void OnDisable()
        {
            Jump.ControllerButtonPressed -= OnJumpPressed;
            Dash.ControllerButtonPressed -= OnDashPressed;
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
            if (!isInJumpField) return;
            isGrounded = Physics2D.OverlapCircle(transform.position, groundCheckRadius, GroundLayer);
            if (isGrounded)
            {

            }
        }

        private void OnDashPressed()
        {
            if(!isInDashField) return;

        }
    }
}