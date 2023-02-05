using UnityEngine;

namespace BeatRoot
{
    public class ZombieHorde : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
            if (pc != null) pc.Dies();
        }
    }
}
