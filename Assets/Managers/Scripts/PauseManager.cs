using System;
using UnityEngine;

namespace Managers.Scripts
{
    public class PauseManager : MonoBehaviour
    {
        public static event Action OnPaused;
        public static event Action OnResumed;

        public static bool IsPaused { get; private set; }

        public static void PauseGame()
        {
            if (IsPaused) return;
        
            Time.timeScale = 0f;
            IsPaused = true;
            OnPaused?.Invoke();
        }

        public static void ResumeGame()
        {
            if (!IsPaused) return;
        
            Time.timeScale = 1f;
            IsPaused = false;
            OnResumed?.Invoke();
        }
    }
}
