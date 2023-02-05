using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace BeatRoot
{
    public class ZombieWiggle : MonoBehaviour
    {
        [SerializeField] private float startMove;
        private void OnEnable()
        {
            transform.DOLocalMoveY(startMove, 3).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
