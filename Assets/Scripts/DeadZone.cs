using UnityEngine;


namespace BeatRoot
{
    public class DeadZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
            if (pc != null) pc.Fall();
        }
    }
}
