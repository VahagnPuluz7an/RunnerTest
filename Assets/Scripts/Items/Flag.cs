using Audio;
using UnityEngine;

namespace Items
{
    public class Flag : MonoBehaviour
    {
        [SerializeField] private Animator anim;
        [SerializeField] private AudioClip openSound;
        
        private static readonly int Open = Animator.StringToHash("Open");

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;
            
            anim.SetTrigger(Open);
            AudioManager.PlaySound(openSound);
        }
    }
}
