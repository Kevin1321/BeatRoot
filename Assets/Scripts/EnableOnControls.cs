
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeatRoot
{
    public class EnableOnControls : MonoBehaviour
    {
        [SerializeField] private bool isControllerElement;
        void Start()
        {
            gameObject.SetActive(InputDetection.Instance.IsControllerAttached == isControllerElement);
        }
    }
}
