using System;
using UnityEngine;

namespace UI
{
    public class StartPanel : MonoBehaviour
    {
        public static event Action PlayerStarted; 
        
        private void Update()
        {
            if (!Input.GetMouseButton(0)) return;
            
            gameObject.SetActive(false);
            PlayerStarted?.Invoke();
        }
    }
}
