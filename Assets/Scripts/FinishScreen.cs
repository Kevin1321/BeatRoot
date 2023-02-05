using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace BeatRoot
{
    public class FinishScreen : MonoBehaviour
    {
        [SerializeField] private RectTransform box1;
        [SerializeField] private RectTransform box2;
        [SerializeField] private RectTransform balloon1;
        [SerializeField] private RectTransform balloon2;
        [SerializeField] private RectTransform light1;
        [SerializeField] private RectTransform light2;
        [SerializeField] private RectTransform rooty;

        [SerializeField] private AudioSource audioSource;
 
        private void OnEnable()
        {
            audioSource.Play();
            box1.DOShakePosition(0.5f, new Vector3(7f, 7f)).SetEase(Ease.OutCubic).SetLoops(-1);
            box2.DOShakePosition(0.5f, new Vector3(7f, 7f)).SetEase(Ease.OutCubic).SetLoops(-1);
            //rooty.DOScale(new Vector3(1.3f, 1.3f), 0.5f).SetLoops(-1, LoopType.Yoyo);
            //balloon1.DOMoveY(200, 0.5f).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo);
            //balloon2.DOMoveY(200, 0.5f).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
