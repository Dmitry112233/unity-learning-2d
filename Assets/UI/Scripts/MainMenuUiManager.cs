using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Scripts
{
    public class MainMenuUiManager : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button exitButton;
        
        private void Awake()
        {
            startButton.onClick.AddListener(OnStartClicked);
            exitButton.onClick.AddListener(OnExitClicked);
        }
        
        private void OnStartClicked()
        {
            SceneManager.LoadScene("GameScene");
        }

        private void OnExitClicked()
        {
            Application.Quit();

            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}
