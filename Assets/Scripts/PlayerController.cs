using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BeatRoot
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private SO_InputEvent Jump;
        [SerializeField] private SO_InputEvent Dash;
        [SerializeField] private SO_InputEvent Restart;

        [SerializeField] private Animator BeatRootAnimator;

        [SerializeField] private float JumpForce = 10;
        [SerializeField] private float Speed = 0;
        [SerializeField] private float Gravity = -3;
        [SerializeField] private float DashDuration;
        [SerializeField] private float DashDistance;
        [SerializeField] private AudioSource MusicPlayer;
        [SerializeField] private AudioSource OverlayMusicPlayer;
        [SerializeField] private AudioClip JumpSound;
        [SerializeField] private AudioClip DashSound;
        [SerializeField] private GameObject FinishScreen;
        [SerializeField] private LooseScreen LooseScreen;

        [SerializeField] private ZombieHorde Zombies;

        public static PlayerController Instance;
        public static Action OnPlayerFalling;
        public LayerMask GroundLayer;
        private float groundCheckDistance = 0.01f;

        public bool isAlive = true;

        private float verticalSpeed;
        private bool isInJumpField = false;
        private bool isInDashField = false;
        private bool finished = false;
        private bool isGrounded = true;
        private float dashSpeed;
        private bool isDashing;
        private WaitForEndOfFrame waitForFrame;
        private InteractableField interactableFieldInRange;
        private AudioSource playerSounds;


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
            Restart.ControllerButtonPressed += OnRestartPressed;
        }

        private void Start()
        {
            MusicPlayer.Play();
            OverlayMusicPlayer.Play();
            playerSounds = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (!MusicPlayer.isPlaying) return;

            if (!isAlive) return;

            if (isDashing) return;

            newPosition = transform.position;


            if (!finished)
            {
                isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, GroundLayer);

                verticalSpeed = isGrounded ? Mathf.Max(verticalSpeed, 0) : verticalSpeed + Gravity * Time.deltaTime;
                if (verticalSpeed == 0 && BeatRootAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "A_Jump_Down")
                    BeatRootAnimator.Play("A_Walk");
                if (verticalSpeed < 0 && BeatRootAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "A_Jump_Up")
                    BeatRootAnimator.Play("A_Jump_Down");
                newPosition.y += verticalSpeed * Time.deltaTime;
            }

            newPosition.x += dashSpeed > 0 ? dashSpeed : Speed * Time.deltaTime;

            transform.position = newPosition;
        }

        private void OnDisable()
        {
            Jump.ControllerButtonPressed -= OnJumpPressed;
            Dash.ControllerButtonPressed -= OnDashPressed;
            Restart.ControllerButtonPressed -= OnRestartPressed;
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

            if (collision.GetComponent<Goal>() != null) Finish();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<JumpInstrument>() != null) isInJumpField = false;
            if (collision.GetComponent<DashInstrument>() != null) isInDashField = false;
        }

        private void OnJumpPressed()
        {
            if (finished || !isAlive) return;

            if (!isInJumpField)
            {
                Zombies.GetCloser();
                return;
            }

            playerSounds.clip = JumpSound;
            playerSounds.Play();
            BeatRootAnimator.Play("A_Jump_Up");
            interactableFieldInRange.Use();
            verticalSpeed = JumpForce;
        }

        private void OnDashPressed()
        {
            if (finished || !isAlive) return;

            if (!isInDashField)
            {
                Zombies.GetCloser();
                return;
            }

            playerSounds.clip = DashSound;
            playerSounds.Play();
            BeatRootAnimator.Play("A_Dash");
            interactableFieldInRange.Use();
            isDashing = true;
            transform.DOMoveX(transform.position.x + DashDistance, DashDuration).SetEase(Ease.InQuint)
                .OnComplete(SetIsDashingToFalse);
        }

        private void OnRestartPressed()
        {
            if (isAlive) return;
            SceneManager.LoadScene("Level_1");
        }

        private void SetIsDashingToFalse()
        {
            BeatRootAnimator.Play("A_Walk");
            isDashing = false;
        }

        public float GetXPosition()
        {
            return transform.position.x;
        }

        public void GetEaten()
        {
            Die();
        }

        public void Fall()
        {
            Die();
            transform.Rotate(new Vector3(0, 0, 90));
            OnPlayerFalling?.Invoke();
            StartCoroutine(Falling());
        }

        private IEnumerator Falling()
        {
            var fallSpeed = 1.5f;
            var fallVelocity = 1f;
            var fallVector = new Vector3(0, 1, 0);
            var timer = 2f;
            while (timer > 0)
            {
                fallSpeed += Mathf.Min(fallVelocity * Time.deltaTime, 3f);
                transform.position -= fallVector * fallSpeed * Time.deltaTime;
                yield return waitForFrame;
                timer -= Time.deltaTime;
            }
        }

        private void Die()
        {
            isAlive = false;
            BeatRootAnimator.Play("A_Dead");
            OverlayMusicPlayer.DOFade(0f, 0.3f);
            MusicPlayer.DOFade(0.7f, 0.3f);
            LooseScreen.FadeIn();
        }

        private void Finish()
        {
            finished = true;
            var initialSpeed = Speed;
            DOVirtual.Float(1f, 0f, 2f, value => { Speed = Mathf.Lerp(initialSpeed, 0f, value); }).SetEase(Ease.OutCirc)
                .OnComplete(GoToFinish);
        }

        private void GoToFinish()
        {
            MusicPlayer.enabled = false;
            OverlayMusicPlayer.enabled = false;
            FinishScreen.SetActive(true);
        }
    }
}