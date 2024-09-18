using System;
using Audio;
using DG.Tweening;
using UnityEngine;
using Utils;

namespace Items
{
    public class PickupItem : MonoBehaviour
    {
        [SerializeField] private ParticleSystem takeFX;
        [SerializeField] private GameObject visual;
        [SerializeField, Range(-1f, 1f)] private float richValue;
        [SerializeField] private Vector3 rotate;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private Vector3 punch;
        [SerializeField] private float punchDuration;
        [SerializeField] private AudioClip pickupSound;
        
        private void Start()
        {
            transform.DOPunchPosition(punch, punchDuration, 1).SetLoops(-1);
        }

        private void Update()
        {
            transform.Rotate(rotate * rotateSpeed);
        }

        public float PickUp()
        {
            takeFX.Play();
            visual.SetActive(false);
            transform.SetActive(false,2f);
            transform.DOKill();
            AudioManager.PlaySound(pickupSound);
            return richValue;
        }
    }
}
