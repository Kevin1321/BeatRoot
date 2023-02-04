using System;
using DG.Tweening;
using UnityEngine;

namespace BeatRoot
{
    public class InteractableField : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer SR;
        private bool hitOnSuccess;
        
        public void Use()
        {
            GetComponent<Collider2D>().enabled = false;
            DestroyOnUse();
        }

        private void DestroyOnUse()
        {
            hitOnSuccess = true;
            transform.DOScale(0.05f, 0.5f).SetEase(Ease.InBack).OnComplete(() => Destroy(gameObject));
        }

        private void Update()
        {
            if(hitOnSuccess)
                return;
            
            if (PlayerController.Instance.GetXPosition() > transform.position.x + 0.5f)
            {
                DestroyOnMiss();
            }
        }

        private void DestroyOnMiss()
        {
            SR.color = Color.gray;
            transform.DOShakePosition(0.4f, new Vector3(0.1f, 0, 0)).OnComplete(() => Destroy(gameObject));
        }
    }
}