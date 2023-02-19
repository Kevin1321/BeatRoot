using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BeatRoot
{
    public class LooseScreen : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private float backgroundAlpha;
        [SerializeField] private Image deadRoot;
        [SerializeField] private List<TextMeshProUGUI> texts;

        [Header("Tween times")] 
        [SerializeField] private float fadeInTimeBackground;
        [SerializeField] private float fadeInTimeDeadRoot;
        
        public void FadeIn()
        {
            var sequence = DOTween.Sequence();
            var backgroundColor = background.color;
            var deadRootColor = deadRoot.color;
            var textColor = texts[0].color;


            sequence.Append(DOVirtual.Float(0, backgroundAlpha, fadeInTimeBackground, value =>
            {
                backgroundColor.a = value;
                background.color = backgroundColor;
            }));
            sequence.Append(DOVirtual.Float(0, 1, fadeInTimeDeadRoot, value =>
            {
                deadRootColor.a = value;
                textColor.a = value;
                deadRoot.color = deadRootColor;
                texts.ForEach(x => x.color = textColor);
            }));
            
        }
    }
}
