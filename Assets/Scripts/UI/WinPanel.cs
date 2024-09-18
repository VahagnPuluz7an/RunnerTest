using ButchersGames;
using UnityEngine;

namespace UI
{
    public class WinPanel : MonoBehaviour
    {
        private void Awake()
        {
            Finisher.Finished += FinisherOnFinished;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            Finisher.Finished -= FinisherOnFinished;
        }
        
        public void NextLevel()
        {
            LevelManager.Default.NextLevel();
        }
        
        private void FinisherOnFinished()
        {
            gameObject.SetActive(true);
        }
    }
}
