using System;
using DG.Tweening;
using UnityEngine;


namespace BeatRoot
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private SO_InputEvent Jump;
        [SerializeField] private SO_InputEvent Dash;

        [SerializeField] private SpriteRenderer SR;
        [SerializeField] private Sprite BaseSprite;
        [SerializeField] private Sprite JumpSprite;
        [SerializeField] private Sprite DashSprite;
   
        [SerializeField] private float JumpForce = 10;
        [SerializeField] private float Speed = 0;
        [SerializeField] private float Gravity = -3;
        [SerializeField] private float DashDuration;
        [SerializeField] private float DashDistance;
        [SerializeField] private AudioSource MusicPlayer;


        public static PlayerController Instance;
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
        private InteractableField interactableFieldInRange;


        private Vector2 newPosition;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }


        private void OnEnable()
        {
            Jump.ControllerButtonPressed += OnJumpPressed;
            Dash.ControllerButtonPressed += OnDashPressed;
        }

        private void Start()
        {
            MusicPlayer.Play();
        }

        private void Update()
        {
            if(!MusicPlayer.isPlaying) return;
            
            if (!isAlive) return;
            
            if (isDashing) return;
            
            newPosition = transform.position;


            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, GroundLayer);
            
            verticalSpeed = isGrounded ? Mathf.Max(verticalSpeed, 0) : verticalSpeed + Gravity * Time.deltaTime;
            if (verticalSpeed == 0 && SR.sprite == JumpSprite) SR.sprite = BaseSprite; 
            newPosition.y += verticalSpeed * Time.deltaTime;
            newPosition.x += dashSpeed > 0 ? dashSpeed : Speed * Time.deltaTime;
            
            transform.position = newPosition;
        }

        private void OnDisable()
        {
            Jump.ControllerButtonPressed -= OnJumpPressed;
            Dash.ControllerButtonPressed -= OnDashPressed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var jumpInstrument = collision.GetComponent<JumpInstrument>();
            var dashInstrument = collision.GetComponent<DashInstrument>();
            if (jumpInstrument != null)
            {
                isInJumpField = true;
                interactableFieldInRange = jumpInstrument;
            }

            if (dashInstrument != null)
            {
                isInDashField = true;
                interactableFieldInRange = dashInstrument;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<JumpInstrument>() != null) isInJumpField = false;
            if (collision.GetComponent<DashInstrument>() != null) isInDashField = false;
        }

        private void OnJumpPressed()
        {
            if (!isInJumpField) return;
            SR.sprite = JumpSprite;
            interactableFieldInRange.Use();
            verticalSpeed = JumpForce;
        }

        private void OnDashPressed()
        {
            if(!isInDashField) return;
            SR.sprite = DashSprite;
            interactableFieldInRange.Use();
            isDashing = true;
            transform.DOMoveX(transform.position.x + DashDistance, DashDuration).SetEase(Ease.InBack).OnComplete(SetIsDashingToFalse);
        }

        private void SetIsDashingToFalse()
        {
            SR.sprite = BaseSprite;
            isDashing = false;
        }

        public float GetXPosition()
        {
            return transform.position.x;
        }
    }
}