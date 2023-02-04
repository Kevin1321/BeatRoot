using DG.Tweening;
using UnityEditor.Timeline;
using UnityEngine;


namespace BeatRoot
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private SO_InputEvent Jump;
        [SerializeField] private SO_InputEvent Dash;

   
        [SerializeField] private float JumpForce = 10;
        [SerializeField] private float Speed = 0;
        [SerializeField] private float Gravity = -3;
        [SerializeField] private float DashDuration;
        [SerializeField] private float DashDistance;

        
        public LayerMask GroundLayer;
        private float groundCheckRadius = 0.2f;
        private float groundCheckDistance = 0.01f;

        public bool isAlive = true;

        private float verticalSpeed;
        private bool isInJumpField = false;
        private bool isInDashField = false;
        private bool isGrounded = true;
        private float dashSpeed;
        private bool isDashing;


        private Vector2 newPosition;


        private void OnEnable()
        {
            Jump.ControllerButtonPressed += OnJumpPressed;
            Dash.ControllerButtonPressed += OnDashPressed;
        }

        private void Update()
        {
            if (!isAlive) return;
            
            newPosition = transform.position;

            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, GroundLayer);
            
            verticalSpeed = isGrounded ? Mathf.Max(verticalSpeed, 0) : verticalSpeed + Gravity * Time.deltaTime;
            
            if (!isDashing)
            {
                newPosition.y += verticalSpeed * Time.deltaTime;
                newPosition.x += dashSpeed > 0 ? dashSpeed : Speed * Time.deltaTime;
                
                transform.position = newPosition;
            }
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
            if (isInJumpField)
            {
                verticalSpeed += JumpForce;
            }
        }

        private void OnDashPressed()
        {
            if(!isInDashField) return;

            isDashing = true;
            transform.DOMoveX(transform.position.x + DashDistance, DashDuration).SetEase(Ease.InBack).OnComplete(SetIsDashingToFalse);
        }

        private void SetIsDashingToFalse()
        {
            isDashing = false;
        }
    }
}