using System;
using UnityEngine;


namespace BeatRoot
{
    public class ZombieHorde : MonoBehaviour
    {
        [SerializeField] private AudioSource Growl;
        [SerializeField] private AudioSource Mampf;
        [SerializeField] private Transform BeatRoot;
        [SerializeField] private float offset;

        private bool followPlayer = true;

        private void OnEnable()
        {
            PlayerController.OnPlayerFalling += StopFollowing;
        }

        private void OnDisable()
        {
            PlayerController.OnPlayerFalling -= StopFollowing;
        }

        private void LateUpdate()
        {
            if(!followPlayer) return;
            
            Vector3 position = BeatRoot.transform.position;
            position.x = position.x - offset;
            position.y = position.y + 2;
            transform.position = position;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.GetEaten();
                Mampf.Play();
            }
        }

        public void GetCloser()
        {
            offset = offset - 4f;
            Growl.Play();
        }

        private void StopFollowing()
        {
            followPlayer = false;
        }
    }
}