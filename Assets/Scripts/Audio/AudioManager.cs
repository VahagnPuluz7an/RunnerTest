using Pooling;
using UnityEngine;
using Utils;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioElement elementExample;

        private static PoolMono<AudioElement> _audios;

        private void Start()
        {
            _audios = new PoolMono<AudioElement>(elementExample, 10)
            {
                AutoExpand = true
            };
        }

        public static void PlaySound(AudioClip clip)
        {
            var audio = _audios.GetFreeElement();
            audio.Source.PlayOneShot(clip);
            audio.SetActive(false, clip.length);
        }
    }
}
