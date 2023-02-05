using UnityEngine;


namespace BeatRoot
{
    public class ZombieHorde : MonoBehaviour
    {
        [SerializeField] private AudioSource AS;
        [SerializeField] private Transform BeatRoot;
        [SerializeField] private float offset;

        private void LateUpdate()
        {
            Vector3 position = BeatRoot.transform.position;
            position.x = position.x - offset;
            transform.position = position;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
            if (pc != null) pc.Dies();
        }

        public void GetCloser()
        {
            offset = offset - 1f;
            if(!AS.isPlaying) AS.Play();
        }
    }
}