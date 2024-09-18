using System;
using System.Linq;
using DG.Tweening;
using Items;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerRichChanger : MonoBehaviour
    {
        public static event Action Lost;
        
        [SerializeField] private Animator anim;
        [SerializeField] private int[] animIndexes;
        [SerializeField] private Transform visualsParent;
        [SerializeField] private GameObject[] visuals;
        [SerializeField] private Slider richSlider;
        [SerializeField] private Image sliderFill;
        [SerializeField] private Gradient sliderGradient;
        
        private int _richLevel;
        private float _richExp;
        private static readonly int RichLevel = Animator.StringToHash("RichLevel");

        private void Start()
        {
            _richLevel = 1;
            foreach (var visual in visuals) visual.SetActive(false);
            visuals[_richLevel].SetActive(true);
            
            richSlider.maxValue = 5 * visuals.Length;
            richSlider.value = 10;
            
            sliderFill.color = sliderGradient.Evaluate(richSlider.value / richSlider.maxValue);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Pickup"))
                return;
            
            ChangeRichLevel(other.GetComponent<PickupItem>().PickUp());
        }

        private void ChangeRichLevel(float value)
        {
            _richExp += value;
            richSlider.value += value;
            
            sliderFill.color = sliderGradient.Evaluate(richSlider.value / richSlider.maxValue);
            
            if (_richExp is < 5 and > -5) return;

            _richLevel += _richExp < 0 ? -1 : 1;
            _richExp = 0;

            if (_richLevel < 0)
            {
                Lost?.Invoke();
                return;
            }

            if (_richLevel >= visuals.Length)
                return;

            foreach (var visual in visuals) visual.SetActive(false);
            visuals[_richLevel].SetActive(true);
            anim.SetInteger(RichLevel, animIndexes.OrderBy(x => Mathf.Abs(x - _richLevel)).FirstOrDefault());
            visualsParent.DOKill();
            visualsParent.DOLocalRotate(new Vector3(0f, 360f, 0f), 1f, RotateMode.FastBeyond360);
        }
    }
}