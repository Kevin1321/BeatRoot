using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BeatRoot
{
    public class FinishScreen : MonoBehaviour
    {
        [SerializeField] private RectTransform box1;
        [SerializeField] private RectTransform box2;
        [SerializeField] private RectTransform balloon1;
        [SerializeField] private RectTransform balloon2;
        [SerializeField] private Image light1;
        [SerializeField] private Image light2;
        [SerializeField] private RectTransform rooty;

        [SerializeField] private AudioSource audioSource;
        [SerializeField] private float grooveTime;

        private float rootyRotationCount;
 
        private void OnEnable()
        {
            var alphaColor = new Color(255,255,255, 0.5f);
            var invisible = new Color(255, 255, 255, 0f);
            audioSource.Play();
            box1.DOShakePosition(grooveTime, new Vector3(7f, 7f)).SetEase(Ease.OutCubic).SetLoops(-1);
            box2.DOShakePosition(grooveTime, new Vector3(7f, 7f)).SetEase(Ease.OutCubic).SetLoops(-1);
            rooty.DOScale(new Vector3(1.8f, 1.8f), grooveTime).SetLoops(-1, LoopType.Yoyo);
            balloon1.DOMoveY(300, grooveTime).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo);
            balloon2.DOMoveY(300, grooveTime).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo);

            DOVirtual.Color(alphaColor, invisible, grooveTime, value => light1.color = value).SetLoops(-1, LoopType.Yoyo);
            DOVirtual.Color(invisible, alphaColor, grooveTime, value => light2.color = value).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
