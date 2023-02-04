using UnityEngine;


namespace BeatRoot
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float Speed = 1.0f;

        private void Update()
        {
            Vector2 postion = transform.position;
            postion += new Vector2(Speed * Time.deltaTime, 0);
            
            transform.position = postion;
        }
    }
}