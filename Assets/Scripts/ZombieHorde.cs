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
            position.y = position.y + 2;
            transform.position = position;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
            if (pc != null) pc.GetEaten();
        }

        public void GetCloser()
        {
            offset = offset - 4f;
            AS.Play();
        }
    }
}