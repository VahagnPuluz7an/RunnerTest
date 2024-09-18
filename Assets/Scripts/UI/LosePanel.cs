using System;
using ButchersGames;
using Player;
using UnityEngine;

namespace UI
{
    public class LosePanel : MonoBehaviour
    {
        private void Awake()
        {
            PlayerRichChanger.Lost += PlayerRichChangerOnLost;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            PlayerRichChanger.Lost -= PlayerRichChangerOnLost;
        }

        private void PlayerRichChangerOnLost()
        {
            gameObject.SetActive(true);
        }

        public void RestartLevel()
        {
            LevelManager.Default.RestartLevel();
        }
    }
}
