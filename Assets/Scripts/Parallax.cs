using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace BeatRoot
{
    public class Parallax : MonoBehaviour
    {
        private float length, startpos;

        public GameObject Camera;
        public float parallaxEffect;

        private void Start()
        {
            startpos = transform.position.x;
            length = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        private void LateUpdate()
        {
            float temp = (Camera.transform.position.x * (1 - parallaxEffect));
            float dist = (Camera.transform.position.x * parallaxEffect);

            transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

            if (temp > startpos + length) startpos += length;
            else if (temp < startpos - length) startpos -= length;  
        }
    }
}