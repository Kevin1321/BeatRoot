using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace BeatRoot
{
    public class CustomCameraControl : MonoBehaviour
    {
        private CinemachineBrain cinemachineBrain;
        private void Awake()
        {
            cinemachineBrain = GetComponent<CinemachineBrain>();
        }

        private void OnEnable()
        {
            PlayerController.OnPlayerFalling += DisableCinemachineBrain;
        }

        private void OnDisable()
        {
            PlayerController.OnPlayerFalling -= DisableCinemachineBrain;
        }

        private void DisableCinemachineBrain()
        {
            cinemachineBrain.enabled = false;
        }
    }
}
